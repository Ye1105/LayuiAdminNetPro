﻿using LayuiAdminNetCore.AuthorizationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    [Authorize(Policy = Policys.Admin)]
    public class HomeController : Controller
    {
        /// <summary>
        /// 框架主界面
        /// </summary>
        /// <returns></returns>
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}