using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("persons")]
    [Authorize(Policy = Policys.Admin)]
    public class PersonsController : ViewController
    {
        /// <summary>
        /// 个人信息界面
        /// </summary>
        /// <returns></returns>
        public override IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 修改密码界面
        /// </summary>
        /// <returns></returns>
        [HttpGet("psd")]
        public IActionResult Psd()
        {
            return View();
        }
    }
}