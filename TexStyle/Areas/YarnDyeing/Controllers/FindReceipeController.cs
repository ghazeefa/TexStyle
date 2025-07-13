using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.ViewModels;

namespace TexStyle.Areas.YarnDyeing.Controllers
{
    [Area(AreaConstants.YARN_DYEING.Name)]
    public class FindReceipeController : Controller
    {
        private readonly TempDataViewModel _tempData;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public FindReceipeController(IUnitOfWork uow, IMapper mapper)
        {
            _tempData = new TempDataViewModel();
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.RecipeService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        }
    }
}