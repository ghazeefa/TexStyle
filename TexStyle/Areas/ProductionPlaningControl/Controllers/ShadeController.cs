using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class ShadeController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(Shade)}";

        private readonly TempDataViewModel _tempData;
        private IShadeService _shadeService;
        private readonly IMapper _mapper;
        public ShadeController(IShadeService shadeService, IMapper mapper) {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(Shade)}";
            _tempData = new TempDataViewModel();
            _shadeService = shadeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            return View((await _shadeService.GetAll()).ToList());
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
        //    return View(_shadeService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            ShadeViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<ShadeViewModel>(await _shadeService.GetById(id.Value));
            }
            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, ShadeViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<Shade>(vm);
                    if (!id.HasValue) {
                        // create
                        await _shadeService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update
                        await _shadeService.Update(m);
                        _tempData.MSG = "Successfully Updated";
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
                    await _shadeService.Delete(await _shadeService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            } catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
    }
}