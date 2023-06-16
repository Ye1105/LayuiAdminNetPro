using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("roleinfos")]
    [Authorize(Policy = Policys.Admin)]
    public class RoleInfosController : ViewController
    {
        public override IActionResult Paged()
        {
            return View();
        }

        public override IActionResult Create()
        {
            return View();
        }
    }
}