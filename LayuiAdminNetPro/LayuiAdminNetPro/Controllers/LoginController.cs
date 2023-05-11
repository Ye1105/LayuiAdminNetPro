using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Controllers
{
    /// <summary>
    /// 登录管理
    /// </summary>
    [Route("login")]
    [ApiController]
    public class LoginController : Controller
    {
        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            //DeleteCookies(".AspNetCore.Token");
            return View();
        }




    }
}
