using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetPro.Utilities.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class HomeController : Controller
    {
        /// <summary>
        /// 框架主界面
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        [Authorize(Policy = Policys.Admin)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 欢迎界面【公告、项目版本等展示】
        /// </summary>
        /// <returns></returns>
        //[Route(nameof(Areas.View))]
        [HttpGet("welcome")]
        [Authorize(Policy = Policys.Admin)]
        public IActionResult Welcome()
        {
            return View();
        }
    }
}