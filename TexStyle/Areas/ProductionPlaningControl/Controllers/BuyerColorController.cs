using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class BuyerColorController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(BuyerColor)}";

        private readonly TempDataViewModel _tempData;
        private readonly IBuyerColorService _buyerColorService;
        private readonly IBuyerService _buyerService;
        private readonly IMapper _mapper;
        public BuyerColorController(IBuyerColorService buyerColorService, IBuyerService buyerService, IMapper mapper) {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(BuyerColor)}";
            _tempData = new TempDataViewModel();
            _buyerColorService = buyerColorService;
            _buyerService = buyerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            return View(await _buyerColorService.GetAll());
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
        //    return View(_buyerColorService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            BuyerColorViewModel vm = null;
            List<SelectListItem> buyersList =(await _buyerService.GetAll()).ToSelectList();
            if (id.HasValue) {
                vm = _mapper.Map<BuyerColorViewModel>(await _buyerColorService.GetById(id.Value));
                buyersList.Find(x => Convert.ToInt64(x.Value) == vm.BuyerId).Selected = true;
            }
            ViewBag.BuyersList = buyersList;
            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, BuyerColorViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<BuyerColor>(vm);
                    if (!id.HasValue) {
                        // create
                        await _buyerColorService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update
                        await _buyerColorService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex) {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col) {
            try {
                if (id.HasValue) {
                    await _buyerColorService.Delete(await _buyerColorService.GetById(id.Value));
                    return new StatusCodeResult(StatusCodes.Status200OK);
                }
            }
            catch (Exception) {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }
    }
}