using CodeHelper.Common;
using LayuiAdminNetPro.Utilities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace LayuiAdminNetPro.Utilities.Expansions
{
    public class CustomAuthorizationHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            // If the authorization was forbidden and the resource had a specific requirement,
            // provide a custom 404 response
            var endpoint = context.GetEndpoint();
            var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            if (!authorizeResult.Succeeded)
            {
                if (ControllBase.IsAjaxRequest(context.Request) || context.Request.Path.Value!.Contains("/api"))
                {
                    context.Response.StatusCode = (int)HttpStatus.OK;
                    //new {    Status = HttpStatus.FORBIDDEN }
                    await context.Response.WriteAsJsonAsync(ControllBase.Res(status: HttpStatus.FORBIDDEN, msg: "权限不足", uimsg: "权限不足"));

                    return;
                }
                else
                {
                    context.Response.Redirect($"login");

                    //context.Response.Redirect($"/errors?status={HttpStatus.FORBIDDEN}");
                    return;
                }
            }

            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}