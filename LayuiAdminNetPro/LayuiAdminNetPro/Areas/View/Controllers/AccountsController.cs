using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    //[Route($"{nameof(Areas.View)}/accounts")]
    [Route("[controller]")]
    [Authorize(Policy = Policys.Admin)]
    public class AccountsController : Controller
    {
        /// <summary>
        /// 账号列表界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}