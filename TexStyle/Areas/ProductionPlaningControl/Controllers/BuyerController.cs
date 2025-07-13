using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class BuyerController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(Buyer)}";

        private readonly TempDataViewModel _tempData;
        private IUnitOfWork _uow;
        private readonly IBuyerService _bs;
        private readonly IMapper _mapper;
        public BuyerController(IUnitOfWork uow, IMapper mapper, IBuyerService bs) {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(Buyer)}";
            _tempData = new TempDataViewModel();
            _uow = uow;
            _bs = bs;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            return View((await _uow.BuyerService.GetAll()).ToList());
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
        //    return View(_uow.BuyerService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            var partyList =(await _uow.PartyService.GetAll()).ToSelectList();

            BuyerViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<BuyerViewModel>(await _uow.BuyerService.GetById(id.Value));
                partyList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
            }

            ViewBag.partyList = partyList;
            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, [FromForm]BuyerViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<Buyer>(vm);
                    if (!vm.Id.HasValue) {
                        // create
                        await _uow.BuyerService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update

                        await _bs.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex) {
                    throw ex;
                    _tempData.Error = ex.Message;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id) {
            try {
                if (id.HasValue) {
                    await _uow.BuyerService.Delete(await _uow.BuyerService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
    }
}