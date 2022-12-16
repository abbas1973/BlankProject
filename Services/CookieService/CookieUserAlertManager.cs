using Microsoft.AspNetCore.Http;

namespace Services.CookieServices
{
    /// <summary>
    /// ست کردن توکن کاربر در کوکی برای نمایش اطلاعات هنگام لاگین 
    /// </summary>
    public static class CookieUserAlertManager
    {
        public static readonly string Key = "UserAlert";

        /// <summary>
        /// گرفتن توکن کاربر
        /// </summary>
        /// <returns></returns>
        public static string GetCookieUserAlert(this HttpContext HttpContext)
        {
            try
            {
                return HttpContext?.Request?.Cookies?.Get(Key);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// افزودن توکن کاربر به کوکی
        /// </summary>
        /// <returns></returns>
        public static bool SetCookieUserAlert(this HttpContext HttpContext, string Token)
        {
            try
            {
                var ResponseCookies = HttpContext?.Response?.Cookies;
                ResponseCookies?.Set(Key, Token, HttpOnly: true);                
                return true;
            }
            catch
            {
                return false;
            }
        }




        /// <summary>
        /// حذف توکن کاربر از کوکی
        /// </summary>
        public static bool RemoveCookieUserAlert(this HttpContext HttpContext)
        {
            try
            {
                HttpContext?.RemoveCookie(Key);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
