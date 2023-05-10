using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetGate.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

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
            this._roleModulePermissionService = roleModulePermissionService;
            this._memoryCache = memoryCache;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            try
            {
                HttpContext? httpContext = context.Resource as HttpContext;

                var path = httpContext?.Request.Path;

                context?.Succeed(requirement);
                return;
            }
            catch
            {
                return;
            }
        }
    }
}