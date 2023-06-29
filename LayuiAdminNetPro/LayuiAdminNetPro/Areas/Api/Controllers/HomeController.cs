using AutoMapper;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Manager.Admin.Server.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    [Route($"{nameof(Api)}/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class HomeController : BaseController
    {
        private readonly IAdminAccountRoleService _adminAccountRoleService;
        private readonly IAdminRolePermissionService _adminRolePermissionService;
        private readonly IAdminAccountService _adminAccountService;
        private readonly IMapper _mapper;

        public HomeController(IAdminAccountRoleService adminAccountRoleService, IAdminRolePermissionService adminRolePermissionService, IAdminAccountService adminAccountService, IMapper mapper)
        {
            _adminAccountRoleService = adminAccountRoleService;
            _adminRolePermissionService = adminRolePermissionService;
            _adminAccountService = adminAccountService;
            _mapper = mapper;
        }

        #region Get

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //获取当前登陆用户的 账号信息 include 角色信息 | 菜单路由 信息

            var uid = base.UId;

            if (uid == Guid.Empty)
            {
                return Ok(Fail("未获取到用户信息"));
            }

            var account = await _adminAccountService.FirstOrDefaultAsync(x => x.UId == uid, isIncludeAccountRoles: true, isTrack: false);
            var dtoAccount = _mapper.Map<DtoAdminAccount>(account);

            List<Guid>? rIds = account?.AdminAccountRoles?.Select(x => x.RId).ToList();

            if (rIds != null && rIds.Any())
            {
                var menus = await _adminRolePermissionService.QueryAsync(
                      x => rIds.Contains(x.RId) && x.CId == (sbyte)CrudType.READ,
                      isInculdeModuleInfo: true,
                      isTrack: false
                 );

                return Ok(Success(new { account = dtoAccount, menus }));
            }
            else
            {
                return Ok(Success(new { account = dtoAccount, menus = new List<AdminRolePermission>() { } }));
            }
        }

        #endregion Get
    }
}