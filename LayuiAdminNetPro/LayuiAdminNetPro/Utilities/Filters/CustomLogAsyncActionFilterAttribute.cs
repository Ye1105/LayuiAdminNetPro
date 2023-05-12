using CodeHelper.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LayuiAdminNetPro.Utilities.Filters
{
    //https://www.bilibili.com/video/BV1Vd4y1t7er/?p=35&spm_id_from=pageDriver&vd_source=874ef91701c817855be9727acd96b7cd
    /// <summary>
    /// log aop
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class CustomLogAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {

        public CustomLogAsyncActionFilterAttribute()
        {

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //包含了标记在当前要访问的Action上/当前Action所在的控制器上标记的所有的特性
            var meta = context.ActionDescriptor.EndpointMetadata;
            if (meta.Any(x => x.GetType().Equals(typeof(CustomAllowAnonymousAttribute))))
            {
                await next.Invoke();
                return;
            }

            var arguments = context.ActionArguments.SerObj();
            var controllerName = context.HttpContext.GetRouteValue("controller");
            var actionName = context.HttpContext.GetRouteValue("action");
            var path = context.HttpContext.Request.Path;

#if DEBUG
            Console.WriteLine("[OnActionExecuting]-[{0}]-[{1}]-[{2}]-[{3}]-[{4}]", controllerName, actionName, path, context.HttpContext.Request.ContentType, arguments);
#endif

            ActionExecutedContext excutedContext = await next.Invoke();  //此处执行Action
        }
    }
}
