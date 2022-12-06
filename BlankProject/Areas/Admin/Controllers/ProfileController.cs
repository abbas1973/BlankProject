﻿using BLL.Interface;
using Domain.Enums;
using DTO.User;
using Filters;
using Microsoft.AspNetCore.Mvc;
using Services.RedisService;
using Services.SessionServices;

namespace BlankProject.Areas.Admin.Controllers
{

    /// <summary>
    /// پروفایل کاربر
    /// </summary>

    [Area("Admin")]
    //[UserAuthorize(Area: "admin", Controller: "profile", Action: "edit")]
    [UserAuthorize(IsPublic: true, CheckPasswordChange: false)]
    public class ProfileController : Controller
    {
        private readonly IUserManager userManager;
        private readonly ISession session;
        private readonly IRedisManager Redis;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProfileController(IUserManager _userManager, IHttpContextAccessor _httpContextAccessor, IRedisManager _Redis)
        {
            userManager = _userManager;
            httpContextAccessor = _httpContextAccessor;
            Redis = _Redis;
            session = _httpContextAccessor.HttpContext.Session;
        }


        #region ویرایش
        [UserAuthorize(IsPublic: true)]
        public IActionResult Edit()
        {
            var user = session.GetUser();
            var model = userManager.GetEditProfileDTO(user.Id);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserEditProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                var res = userManager.UpdateProfile(model);
                _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Update, MenuType.Profile, res.Status, $"کاربر {model.Username} با آیدی {model.Id} : " + res.Message, model.Id).Result;
                return Json(res);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Update, MenuType.Profile, false, $"کاربر {model.Username} با آیدی {model.Id} : " + string.Join("/", errors), model.Id).Result;
                return Json(new
                {
                    Status = false,
                    Message = string.Join("</br>", errors)
                });
            }
        }

        #endregion


        #region تغییر کلمه عبور
        public IActionResult ChangePassword()
        {
            var user = session.GetUser();
            ViewBag.FullName = user.FullName;
            var model = new UserProfileChangePasswordDTO() { Id = user.Id, PasswordIsChanged = user.PasswordIsChanged };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(UserProfileChangePasswordDTO model, string Captcha)
        {
            var captcha = HttpContext.Session.GetString("Captcha");
            if (string.IsNullOrEmpty(captcha) || captcha != Captcha)
            {
                _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.ChangePassword, MenuType.Profile, false, $"کاربر با آیدی {model.Id} : " + "کد امنیتی صحیح نیست!", model.Id).Result;
                return Json(new { Status = false, Message = "کد امنیتی صحیح نیست!" });
            }

            if (ModelState.IsValid)
            {
                var res = userManager.ProfileChangePassword(model);
                _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.ChangePassword, MenuType.Profile, res.Status, $"کاربر با آیدی {model.Id} : " + res.Message, model.Id).Result;
                return Json(res);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                _ = Redis.db.SetLog(Redis.ContextAccessor, ActionType.Update, MenuType.Profile, false, $"کاربر با آیدی {model.Id} : " + string.Join("/", errors), model.Id).Result;
                return Json(new
                {
                    Status = false,
                    Message = string.Join("</br>", errors)
                });
            }
        }
        #endregion

    }
}
