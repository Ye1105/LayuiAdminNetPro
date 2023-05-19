using CodeHelper.Common;
using CodeHelper.Common.Validator;
using LayuiAdminNetCore.Appsettings;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetGate.IServices;
using LayuiAdminNetPro.Utilities.Common;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    /// <summary>
    /// 登录管理
    /// </summary>
    [Route($"{nameof(Api)}/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class LoginController : ControllBase
    {
        private readonly IAdminAccountService _admin;
        private readonly IAuthenticateService _auth;
        private readonly IOptions<AppSettings> _appSettings;

        public LoginController(IAdminAccountService adminAccountService, IAuthenticateService authenticateService, IOptions<AppSettings> appSettings)
        {
            _admin = adminAccountService;
            _auth = authenticateService;
            _appSettings = appSettings;
        }

        /// <summary>
        /// 用户登录接口
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Signing([FromQuery] string phone, [FromQuery] string password)
        {
            /*
             * 1.参数校验
             * 2.验证码 短信校验  demo中不做具体实现
             * 3.账号校验
             * 4.JWT AccessToken
             * 5.返回 AccessToken
             */

            #region 参数校验

            #region 账号校验

            //var namePatterns = new[] { Rgx.RGX_SPECIAL_CHARACTER, Rgx.RGX_UNDERLING, Rgx.RGX_NUMBER };
            ////账号校验
            //var nameVerify = name.RegexVerify(namePatterns, () =>
            //  {
            //      foreach (var pattern in namePatterns)
            //      {
            //          if (Regex.IsMatch(name, pattern.Item1))
            //          {
            //              if (pattern == Rgx.RGX_SPECIAL_CHARACTER)
            //                  continue;
            //              return (false, pattern.Item2);
            //          }
            //          else
            //          {
            //              if (pattern == Rgx.RGX_SPECIAL_CHARACTER)
            //                  return (false, pattern.Item2);
            //          }
            //      }
            //      return (true, "");
            //  });
            //if (!nameVerify.Item1)
            //{
            //    return Ok(Fail(nameVerify.Item2));
            //}

            #endregion 账号校验

            //手机号校验
            var phonePatterns = new[] { Rgx.RGX_PHONE };
            var phoneVerify = phone.RegexVerify(phonePatterns, () =>
            {
                foreach (var pattern in phonePatterns)
                {
                    if (Regex.IsMatch(phone, pattern.Item1))
                        return (true, "");
                    else
                        return (false, pattern.Item2);
                }
                return (true, "");
            });
            if (!phoneVerify.Item1)
            {
                return Ok(Fail(phoneVerify.Item2));
            }

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

            if (!passwordVerify.Item1)
            {
                return Ok(Fail(passwordVerify.Item2));
            }

            #endregion 参数校验

            #region 账号校验

            var account = await _admin.FirstOrDefaultAsync(phone, password);
            if (account == null)
            {
                return Ok(Fail("手机号或者密码错误"));
            }

            /* 3.账号状态 */
            if (account.Status != (sbyte)Status.ENABLE)
            {
                return Ok(Fail($"账号状态:{EnumDescriptionAttribute.GetEnumDescription((Status)account.Status)}"));
            }

            #endregion 账号校验

            //4.AccessToken
            if (!_auth.IsAuthenticated(new Authenticate() { UId = account.UId, Phone = account.Phone }, out string accessToken))
            {
                return Ok(Fail("账号认证失败"));
            }

            return Ok(Success(new { accessToken }));
        }
    }
}