﻿using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetGate.IServices;
using LayuiAdminNetGate.Services;
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
            this._roleModulePermissionService = roleModulePermissionService;
            this._memoryCache = memoryCache;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            try
            {
                HttpContext? httpContext = context.Resource as HttpContext;

                var path = httpContext?.Request.Path;

                //context?.Succeed(requirement);
                bool? isAuthenticated = context?.User?.Identity?.IsAuthenticated;

                var claimssd = httpContext?.User?.Claims;

                if (isAuthenticated != null && isAuthenticated.Value)
                {
                    var claims = httpContext?.User?.Claims;

                    var dicClaims = new Dictionary<string, string>();

                    foreach (var item in claims?.AsEnumerable() ?? new List<Claim>())
                    {
                        var type = item.Type;
                        var value = item.Value;
                        dicClaims[type] = value;
                    }
                    if (dicClaims is not null && dicClaims.Any())
                    {
                        //校验用户状态
                        if (path == "/")
                        {

                            context?.Succeed(requirement);
                        }
                    }
                }

                context?.Fail();
                return;
            }
            catch
            {
                return;
            }
        }
    }
}