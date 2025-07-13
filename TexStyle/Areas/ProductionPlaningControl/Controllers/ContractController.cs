using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Common;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class ContractController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}