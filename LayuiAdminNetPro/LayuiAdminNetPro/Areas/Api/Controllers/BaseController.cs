using CodeHelper.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    public class BaseController : Controller
    {
        //https://learn.microsoft.com/zh-cn/dotnet/csharp/properties 属性用法
        /// <summary>
        /// Attribute => 获取当前登陆用户的信息
        /// </summary>
        protected Dictionary<string, string>? UserClaims
        {
            get
            {
                var dicClaims = new Dictionary<string, string>();
                var claims = HttpContext.User?.Claims.AsEnumerable();
                if (claims is not null && claims.Any())
                {
                    foreach (var item in claims?.AsEnumerable() ?? new List<Claim>())
                    {
                        var k = item.Type;
                        var v = item.Value;
                        dicClaims[k] = v;
                    }
                }
                return dicClaims;
            }
        }

        /// <summary>
        /// Attribute => 获取当前登陆用户 uId
        /// </summary>
        protected Guid UId
        {
            get
            {
                var claims = HttpContext.User?.Claims.AsEnumerable();
                if (claims is not null && claims.Any())
                {
                    foreach (var item in claims?.AsEnumerable() ?? new List<Claim>())
                    {
                        if (item.Type == "uId")
                        {
                            return Guid.Parse(item.Value);
                        }
                    }
                    return Guid.Empty;
                }
                else
                    return Guid.Empty;
            }
        }

        #region Requst

        /// <summary>
        /// 判定 HttpRequest 是否是 Ajax 请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(HttpRequest request)
        {
            string? header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }

        /// <summary>
        /// 将 Context.Body 中的请求参数转为 json 字符串
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected string? GetRequestBody(HttpContext context)
        {
            //操作Request.Body之前加上EnableBuffering即可
            context.Request.EnableBuffering();

            var stream = new StreamReader(context.Request.Body);
            string body = stream.ReadToEndAsync().GetAwaiter().GetResult();

            //重置 position 的位置
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            return body;
        }

        #endregion Requst

        #region Reponse

        public static ResponseModel Res(HttpStatus status, string msg = "", string uimsg = "", object? data = null)
        {
            var res = new ResponseModel()
            {
                Status = status,
                Msg = msg,
                Uimsg = uimsg,
                Data = data
            };
            return res;
        }

        public static ResponseModel Success(string msg, string uimsg, object? data = null)
        {
            var res = new ResponseModel()
            {
                Status = HttpStatus.OK,
                Msg = msg,
                Uimsg = uimsg,
                Data = data
            };
            return res;
        }

        public static ResponseModel Success(string detail, object? data = null)
        {
            var res = new ResponseModel()
            {
                Status = HttpStatus.OK,
                Msg = detail,
                Uimsg = detail,
                Data = data
            };
            return res;
        }

        public static ResponseModel Success(object? data = null)
        {
            var res = new ResponseModel()
            {
                Status = HttpStatus.OK,
                Msg = "",
                Uimsg = "",
                Data = data
            };
            return res;
        }

        public static ResponseModel Fail(object? errors = null, string? msg = "", string? uimsg = "")
        {
            var res = new ResponseModel()
            {
                Status = HttpStatus.BAD_REQUEST,
                Msg = msg,
                Uimsg = uimsg,
                Errors = errors
            };
            return res;
        }

        public static ResponseModel Fail(object? errors = null, string? detail = "")
        {
            var res = new ResponseModel()
            {
                Status = HttpStatus.BAD_REQUEST,
                Msg = detail,
                Uimsg = detail,
                Errors = errors
            };
            return res;
        }

        public static ResponseModel Fail(string? msg = "", string? uimsg = "")
        {
            var res = new ResponseModel()
            {
                Status = HttpStatus.BAD_REQUEST,
                Msg = msg,
                Uimsg = uimsg,
            };
            return res;
        }

        public static ResponseModel Fail(string? detail = "")
        {
            var res = new ResponseModel()
            {
                Status = HttpStatus.BAD_REQUEST,
                Msg = detail,
                Uimsg = detail,
            };
            return res;
        }

        public static ResponseModel Fail()
        {
            var res = new ResponseModel()
            {
                Status = HttpStatus.BAD_REQUEST,
                Msg = "",
                Uimsg = "",
            };
            return res;
        }

        #endregion Reponse

        #region Cookie

        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        protected void DeleteCookies(string key)
        {
            HttpContext.Response.Cookies.Delete(key);
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string? value);
            if (string.IsNullOrWhiteSpace(value))
                value = string.Empty;
            return value;
        }

        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">过期时长，单位：天</param>
        protected void SetCookies(string key, string value, int days = 7)
        {
            HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(days),
                IsEssential = true
            });
        }

        #endregion Cookie
    }

    /// <summary>
    /// 返回数据实体类
    /// </summary>
    public class ResponseModel
    {
        [JsonProperty("status")]
        public HttpStatus Status { get; set; }

        [JsonProperty("errors")]
        public object? Errors { get; set; }

        [JsonProperty("data")]
        public object? Data { get; set; }

        [JsonProperty("msg")]
        public string? Msg { get; set; } = "";

        [JsonProperty("uimsg")]
        public string? Uimsg { get; set; } = "";
    }
}