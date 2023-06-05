using CodeHelper.Common;
using LayuiAdminNetCore.Appsettings;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetGate.IServices;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    /// <summary>
    /// 登录管理
    /// </summary>
    [Route($"{nameof(Api)}/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class LoginController : BaseController
    {
        private readonly IAdminAccountService _admin;
        private readonly IAuthenticateService _auth;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginController(IAdminAccountService adminAccountService, IAuthenticateService authenticateService, IOptions<AppSettings> appSettings, IWebHostEnvironment webHostEnvironment)
        {
            _admin = adminAccountService;
            _auth = authenticateService;
            _appSettings = appSettings;
            _webHostEnvironment = webHostEnvironment;
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

            var signinSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "signin");

            var sign = new
            {
                phone,
                password
            };

            var psdValidate = JObject.Parse(sign.SerObj()).IsValid(JSchema.Parse(signinSchema), out IList<string> errorMessages);
            if (!psdValidate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
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