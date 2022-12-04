using BLL.Interface;
using DTO.User;
using Filters;
using Microsoft.AspNetCore.Mvc;
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
        public ProfileController(IUserManager _userManager, IHttpContextAccessor _httpContextAccessor)
        {
            userManager = _userManager;
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
                return Json(new { Status = false, Message = "کد امنیتی صحیح نیست!" });
            }

            if (ModelState.IsValid)
            {
                var res = userManager.ProfileChangePassword(model);
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

    }
}
