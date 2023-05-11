using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Controllers
{
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
        [HttpGet("welcome")]
        [Authorize(Policy = Policys.Admin)]
        public IActionResult Welcome()
        {
            return View();
        }


        [HttpGet("server")]
        [Authorize(Policy = Policys.Admin)]
        public IActionResult Server()
        {
            return View();
        }
    }
}