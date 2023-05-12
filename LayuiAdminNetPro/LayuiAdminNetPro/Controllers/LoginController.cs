using LayuiAdminNetPro.Utilities.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Controllers
{
    /// <summary>
    /// 登录管理
    /// </summary>
    [Route("login")]
    [ApiController]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class LoginController : Controller
    {
        /// <summary>
        /// 登录界面视图渲染
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            //DeleteCookies(".AspNetCore.Token");
            return View();
        }

        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("{name}/{password}")]
        public IActionResult SignIn(string name, string password)
        {
            /*
             * 1.自定义参数校验    
             * 2.验证码 短信校验  demo中不做具体实现
             * 3.账号校验
             * 4.JWT AccessToken
             * 5.将 accessToken 保存至 cookie 中
             */



            return Ok();
        }

    }
}
