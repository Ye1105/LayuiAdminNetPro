﻿using CodeHelper.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LayuiAdminNetPro.Utilities.Common
{
    public class ControllBase : Controller
    {
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

        #endregion Reponse
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