using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("accounts")]
    [Authorize(Policy = Policys.Admin)]
    public class AccountsController : ViewController
    {
        public override IActionResult Menu()
        {
            return View();
        }

        public override IActionResult Create()
        {
            return View();
        }

        public override IActionResult Edit()
        {
            var param = GetQueryString(HttpContext);
            return View(param);
        }

        [HttpGet("psd")]
        public IActionResult Psd()
        {
            var param = GetQueryString(HttpContext);
            return View(param);
        }
    }
}