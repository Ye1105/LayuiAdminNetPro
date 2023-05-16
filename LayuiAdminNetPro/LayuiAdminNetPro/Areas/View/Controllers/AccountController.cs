using LayuiAdminNetCore.AdminPages;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetServer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    //[Route($"{nameof(Areas.View)}/account")]
    [Route("account")]
    [Authorize(Policy = Policys.Admin)]
    public class AccountController : Controller
    {
        private readonly IAdminAccountService _acc;

        public AccountController(IAdminAccountService accountService)
        {
            _acc = accountService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] AccountPagedParams req)
        {
            //req.IsAutoLoading = 1;

            //var pagedList = req.IsAutoLoading > 0 ? _acc.GetPageList(req, isTrack: false) : null;

            //(AccountPagedParams pagedParams, IPagedList<AdminAccount>? pagedList) data = (req, pagedList);

            //return View(data);

            return View();
        }
    }
}