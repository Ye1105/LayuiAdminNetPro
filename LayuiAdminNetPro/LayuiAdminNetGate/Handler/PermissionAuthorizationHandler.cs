using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetGate.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace LayuiAdminNetGate.Handler
{
    /// <summary>
    ///  https://blog.csdn.net/qq_25086397/article/details/103765090  自定义策略博客
    ///  https://docs.microsoft.com/zh-cn/aspnet/core/security/authorization/policies?view=aspnetcore-6.0  微软官网文档
    /// </summary>
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IRoleModulePermissionService _roleModulePermissionService;
        private readonly IMemoryCache _memoryCache;

        public PermissionAuthorizationHandler(IRoleModulePermissionService roleModulePermissionService, IMemoryCache memoryCache)
        {
            _roleModulePermissionService = roleModulePermissionService;
            _memoryCache = memoryCache;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //是否需要用 lock(this) 线程锁定 ？
            try
            {
                HttpContext? httpContext = context.Resource as HttpContext;

                var path = httpContext?.Request.Path;

                //context?.Succeed(requirement);
                bool? isAuthenticated = context?.User?.Identity?.IsAuthenticated;

                var claims = httpContext?.User?.Claims;

                if (isAuthenticated != null && isAuthenticated.Value)
                {

                    var dicClaims = new Dictionary<string, string>();

                    foreach (var item in claims?.AsEnumerable() ?? new List<Claim>())
                    {
                        var type = item.Type;
                        var value = item.Value;
                        dicClaims[type] = value;
                    }
                    if (dicClaims is not null && dicClaims.Any())
                    {
                        /* 
                         * 赋值用户的userClaims
                         * 获取用户的账号状态 [是否处于启用状态]
                         * 获取路由 和 用户的请求方式  get post put patch delete
                         * 如果请求的是 主页 ，直接返回 
                         * 否则 获取所有用户的角色和菜单列表 ， 校验用户是否有当前路由的权限
                         *  
                         */

                        var uId = Guid.Parse(dicClaims["uId"]);
                        var account = await _roleModulePermissionService.AccountFirstOrDefaultAsync(x => x.UId == uId, isTrack: false);
                        if (account is null || account.Status is not (sbyte)Status.ENABLE)
                        {
                            context?.Fail();
                            return;
                        }
                        //主页
                        if (path == "/")
                        {

                            //记录用户的最近一次登录时间和登录Ip
                            var key = $"LoginFrequency_{uId}";
                            var res = _memoryCache.Get(key);
                            if (res is null)
                            {
                                if (account is not null)
                                {
                                    account.LastLoginTime = DateTime.Now;
                                    account.LastLoginIp = httpContext?.Connection?.RemoteIpAddress?.ToString();
                                    await _roleModulePermissionService.ModifyAccountAsync(account);
                                }
                                //1分钟后过期
                                _memoryCache.Set(key, key, DateTime.Now.AddMinutes(1)
                                );
                            }

                            context?.Succeed(requirement);
                            return;
                        }
                        else
                        {

                            /*
                             * 1. 获取角色和接口列表， MemoryCache 缓存优化性能 
                             * 2. 当前用户的状态
                             * 3. 当前用户的角色
                             * 4. 当前用户的角色对应有权限的的所有 module
                             *
                             */

                            //获取当前的用户角色列表
                            var aR = await _roleModulePermissionService.GetAccountRolesAsync(x => x.UId == Guid.Parse(dicClaims["uId"]), false);
                            if (aR == null || !aR.Any())
                            {
                                context?.Fail();
                                return;
                            }

                            //当前用户的角色 RId 列表
                            var rList = aR.Select(x => x.RId).ToList();

                            //获取 admin_role_permission 和 admin_module_info 列表
                            var pR = await _roleModulePermissionService.GetRolePermissionAsync();

                            if (pR == null || pR.AdminRolePermissions == null || pR.AdminRoutes == null)
                            {
                                context?.Fail();
                                return;
                            }

                            //获取当前用户的请求方式

                            var contentType = httpContext?.Request.Method.ToLower();

                            //restful 请求方式 对应 crud 用户的操作行为

                            var cId = CrudType.UNKNOWN;

                            switch (contentType)
                            {
                                case "post":
                                    cId = CrudType.CREATE;
                                    break;

                                case "get":
                                    cId = CrudType.READ;
                                    break;

                                case "put" or "patch":
                                    cId = CrudType.UPDATE;
                                    break;

                                case "delete":
                                    cId = CrudType.DELETE;
                                    break;

                                default:
                                    break;
                            }

                            if (cId == CrudType.UNKNOWN)
                            {
                                context?.Fail();
                                return;
                            }

                            var p = path!.Value.ToString().ToLower();

                            var res = (from r in pR.AdminRolePermissions
                                       join m in pR.AdminRoutes
                                       on r.MId equals m.Id
                                       where
                                         rList.Contains(r.RId)
                                         && p.Contains(m.Route!) && m.Route != "/"
                                         && r.CId == (sbyte)cId
                                       select r).ToList();


                            if (res is null || !res.Any())
                            {
                                context?.Fail();
                                return;
                            }


                            context?.Succeed(requirement);
                            return;
                        }
                    }
                }

                context?.Fail();
                return;
            }
            catch
            {
                context?.Fail();
                return;
            }
        }
    }
}