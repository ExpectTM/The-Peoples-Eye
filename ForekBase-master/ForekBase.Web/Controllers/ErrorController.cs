using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ForekBase.Web.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;

            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;

            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            TempData["error"] = $"Error: {exceptionHandlerPathFeature.Error.Message}";

            return View("Error");
        }
    }
}
