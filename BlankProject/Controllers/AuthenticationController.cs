using BLL.Interface;
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
        private readonly IRedisManager Redis;
        private readonly ISession Session;

        public AuthenticationController(IAuthManager _AuthManager, IUserLogManager _UserLogManager, IRedisManager _Redis) : base()
        {
            AuthManager = _AuthManager;
            UserLogManager = _UserLogManager;
            Redis = _Redis;
            Session = Redis.ContextAccessor.HttpContext.Session;
        }


        #region لاگین به پنل کاربر
        /// <summary>
        /// لاگین
        /// </summary>
        public IActionResult Index()
        {
            var User = Session.GetUser();
            if (User != null)
                return RedirectToAction("index", "Dashboard", new { area = "Admin" });

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
        public async Task<IActionResult> Index(string Mobile, string Password, string Captcha, string RetUrl)
        {
            var loginLog = await Redis.db.SetLoginLog(Mobile);
            if (loginLog != null && loginLog.Count > 5)
            {
                var diff = (int)(loginLog.CreateDate.AddMinutes(20) - DateTime.Now).TotalMinutes;
                await Redis.db.SetLoginLog(Redis.ContextAccessor, Mobile, false, $"مسدود شدن حساب کاربری تا {diff} دقیقه دیگر به دلیل {loginLog.Count} بار ورود اشتباه کلمه عبور. ");
                ViewBag.Error = $"حساب کاربری شما بدلیل ورود اشتباه کلمه عبور تا {diff} دقیقه آینده مسدود می باشد.";
                return View();
            }

            var captcha = HttpContext.Session.GetString("Captcha")?.Trim().ToEnglishNumber();
            if (string.IsNullOrEmpty(captcha) || captcha != Captcha)
            {
                await Redis.db.SetLoginLog(Redis.ContextAccessor, Mobile, false, "کد امنیتی صحیح نیست!");
                ViewBag.Error = "کد امنیتی صحیح نیست!";
                return View();
            }

            var res = AuthManager.Login(Mobile, Password);
            if (!res.Status)
            {
                await Redis.db.SetLoginLog(Redis.ContextAccessor, Mobile, false, res.Message);
                var UserId = res.Model as long?;
                if (UserId == null)
                {
                    ViewBag.Error = res.Message;
                    ViewBag.InvalidLogin = true;

                    return View();
                }
            }

            #region اطلاعات ردیس و کوکی
            var User = res.Model as UserSessionDTO;
            // افزودن لاگ برای لاگین موفق
            await Redis.db.SetLoginLog(Redis.ContextAccessor, Mobile, true, $"کاربر {User.FullName} وارد شد.");
            // حذف اطلاعات مربوط به کنترل تعداد دفعات تلاش برای لاگین
            await Redis.db.RemoveLoginLog(Mobile);
            // افزودن توکن کاربر به ردیس برای جلوگیری 
            var token = await Redis.db.SetLoginToken(User.Id);
            // افزودن توکن به کوکی
            HttpContext.SetCookieUserToken(token);
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
            _ = Redis.db.SetLoginLog(Redis.ContextAccessor, user.Username, true, $"کاربر {user.FullName} خارج شد.", true).Result;
            Session.RemoveUser();
            HttpContext.Response.Cookies.Delete("_Session.cookie");
            return RedirectToAction("index");
        }
        #endregion


    }
}