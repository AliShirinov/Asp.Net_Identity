﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HRApp.Areas.HR.Controllers
{
    public class HomeController : Controller
    {
        [Area("HR")]
        public IActionResult Index()
        {
            return View();
        }
    }
}