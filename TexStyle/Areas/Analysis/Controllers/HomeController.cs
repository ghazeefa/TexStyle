using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Common;

namespace TexStyle.Areas.Analysis.Controllers {
    [Area(AreaConstants.Analysis.Name)]

    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}