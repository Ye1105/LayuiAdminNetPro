using AutoMapper;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetPro.Utilities.Common;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
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
        private readonly IMapper _mapper;

        public AccountsController(IAdminAccountService adminAccountService, IMapper mapper)
        {
            _admin = adminAccountService;
            _mapper = mapper;
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

        /// <summary>
        /// 账号列表【分页】
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AccountPagedRequest req)
        {
            var list = await _admin.QueryPagedAsync(req);

            var JsonData = new
            {
                list.TotalPages,
                list.CurrentPage,
                list.PageSize,
                list.TotalCount,
                list = _mapper.Map<IEnumerable<DtoAdminAccount>>(list)
            };

            return Ok(Success(JsonData));
        }
    }
}
