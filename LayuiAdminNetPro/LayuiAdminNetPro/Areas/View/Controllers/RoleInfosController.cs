﻿using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Route("roleinfos")]
    [Authorize(Policy = Policys.Admin)]
    public class RoleInfosController : ViewController
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