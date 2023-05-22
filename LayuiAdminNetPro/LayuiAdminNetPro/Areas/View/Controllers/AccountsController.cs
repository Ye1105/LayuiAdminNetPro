using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetPro.Areas.View.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("[controller]")]
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
    }
}