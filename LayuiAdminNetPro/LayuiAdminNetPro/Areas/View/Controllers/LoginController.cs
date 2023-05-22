﻿using LayuiAdminNetPro.Areas.View.Common;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    /// <summary>
    /// 登录模块
    /// </summary>
    [Route("[controller]")]
    public class LoginController : ViewController
    {
        /// <summary>
        /// 登录界面视图渲染
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override IActionResult Index()
        {
            return View();
        }
    }
}