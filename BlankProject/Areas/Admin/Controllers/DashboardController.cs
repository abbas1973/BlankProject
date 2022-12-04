using BLL.Interface;
using Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services.CacheServices;
using Services.SessionServices;
using Utilities.Extentions;

namespace BlankProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[UserAuthorize(Area: "admin", Controller: "dashboard", Action: "index")]
    [UserAuthorize(IsPublic: true)]
    public class DashboardController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly IUserManager userManager;
        private readonly ISession session;
        public DashboardController(IUserManager _userManager, IHttpContextAccessor _httpContextAccessor, IMemoryCache _cache)
        {
            userManager = _userManager;
            cache = _cache;
            session = _httpContextAccessor.HttpContext.Session;
        }


        public IActionResult Index()
        {
            return View();
        }



        #region پاک کردن کش
        public ActionResult ClearCache()
        {
            cache.ClearCache();
            return RedirectToAction("index");
        }
        #endregion



    }
}