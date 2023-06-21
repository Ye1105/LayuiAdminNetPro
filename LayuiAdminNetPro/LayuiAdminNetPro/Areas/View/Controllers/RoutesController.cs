using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Route("routes")]
    [Authorize(Policy = Policys.Admin)]
    public class RoutesController : ViewController
    {
        public override IActionResult Index()
        {
            var param = GetQueryString(HttpContext);
            return View(param);
        }

        public override IActionResult Paged()
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
    }
}