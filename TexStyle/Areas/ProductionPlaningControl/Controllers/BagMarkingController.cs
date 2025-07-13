using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Infrastructure;
using TexStyle.Core.PPC;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class BagMarkingController : Controller {

        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(BagMarking)}";

        private readonly TempDataViewModel _tempData;
        private IBagMarkingService _bagMarkingService;
        private readonly IMapper _mapper;
        public BagMarkingController(IBagMarkingService bagMarkingService, IMapper mapper) {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(BagMarking)}";
            _tempData = new TempDataViewModel();
            _bagMarkingService = bagMarkingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            return View((await _bagMarkingService.GetAll()).ToList());
        }

        //[HttpGet]
        //public IActionResult Index([FromQuery] FilterOptions options) {
        //    var today = DateTime.Now;
        //    var startDate = new DateTime(today.Year, today.Month, 1);
        //    var endDate = startDate.AddMonths(1).AddDays(-1);
        //    if (!options.sd.HasValue || !options.ed.HasValue) {
        //        options.sd = startDate;
        //        options.ed = endDate;
        //    }
        //    ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
        //    return View(_bagMarkingService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            BagMarkingViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<BagMarkingViewModel>(await _bagMarkingService.GetById(id.Value));
            }
            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, BagMarkingViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<BagMarking>(vm);
                    if (!id.HasValue) {
                        // create
                        await _bagMarkingService.Create(m);
                    } else {
                        //update
                        await _bagMarkingService.Update(m);
                    }

                } catch (Exception ex) {
                    _tempData.Error = ex.Message;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col) {
            try {
                if (id.HasValue) {
                    await _bagMarkingService.Delete(await _bagMarkingService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            } catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }

    }
}
