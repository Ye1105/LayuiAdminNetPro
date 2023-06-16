using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("roles")]
    [Authorize(Policy = Policys.Admin)]
    public class RolesController : ViewController
    {
        public override IActionResult Index()
        {
            var param = GetQueryString(HttpContext);
            return View(param);
        }
    }
}