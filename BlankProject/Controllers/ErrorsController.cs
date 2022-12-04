using DTO.Base;
using DTO.ExceptionHandling;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlankProject.Controllers
{

    /// <summary>
    /// مدیریت خطاها و تولید خروجی مناسب با استاتوس کد مناسب
    /// </summary>
    public class ErrorsController : Controller
    {
        private readonly string MainSiteUrl;
        public ErrorsController(IConfiguration iConfig)
        {
            MainSiteUrl = iConfig.GetSection("MainSiteUrl").Value?.Trim();
        }


        [HttpGet]
        [Route("error")]
        public IActionResult GetError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = 500;
            string title = "خطا";

            if (exception is HttpStatusException httpException)
            {
                code = (int)httpException.Status;
                title = httpException.Title;
            }
            
            Response.StatusCode = code;

            ViewBag.MainSiteUrl = MainSiteUrl;
            // اگر درخواست از نوع اجکس بود
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new BaseResult(false, exception.Message));
            else // درخواست غیر اجکس
                return View("ErrorPage", new ErrorPageDTO(code.ToString(), title, exception.Message));
        }



        [HttpPost]
        [Route("error")]
        public IActionResult PostError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = 500;
            string title = "خطا";

            if (exception is HttpStatusException httpException)
            {
                code = (int)httpException.Status;
                title = httpException.Title;
            }

            Response.StatusCode = code;
            
            ViewBag.MainSiteUrl = MainSiteUrl;
            // اگر درخواست از نوع اجکس بود
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Json(new BaseResult(false, exception.Message));
            else // درخواست غیر اجکس
                return View("ErrorPage", new ErrorPageDTO(code.ToString(), title, exception.Message));
        }

    }
}