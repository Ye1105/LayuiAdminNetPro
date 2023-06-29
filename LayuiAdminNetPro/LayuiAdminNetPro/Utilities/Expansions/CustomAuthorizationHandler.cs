using CodeHelper.Common;
using LayuiAdminNetPro.Areas.Api.Controllers;
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
                if (BaseController.IsAjaxRequest(context.Request) || context.Request.Path.Value!.Contains("/api"))
                {
                    context.Response.StatusCode = (int)HttpStatus.OK;

                    await context.Response.WriteAsJsonAsync(BaseController.Res(status: HttpStatus.FORBIDDEN, msg: "权限不足", uimsg: "权限不足"));

                    return;
                }
                else
                {
                    //context.Response.Redirect($"login");
                    if (context.Request.Path.Value == "/")
                    {
                        context.Response.Redirect($"/login");
                        return;
                    }

                    var renderHtml = "<!DOCTYPE html><html>";
                    renderHtml += "<head>";
                    renderHtml += "<meta charset='utf-8'>";
                    renderHtml += "<title>403 权限不足</title>";
                    renderHtml += "<meta name='renderer' content ='webkit'>";
                    renderHtml += "<meta http-equiv='X-UA-Compatible' content ='IE=edge,chrome=1'>";
                    renderHtml += "<meta name='viewport' content='width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0' >";
                    renderHtml += "<link rel='stylesheet' href ='../layuiadmin/layui/css/layui.css' media='all' >";
                    renderHtml += "<link rel='stylesheet' href='../layuiadmin/style/admin.css' media='all'>";
                    renderHtml += "</head>";
                    renderHtml += "<body>";
                    renderHtml += "<div class='layui-fluid'>";
                    renderHtml += "<div class='layadmin-tips'>";
                    renderHtml += "<i class='layui-icon' face >&#xe664;</i> ";
                    renderHtml += "<div class='layui-text'> ";
                    renderHtml += "<h1><span class='layui-anim layui-font-32'> 权限不足 </span></h1>";
                    renderHtml += "</div> ";
                    renderHtml += "</div>";
                    renderHtml += "</div>";
                    renderHtml += "</body>";
                    renderHtml += "</html>";

                    await context.Response.WriteAsync(renderHtml);

                    //context.Response.Redirect($"/errors?status={HttpStatus.FORBIDDEN}");
                    return;
                }
            }

            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}