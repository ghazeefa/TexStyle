using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Common;

namespace TexStyle.Areas.Gate {
    [Area(AreaConstants.GATE.Name)]
    public class HomeController : Controller {
        [ViewData]
        public string AreaName { get; set; }

        public HomeController() {
            AreaName = $"{AreaConstants.GATE.NormalizedName} / Home";
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult AddOrUpdate(long? id) {
            return View();
        }

        [HttpPost]
        public IActionResult AddOrUpdate(long? id, IFormCollection col) {
            return View();
        }
    }
}