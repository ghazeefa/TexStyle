using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.Common;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    //[Route(AreaConstants.PRODUCTION_PLANING_CONTROL.RouteString)]
    public class HomeController : Controller {
        [ViewData]
        public string AreaName { get; set; }

        public HomeController() {
            AreaName  = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / Home";
        }
        
        [HttpGet]
        public async Task<IActionResult> Index() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            return View();   
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id,IFormCollection col) {
            return View();
        }

    }
}