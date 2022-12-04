using BLL.Interface;
using DTO.User;
using Microsoft.AspNetCore.Mvc;
using Services.CookieServices;
using Services.SessionServices;
using Utilities.Extentions;

namespace BlankProject.Controllers
{
    /// <summary>
    /// صفحه لاگین کاربران با یوزر پس
    /// </summary>
    public class AuthenticationController : Controller
    {
        private readonly ISession session;
        private readonly IAuthManager authManager;
        public AuthenticationController(IHttpContextAccessor _httpContextAccessor, IAuthManager _authManager)
        {
            session = _httpContextAccessor.HttpContext.Session;
            authManager = _authManager;
        }


        #region لاگین به پنل کاربر
        /// <summary>
        /// لاگین
        /// </summary>
        public IActionResult Index()
        {
            var User = session.GetUser();
            if (User != null)
                return RedirectToAction("index", "Dashboard", new { area = "Admin" });
            return View();
        }


        public IActionResult Login()
        {
            var User = session.GetUser();
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
        public IActionResult Index(string Mobile, string Password, string Captcha, string RetUrl)
        {

            var captcha = HttpContext.Session.GetString("Captcha")?.Trim().ToEnglishNumber();
            if (string.IsNullOrEmpty(captcha) || captcha != Captcha)
            {
                ViewBag.Error = "کد امنیتی صحیح نیست!";
                return View();
            }

            var res = authManager.Login(Mobile, Password);
            if (!res.Status)
            {
                var UserId = res.Model as int?;
                if (UserId == null)
                {
                    ViewBag.Error = res.Message;
                    ViewBag.InvalidLogin = true;
                    return View();
                }
                //else //تایید موبایل
                //    return RedirectToAction("index", "ForgetPassword", new { ui = UserId });
            }

            if (!string.IsNullOrEmpty(RetUrl))
                return Redirect(RetUrl);


            var User = res.Model as UserSessionDTO;
            return RedirectToAction("index", "Dashboard", new { area = "Admin" });
        }
        #endregion








        #region خروج از حساب کاربری - logout
        public ActionResult Logout()
        {
            session.RemoveUser();
            HttpContext.Response.Cookies.Delete("_session.cookie");
            return RedirectToAction("index");
        }
        #endregion



    }
}