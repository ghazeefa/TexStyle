using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Common;
using TexStyle.ViewModels;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class ChemicalIssuanceRecipeTrDetailController : Controller {
        public IActionResult Index() {
            return View();
        }
        [HttpGet]
        public IActionResult Index([FromQuery] FilterOptions options) {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue) {
                options.sd = startDate;
                options.ed = endDate;
            }
            throw new NotImplementedException();
            //return View(_uow.DyesChemicalOpenningDetailService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        }

    }
}