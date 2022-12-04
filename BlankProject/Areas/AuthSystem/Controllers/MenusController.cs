using BLL.Interface;
using Domain.Entities;
using Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlankProject.Areas.AuthSystem.Controllers
{
    /// <summary>
    /// مدیریت منو های مشتریان
    /// </summary>
    [Area("AuthSystem")]
    [UserAuthorize(Area: "AuthSystem", Controller: "menus", Action: "index")]
    public class MenusController : Controller
    {
        private readonly IMenuManager menuManager;
        public MenusController(IMenuManager _menuManager)
        {
            menuManager = _menuManager;
        }

        #region نمایش همه
        public IActionResult Index()
        {
            return View();
        }


        #region گرفتن همه منو ها با اجکس
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = menuManager.GetTreeViewDTO();
            return Json(model);
        }
        #endregion
        #endregion        


        #region ایجاد
        /// <summary>
        /// لود کردن فرم ایجاد منو در مدال
        /// </summary>
        /// <param name="ParentId">شناسه والد</param>
        /// <param name="ParentName">عنوان والد</param>
        /// <returns></returns>
        public IActionResult LoadCreateForm(long? ParentId, string ParentName)
        {
            var model = new Menu
            {
                ParentId = ParentId,
            };

            ViewBag.ParentName = ParentName;
            return PartialView("_Create", model);
        }


        /// <summary>
        /// ایجاد منو جدید
        /// </summary>
        /// <param name="menu">منو</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var res = menuManager.Create(menu);
                return Json(new { res.Status, res.Message });
            }
            else
                return Json(new { Status = false, Message = "اطلاعات وارد شده صحیح نمی باشد!" });
        }
        #endregion


        #region ویرایش
        /// <summary>
        /// لود کردن فرم ویرایش منو در مدال
        /// </summary>
        /// <returns></returns>
        public IActionResult LoadEditForm(long? id)
        {
            var model = menuManager.GetById(id);
            return PartialView("_Edit", model);
        }


        /// <summary>
        /// ویرایش اطلاعات منو
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                var res = menuManager.Update(menu);
                return Json(new { res.Status, res.Message });
            }
            else
                return Json(new { Status = false, Message = "اطلاعات وارد شده صحیح نمی باشد!" });
        }

        #endregion


        #region حذف
        /// <summary>
        /// حذف منو
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(long id)
        {
            var HasChild = menuManager.HasChild(id);
            if (HasChild)
                return Json(new { Status = false, Message = "لطفا ابتدا زیر مجموعه های این منو را حذف کنید!" });

            var res = menuManager.DeleteWithRoles(id);
            return Json(res);
        }
        #endregion


    }
}
