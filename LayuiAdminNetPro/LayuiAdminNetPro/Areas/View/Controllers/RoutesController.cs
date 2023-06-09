﻿using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    /// <summary>
    /// 路由配置管理
    /// </summary>
    [Route("routes")]
    [Authorize(Policy = Policys.Admin)]
    public class RoutesController : ViewController
    {
        public override IActionResult Menu()
        {
            return View();
        }

        public override IActionResult Create()
        {
            return View();
        }

        public override IActionResult Edit()
        {
            var param = GetQueryString(HttpContext);
            return View(param);
        }
    }
}