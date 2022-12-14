﻿using BLL.Interface;
using DTO.User;
using Microsoft.AspNetCore.Mvc;
using Services.CookieServices;
using Services.RedisService;
using Services.SessionServices;
using Utilities.Extentions;


namespace BlankProject.Controllers
{
    /// <summary>
    /// صفحه لاگین کاربران با یوزر پس
    /// </summary>
    public class AuthenticationController : Controller
    {
        private readonly IAuthManager AuthManager;
        private readonly IUserLogManager UserLogManager;
        private readonly IConstantManager ConstantManager;
        private readonly IRedisManager Redis;
        private readonly ISession Session;

        public AuthenticationController(IAuthManager _AuthManager, IUserLogManager _UserLogManager, IRedisManager _Redis, IConstantManager constantManager) : base()
        {
            AuthManager = _AuthManager;
            UserLogManager = _UserLogManager;
            Redis = _Redis;
            Session = Redis.ContextAccessor.HttpContext.Session;
            ConstantManager = constantManager;
        }


        #region لاگین به پنل کاربر
        /// <summary>
        /// لاگین
        /// </summary>
        public IActionResult Index(long? mid)
        {
            if (mid == null)
            {
                var User = Session.GetUser();
                if (User != null)
                    return RedirectToAction("index", "Dashboard", new { area = "Admin" });
            }
            return View();
        }


        public IActionResult Login()
        {
            return View("index");
        }




        /// <summary>
        /// لاگین با یوزر پس
        /// </summary>
        /// <param name="Mobile">نام کاربری</param>
        /// <param name="Password">پسورد</param>
        /// <param name="RetUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index(string Mobile, string Password, string Captcha, string RetUrl, long? MenuId)
        {
            // برای دفعات بعدی لاگین در صورت اشتباه وارد کردن کلمه عبور، کپچا نشان داده شود
            ViewBag.captcha = true;

            #region بررسی تعداد تلاش برای لاگین و قفل بودن حساب کاربری
            //لاگ دفعات تلاش برای لاگین
            var loginLog = await Redis.db.SetLoginLog(Mobile);
            var FailedLoginCount = ConstantManager.GetFailedLoginCount();
            if (loginLog != null && loginLog.Count > FailedLoginCount)
            {
                var diff = (int)(loginLog.CreateDate.AddMinutes(20) - DateTime.Now).TotalMinutes;
                await Redis.db.SetLoginLog(Redis.ContextAccessor, Mobile, null, false, $"مسدود شدن حساب کاربری تا {diff} دقیقه دیگر به دلیل {loginLog.Count} بار ورود اشتباه کلمه عبور. ");
                ViewBag.Error = $"حساب کاربری شما بدلیل ورود اشتباه کلمه عبور تا {diff} دقیقه آینده مسدود می باشد.";
                return View();
            }
            #endregion

            #region بررسی کپچا در صورت وجود
            if (HttpContext.Request.Form.Keys.Any(x => x.Equals("captcha", StringComparison.OrdinalIgnoreCase)))
            {
                var captcha = HttpContext.Session.GetString("Captcha")?.Trim().ToEnglishNumber();
                if (string.IsNullOrEmpty(captcha) || captcha != Captcha)
                {
                    await Redis.db.SetLoginLog(Redis.ContextAccessor, Mobile, null, false, "کد امنیتی صحیح نیست!");
                    ViewBag.Error = "کد امنیتی صحیح نیست!";
                    return View();
                }
            }
            #endregion

            #region عملیات لاگین
            var res = AuthManager.Login(Mobile, Password);
            if (!res.Status)
            {
                var UserId = res.Model as long?;
                await Redis.db.SetLoginLog(Redis.ContextAccessor, Mobile, UserId, false, res.Message);
                if (UserId == null)
                {
                    ViewBag.Error = res.Message;
                    ViewBag.InvalidLogin = true;

                    return View();
                }
            } 
            #endregion

            #region اطلاعات ردیس و کوکی
            var User = res.Model as UserSessionDTO;
            if (MenuId == null)
            {
                // افزودن لاگ برای لاگین موفق
                await Redis.db.SetLoginLog(Redis.ContextAccessor, Mobile, User.Id, true, $"کاربر {User.FullName} وارد شد.");

                // افزودن توکن کاربر به ردیس برای جلوگیری از لاگین همزمان 2 نفر با یک اکانت
                var token = await Redis.db.SetLoginToken(User.Id);

                // افزودن توکن به کوکی
                HttpContext.SetCookieUserToken(token);
            }
            else
            {
                // اگر لاگین جهت احراز هویت مجدد برای منو خاص بود، اطلاعات درون ردیس ذخیره شود
                await Redis.db.SetUserReAuthorizeMenu((int)MenuId, User.Id);
            }
            // حذف اطلاعات مربوط به کنترل تعداد دفعات تلاش برای لاگین
            await Redis.db.RemoveLoginLog(Mobile);
            #endregion

            if (!string.IsNullOrEmpty(RetUrl))
                return Redirect(RetUrl);

            return RedirectToAction("index", "Dashboard", new { area = "Admin" });
        }
        #endregion


        #region خروج از حساب کاربری - logout
        public ActionResult Logout()
        {
            var user = Session.GetUser();
            _ = Redis.db.SetLoginLog(Redis.ContextAccessor, user.Username, user.Id, true, $"کاربر {user.FullName} خارج شد.", true).Result;
            Session.RemoveUser();
            HttpContext.Response.Cookies.Delete("_Session.cookie");
            return RedirectToAction("index");
        }
        #endregion


    }
}