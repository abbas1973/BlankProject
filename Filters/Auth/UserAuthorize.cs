using DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.CookieServices;
using Services.RedisService;
using Services.SessionServices;

namespace Filters
{
    /// <summary>
    /// بررسی وجود کاربر درون سشن به منظور لاگین
    /// </summary>

    public class UserAuthorize : ActionFilterAttribute
    {
        private bool isPublic;
        private bool checkPasswordChange;
        private string area;
        private string controller;
        private string action;

        /// <summary>
        /// بررسی دسترسی کاربر
        /// </summary>
        /// <param name="IsPublic">آیا ادرس مورد نظر عمومی است و همه به آن دسترسی دارند؟</param>
        public UserAuthorize(bool IsPublic = false, string? Area = null, string? Controller = null, string? Action = null, bool CheckPasswordChange = true)
        {
            isPublic = IsPublic;
            checkPasswordChange = CheckPasswordChange;
            area = Area?.ToLower();
            controller = Controller?.ToLower();
            action = Action?.ToLower();
        }



        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var url = context.HttpContext.Request.Path.ToString()
                        + context.HttpContext.Request.QueryString;


            #region کاربر تست برای لاگین
            //var controller = context.Controller as Controller;
            //var User = new UserSessionDTO
            //{
            //    FullName = "عباس محمدنژاد",
            //    Id = 1,
            //    Mobile = "09359785415"
            //};
            //controller.HttpContext.Session.SetUser(User);
            #endregion


            #region اگر ادمین درون سشن نباشد کاربر به صفحه لاگین ریدایرکت میشود
            var controllerObj = context.Controller as Controller;
            var User = controllerObj?.HttpContext.Session.GetUser();
            if (User == null || User.Menus == null)
            {
                if (IsAjaxRequest(context))
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new EmptyResult();
                }
                else
                    context.Result = new RedirectToActionResult("Index", "Authentication", new { area = "", RetUrl = url });
                return;
            }
            #endregion


            #region اگر توکن کاربر با توکن درون ردیس یکی نباشد کاربر به صفحه لاگین منتقل می شود
            var cookieToken = controllerObj?.HttpContext.GetCookieUserToken();
            var Redis = controllerObj?.HttpContext.RequestServices.GetService<IRedisManager>();
            var redisToken = Redis.db.GetLoginToken(User.Id).Result;
            if (string.IsNullOrEmpty(cookieToken) || cookieToken != redisToken)
            {
                controllerObj?.HttpContext.Session.RemoveUser();
                context.Result = new RedirectToActionResult("Index", "Authentication", new { area = "", RetUrl = url });
            }
            #endregion


            #region بررسی دسترسی کاربر درون سشن به بخش مورد نظر
            //// آیا کاربر دسترسی لازم را دارد؟
            bool _HasPermission = isPublic ? true : HasPermission(context, User);


            if (!_HasPermission)
            {
                if (IsAjaxRequest(context))
                {
                    context.HttpContext.Response.StatusCode = 403;
                    context.Result = new EmptyResult();
                }
                else
                    context.Result = new RedirectToActionResult("Index", "Forbidden", new { area = "Admin" });
                return;
            }
            #endregion


            #region آیا کاربر تغییر رمز را انجام داده؟
            if (checkPasswordChange && !User.PasswordIsChanged)
            {
                context.Result = new RedirectToActionResult("ChangePassword", "Profile", new { area = "Admin" });
                return;
            }
            #endregion

            return;
        }






        #region بررسی دسترسی کاربر 
        public bool HasPermission(ActionExecutingContext context, UserSessionDTO User)
        {

            #region اگر دسترسی خاصی مد نظر نبود، تنها لاگین کاربر کافیست
            if (isPublic)
                return true;
            #endregion


            #region بررسی دسترسی به منو
            #region اگر پارامترهای ادرس مشخص نشده باشد، دسترسی کاربر به ادرس درخواست شده بررسی می شود
            var controllerObj = context.Controller as Controller;
            if (area == null)
            {
                var _area = controllerObj?.RouteData.Values["area"]?.ToString()?.ToLower();
                if (string.IsNullOrEmpty(_area))
                    _area = null;
                area = _area;
            }
            if (controller == null)
            {
                var _controller = controllerObj?.RouteData.Values["controller"]?.ToString()?.ToLower();
                controller = _controller;
            }
            if (action == null)
            {
                var _action = controllerObj?.RouteData.Values["action"]?.ToString()?.ToLower();
                action = _action;
            }
            #endregion

            var isExist = User.Menus.Any(x => x.Area == area && x.Controller == controller && x.Action == action);
            return isExist;
            #endregion
        }
        #endregion




        #region بررسی اینکه آیا درخواست از نوع اجکس است
        public bool IsAjaxRequest(ActionExecutingContext context)
        {
            string requestedWith = context.HttpContext.Request.Headers["X-Requested-With"];
            return requestedWith == "XMLHttpRequest";
        }
        #endregion



    }


}
