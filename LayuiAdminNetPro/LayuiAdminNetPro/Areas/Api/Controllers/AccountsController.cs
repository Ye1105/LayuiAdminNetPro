using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetPro.Utilities.Common;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetServer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = Policys.Admin)]
    [Route($"{nameof(Api)}/[controller]")]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class AccountsController : ControllBase
    {
        private readonly IAdminAccountService _admin;

        public AccountsController(IAdminAccountService adminAccountService)
        {
            _admin = adminAccountService;
        }

        /// <summary>
        /// 单个账号
        /// </summary>
        /// <returns></returns>
        [Route($"{nameof(Api)}/[controller]")]
        [HttpGet("{uId:guid}")]
        public async Task<IActionResult> Single(Guid uId)
        {
            var account = await _admin.FirstOrDefaultAsync(x => x.UId == uId);

            return Ok(Success(new { account }));
        }


        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AccountPagedRequest req)
        {


            return Ok(Success());
        }
    }
}
