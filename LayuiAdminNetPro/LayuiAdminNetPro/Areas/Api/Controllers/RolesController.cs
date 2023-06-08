using AutoMapper;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    [ApiController]
    [Authorize(Policy = Policys.Admin)]
    [Route($"{nameof(Api)}/[controller]")]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class RolesController : BaseController
    {
        private readonly IAdminAccountService _admin;
        private readonly IAdminRoleInfoService _roleInfo;
        private readonly IMapper _mapper;

        public RolesController(IAdminAccountService accountService, IAdminRoleInfoService adminRoleInfoService, IMapper mapper)
        {
            _admin = accountService;
            _roleInfo = adminRoleInfoService;
            _mapper = mapper;
        }

        /// <summary>
        /// 用户角色关系表
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Guid uId)
        {
            /*
             * 1.获取当前用户角色列表
             * 2.获取整个账号角色关系列表
             *
             */

            var account = await _admin.FirstOrDefaultAsync(u => u.UId == uId, isIncludeAccountRoles: true, isTrack: false);

            var dtoAccount = _mapper.Map<DtoAdminAccount>(account);

            var roleInfos = await _roleInfo.QueryAsync(x => true, false);

            return Ok(Success(new { account = dtoAccount, roleInfos }));
        }
    }
}