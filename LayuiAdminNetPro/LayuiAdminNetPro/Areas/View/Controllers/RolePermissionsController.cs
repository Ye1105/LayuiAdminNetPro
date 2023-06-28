using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("rolepermissions")]
    [Authorize(Policy = Policys.Admin)]
    public class RolePermissionsController : ViewController
    {
        public override IActionResult Index()
        {
            var param = GetQueryString(HttpContext);
            return View(param);
        }
    }
}