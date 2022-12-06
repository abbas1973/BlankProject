﻿using BLL.Interface;
using Domain.Enums;
using DTO.User;
using Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Services.RedisService;
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
        private readonly IRedisManager Redis;
        public UsersController(IRedisManager _Redis, IUserManager _UserManager, IRoleManager _roleManager, IDataTableManager _dataTableManager)
        {
            UserManager = _UserManager;
            roleManager = _roleManager;
            dataTableManager = _dataTableManager;
            Redis = _Redis;
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
        public IActionResult Create(UserCreateDTO model)
        {
            try
            {
                if (model.RoleId == 0)
                {
                    _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Create, MenuType.Users, false, "نوع دسترسی کاربر را مشخص کنید!").Result;
                    return Json(new { Status = false, Message = "نوع دسترسی کاربر را مشخص کنید!" });
                }
                if (ModelState.IsValid)
                {
                    var res = UserManager.Create(model);
                    _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Create, MenuType.Users, res.Status,
                    res.Status ? $"پرسنل {model.Name} با آیدی {(long?)res.Model} : " + res.Message : res.Message,
                    res.Status ? (long?)res.Model : null).Result;
                    return Json(res);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Create, MenuType.Users, false, string.Join("/", errors)).Result;
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
        public IActionResult Edit(long id, UserEditDTO model)
        {
            try
            {
                if (id != model.Id)
                {
                    _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Update, MenuType.Users, false, "کاربر یافت نشد!", id).Result;
                    return Json(new { Status = false, Message = "کاربر یافت نشد!" });
                }
                if (model.RoleId == null)
                {
                    _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Update, MenuType.Users, false, "نوع دسترسی کاربر را مشخص کنید!", id).Result;
                    return Json(new { Status = false, Message = "نوع دسترسی کاربر را مشخص کنید!" });
                }

                if (ModelState.IsValid)
                {
                    var res = UserManager.Update(model);
                    _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Update, MenuType.Users, res.Status, $"کاربر {model.Username} با آیدی {model.Id} : " + res.Message, model.Id).Result;
                    return Json(res);
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Update, MenuType.Users, false, $"کاربر {model.Username} با آیدی {model.Id} : " + string.Join("/", errors), model.Id).Result;
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
            if (ModelState.IsValid)
            {
                var res = UserManager.ChangePassword(model);
                _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.ChangePassword, MenuType.Users, res.Status, $"کاربر با آیدی {model.Id} : " + res.Message).Result;
                return Json(res);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.ChangePassword, MenuType.Users, false, $"کاربر با آیدی {model.Id} : " + string.Join("/", errors), model.Id).Result;
                return Json(new
                {
                    Status = false,
                    Message = string.Join("</br>", errors)
                });
            }
        }

        #endregion


        #region ریست کردن کلمه عبور
        //[HttpPost]
        //public IActionResult ResetPassword(long id)
        //{
        //    var res = UserManager.ResetPassword(id);
        //    _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.ResetPassword, MenuType.Users, res.Status, $"کاربر با آیدی {id} : " + res.Message, id).Result;
        //    return Json(res);
        //}
        #endregion


        #region تغییر وضعیت فعال بودن یا نبودن کاربر 
        public IActionResult ToggleEnable(long id)
        {
            if (id == 0)
                return Json(new { Status = false });
            var model = UserManager.GetById(id);
            model.IsEnabled = !model.IsEnabled;
            var res = UserManager.Update(model);
            _ = Redis.db.SetLog(Redis.ContextAccessor, (model.IsEnabled ? ActionType.Enable : ActionType.Disable), MenuType.Users, res.Status, $"کاربر {model.Name} با آیدی {id} : " + res.Message, id).Result;
            return Json(new { res.Status, model.IsEnabled });
        }
        #endregion


        #region حذف
        [HttpPost]
        public IActionResult Delete(long id)
        {
            bool IsSuccess = UserManager.Delete(id);
            _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Remove, MenuType.Users, IsSuccess, $"کاربر با آیدی {id} : " + (IsSuccess ? "کاربر با موفقیت حذف شد" : "حذف کاربر با خطا همراه بوده است! ابتدا مطمئن شوید که این کاربر در جای دیگری از سایت مورد استفاده قرار نگرفته است!"), id).Result;
            return Json(new
            {
                Status = IsSuccess,
                Message = IsSuccess ? "کاربر با موفقیت حذف شد" : "حذف کاربر با خطا همراه بوده است! ابتدا مطمئن شوید که این کاربر در جای دیگری از سایت مورد استفاده قرار نگرفته است!"
            });
        }
        #endregion


    }
}
