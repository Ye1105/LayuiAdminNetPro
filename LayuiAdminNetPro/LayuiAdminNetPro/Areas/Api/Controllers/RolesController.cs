using AutoMapper;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Manager.Admin.Server.IServices;
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
        private readonly IAdminAccountRoleService _accrole;

        public RolesController(IAdminAccountService accountService, IAdminAccountRoleService adminAccountRoleService, IAdminRoleInfoService adminRoleInfoService, IMapper mapper)
        {
            _admin = accountService;
            _roleInfo = adminRoleInfoService;
            _accrole = adminAccountRoleService;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreateReq req)
        {
            /*
             * 1.判断 uId，rIds 是否都是有效 guid
             * 2.删除原有用户角色
             * 3.添加新用户角色
             */

            var account = await _admin.FirstOrDefaultAsync(u => u.UId == req.UId);
            if (account == null)
            {
                return Ok(Fail("用户不存在"));
            }

            if (req.RIds.Count > 0)
            {
                var list = await _roleInfo.QueryAsync(x => req.RIds.Contains(x.RId), false);
                if (list.Count != req.RIds.Count)
                {
                    return Ok(Fail("rIds 列表参数不正确"));
                }

                var accountRoles = new List<AdminAccountRole>();
                for (int i = 0; i < req.RIds.Count; i++)
                {
                    accountRoles.Add(new AdminAccountRole()
                    {
                        RId = req.RIds[i],
                        UId = req.UId
                    });
                }


            }





            return Ok(Success());
        }
    }
}