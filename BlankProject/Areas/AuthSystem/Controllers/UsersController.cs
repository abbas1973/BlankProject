using BLL.Interface;
using Domain.Enums;
using DTO.User;
using Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities.Extentions;

namespace BlankProject.Areas.Admin.Controllers
{
    /// <summary>
    /// مدیریت کاربران - پرسنل
    /// </summary>
    [Area("AuthSystem")]
    [UserAuthorize(Area: "AuthSystem", Controller: "users", Action: "index")]
    public class UsersController : Controller
    {
        private readonly IUserManager UserManager;
        private readonly IRoleManager roleManager;
        private readonly IDataTableManager dataTableManager;
        public UsersController(IUserManager _UserManager, IRoleManager _roleManager, IDataTableManager _dataTableManager)
        {
            UserManager = _UserManager;
            roleManager = _roleManager;
            dataTableManager = _dataTableManager;
        }

        #region نمایش همه
        public IActionResult Index()
        {
            ViewData["Roles"] = new SelectList(roleManager.GetSelectListDTO(), "Id", "Title");
            return View();
        }


        /// <summary>
        /// لیست داده مورد نیاز برای دیتاتیبل
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetList(UserFilterDataTableDTO filters)
        {
            var SearchModel = dataTableManager.GetSearchModel();
            var model = UserManager.GetDataTableDTO(SearchModel, filters);
            return Json(model);
        }
        #endregion


        #region ایجاد
        /// <summary>
        /// لود کردن فرم ایجاد در مدال
        /// </summary>
        /// <returns></returns>
        public IActionResult LoadCreateForm()
        {
            ViewData["RoleId"] = new SelectList(roleManager.GetSelectListDTO(), "Id", "Title");
            ViewData["UserType"] = new SelectList(EnumExtensions.ToEnumViewModel<UserType>(), "Id", "Title");

            var model = new UserCreateDTO();
            return PartialView("_Create", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserCreateDTO User)
        {
            try
            {
                if (User.RoleId == 0)
                    return Json(new { Status = false, Message = "نوع دسترسی کاربر را مشخص کنید!" });

                if (ModelState.IsValid)
                {
                    var res = UserManager.Create(User);
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
            catch
            {
                return Json(new { Status = false, Message = "ثبت اطلاعات با خطا همراه بوده است!" });
            }
        }
        #endregion


        #region ویرایش
        /// <summary>
        /// لود کردن فرم ویرایش در مدال
        /// </summary>
        /// <returns></returns>
        public IActionResult LoadEditForm(long id)
        {
            var User = UserManager.GetEditDTO(id);
            if (User == null) return NotFound();

            ViewData["RoleId"] = new SelectList(roleManager.GetSelectListDTO(), "Id", "Title", User.RoleId);
            ViewData["UserType"] = new SelectList(EnumExtensions.ToEnumViewModel<UserType>(), "Id", "Title");

            return PartialView("_Edit", User);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, UserEditDTO User)
        {
            try
            {
                if (id != User.Id)
                    return Json(new { Status = false, Message = "کاربر یافت نشد!" });
                if (User.RoleId == null)
                    return Json(new { Status = false, Message = "نوع دسترسی کاربر را مشخص کنید!" });

                if (ModelState.IsValid)
                {
                    var res = UserManager.Update(User);
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
            catch
            {
                return Json(new { Status = false, Message = "ثبت اطلاعات با خطا همراه بوده است!" });
            }
        }

        #endregion


        #region تغییر کلمه عبور

        public IActionResult LoadChangePasswordForm(long id, string FullName)
        {
            ViewBag.FullName = FullName;
            var model = new UserChangePasswordDTO() { Id = id };
            return PartialView("_ChangePassword", model);
        }


        [HttpPost]
        public IActionResult ChangePassword(long id, UserChangePasswordDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = UserManager.ChangePassword(model);
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
            catch
            {
                return Json(new { Status = false, Message = "ثبت اطلاعات با خطا همراه بوده است!" });
            }
        }

        #endregion

        

        #region تغییر وضعیت فعال بودن یا نبودن کاربر 
        public IActionResult ToggleEnable(long id)
        {
            if (id == 0)
                return Json(new { Status = false });
            var user = UserManager.GetById(id);
            user.IsEnabled = !user.IsEnabled;
            var res = UserManager.Update(user);
            return Json(new { res.Status, user.IsEnabled });
        }
        #endregion


        #region حذف
        [HttpPost]
        public IActionResult Delete(long id)
        {
            bool IsSuccess = UserManager.Delete(id);
            return Json(new
            {
                Status = IsSuccess,
                Message = IsSuccess ? "کاربر با موفقیت حذف شد" : "حذف کاربر با خطا همراه بوده است! ابتدا مطمئن شوید که این کاربر در جای دیگری از سایت مورد استفاده قرار نگرفته است!"
            });
        }
        #endregion


    }
}
