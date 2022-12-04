using BLL.Interface;
using Domain.Entities;
using DTO.Base;
using Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlankProject.Areas.AuthSystem.Controllers
{
    /// <summary>
    /// مدیریت نقش های کاربران
    /// </summary>
    [Area("AuthSystem")]
    [UserAuthorize(Area: "AuthSystem", Controller: "roles", Action: "index")]
    public class RolesController : Controller
    {
        private readonly IRoleManager roleManager;
        private readonly IRoleMenuManager roleMenuManager;
        private readonly IMenuManager menuManager;
        private readonly IDataTableManager dataTableManager;
        public RolesController(IRoleManager _roleManager, IMenuManager _menuManager, IRoleMenuManager _roleMenuManager, IDataTableManager _dataTableManager)
        {
            roleManager = _roleManager;
            menuManager = _menuManager;
            roleMenuManager = _roleMenuManager;
            dataTableManager = _dataTableManager;
        }


        #region نمایش همه
        public IActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// لیست داده مورد نیاز برای دیتاتیبل
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetList()
        {
            var SearchModel = dataTableManager.GetSearchModel();
            var model = roleManager.GetDataTableDTO(SearchModel);
            return Json(model);
        }

        #endregion


        #region ایجاد

        /// <summary>
        /// لود کردن فرم ایجاد نقش در مدال
        /// </summary>
        /// <returns></returns>
        public IActionResult LoadCreateForm()
        {
            var model = new Role();
            return PartialView("_Create", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                var res = await roleManager.CreateAsync(role);
                return Json(res);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new
                {
                    Status = false,
                    Message = string.Join("</br>", errors)
                });
            }
        }
        #endregion


        #region ویرایش
        /// <summary>
        /// لود کردن فرم ایجاد نقش در مدال
        /// </summary>
        /// <returns></returns>
        public IActionResult LoadEditForm(long id)
        {
            var role = roleManager.GetById(id);
            return PartialView("_Edit", role);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                var res = roleManager.Update(role);
                return Json(res);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new
                {
                    Status = false,
                    Message = string.Join("</br>", errors)
                });
            }
        }

        #endregion


        #region منوهایی که نقش به آنها دسترسی دارد 

        #region گرفتن لیست منو ها به همراه دسترسی های یک نقش
        // لیست همه دسترسی به منوها
        public IActionResult LoadAccess(int id)
        {
            var RoleMenus = roleMenuManager.GetByRoleId(id);
            return Json(RoleMenus);
        }


        // لیست همه منو های فعال
        public IActionResult LoadMenus()
        {
            var Menus = menuManager.GetTreeViewDTO(true);
            return Json(Menus);
        }
        #endregion


        #region ذخیره دسترسی های نقش
        [HttpPost]
        public IActionResult SaveAccess(long RoleId, List<long> MenuIds)
        {
            var xxx = HttpContext.Request;
            if (MenuIds.Count == 0)
                return Json(new BaseResult(false, "منویی انتخاب نشده است!"));
            var res = roleMenuManager.UpdateRoleMenus(RoleId, MenuIds);
            return Json(res);
        }
        #endregion

        #endregion



        #region تغییر وضعیت فعال بودن یا نبودن نقش 
        public IActionResult ToggleEnable(long id)
        {
            if (id == 0)
                return Json(new { Status = false });
            var role = roleManager.GetById(id);
            role.IsEnabled = !role.IsEnabled;
            var res = roleManager.Update(role);
            return Json(new { res.Status, role.IsEnabled });
        }
        #endregion


        #region حذف
        [HttpPost]
        public IActionResult Delete(long id)
        {
            var res = roleManager.DeleteWithMenus(id);
            return Json(res);
        }
        #endregion

    }
}
