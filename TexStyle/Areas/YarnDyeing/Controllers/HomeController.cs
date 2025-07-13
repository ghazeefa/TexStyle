using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Infrastructure;
using TexStyle.Common;

namespace TexStyle.Areas.YarnDyeing.Controllers {
    [Area(AreaConstants.YARN_DYEING.Name)]
    //[Route(AreaConstants.YARN_DYEING.RouteString)]
    public class HomeController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        public HomeController() {
            AreaName = AreaConstants.YARN_DYEING.NormalizedName;
        }

        public IActionResult Index() {
            return View();
        }
    }
}