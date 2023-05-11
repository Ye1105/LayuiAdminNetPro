using CodeHelper.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Controllers;
using CodeHelper.Common;

namespace LayuiAdminNetPro.Utilities.Middlewares
{
    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
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
                if (context.Request.IsAjaxRequest())
                {

                    context.Response.StatusCode = (int)HttpStatus.OK;
                    await context.Response.WriteAsJsonAsync(new { Status = HttpStatus.FORBIDDEN });
                    //await context.ForbidAsync();

                    return;
                }
                else
                {


                    context.Response.Redirect($"/login");

                    //context.Response.Redirect($"/errors?status={HttpStatus.FORBIDDEN}");
                    return;
                }
            }

            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
