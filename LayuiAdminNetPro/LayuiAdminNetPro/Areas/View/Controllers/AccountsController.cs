using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("accounts")]
    [Authorize(Policy = Policys.Admin)]
    public class AccountsController : ViewController
    {
        [HttpGet]
        public override IActionResult Index()
        {
            return View();
        }

        [HttpGet(_create)]
        public override IActionResult Create()
        {
            return View();
        }

        [HttpPatch(_patch)]
        public override IActionResult Patch()
        {
            return View();
        }
    }
}