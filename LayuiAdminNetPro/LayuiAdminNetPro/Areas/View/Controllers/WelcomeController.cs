using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("welcome")]
    [Authorize(Policy = Policys.Admin)]
    public class WelcomeController : ViewController
    {
        /// <summary>
        /// 欢迎界面【公告、项目版本等展示】
        /// </summary>
        /// <returns></returns>
        //[Route(nameof(Areas.View))]
        public override IActionResult Menu()
        {
            return View();
        }
    }
}