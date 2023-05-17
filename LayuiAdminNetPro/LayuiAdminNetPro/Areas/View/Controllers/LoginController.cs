using LayuiAdminNetPro.Utilities.Common;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    /// <summary>
    /// 登录模块
    /// </summary>
    [Route("login")]
    public class LoginController : ControllBase
    {
        /// <summary>
        /// 登录界面视图渲染
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}