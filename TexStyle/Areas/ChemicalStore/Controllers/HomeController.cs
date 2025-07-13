using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Common;
using TexStyle.Infrastructure;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    //[Route(AreaConstants.CHEMICAL_STORE.RouteString)]
    public class HomeController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        public HomeController() {
            AreaName = AreaConstants.CHEMICAL_STORE.NormalizedName;
        }

        public IActionResult Index() {
            return View();
        }

    }
}