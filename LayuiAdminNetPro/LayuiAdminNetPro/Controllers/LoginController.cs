using LayuiAdminNetPro.Utilities.Filters;
using CodeHelper.Common.Validator;
using Microsoft.AspNetCore.Mvc;
using CodeHelper.Common;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Reflection.Metadata.Ecma335;
using LayuiAdminNetServer.IServices;

namespace LayuiAdminNetPro.Controllers
{
    /// <summary>
    /// 登录管理
    /// </summary>
    [Route("login")]
    [ApiController]
    //[TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class LoginController : Controller
    {
        private readonly IAdminAccountService _admin;

        public LoginController(IAdminAccountService adminAccountService)
        {
            _admin = adminAccountService;
        }

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
        public async Task<IActionResult> SignIn(string name, string password)
        {
            /*
             * 1.参数校验    
             * 2.验证码 短信校验  demo中不做具体实现
             * 3.账号校验
             * 4.JWT AccessToken
             * 5.返回 accessToken 
             */

            #region 参数校验

            var namePatterns = new[] { Rgx.RGX_SPECIAL_CHARACTER, Rgx.RGX_UNDERLING, Rgx.RGX_NUMBER };
            //账号校验
            var nameVerify = name.RegexVerify(namePatterns, () =>
              {
                  foreach (var pattern in namePatterns)
                  {
                      if (Regex.IsMatch(name, pattern.Item1))
                      {
                          if (pattern == Rgx.RGX_SPECIAL_CHARACTER)
                              continue;
                          return (false, pattern.Item2);
                      }
                      else
                      {
                          if (pattern == Rgx.RGX_SPECIAL_CHARACTER)
                              return (false, pattern.Item2);
                      }
                  }
                  return (true, "");
              });

            //密码校验
            var passwordPatterns = new[] { Rgx.RGX_PASSWORD_LENGTH };
            var passwordVerify = password.RegexVerify(passwordPatterns, () =>
            {
                foreach (var pattern in passwordPatterns)
                {
                    if (Regex.IsMatch(password, pattern.Item1))
                        return (true, "");
                    else
                        return (false, pattern.Item2);
                }
                return (true, "");
            });

            #endregion

            #region 账号校验

            var account = await _admin.FirstOrDefaultAsync(name, password);



            #endregion


            return Ok();
        }
    }
}
