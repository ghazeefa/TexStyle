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
    public class PartyController : Controller {

        [ViewData]
        public string AreaName { get; set; }

        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(Party)}";

        private readonly TempDataViewModel _tempData;
        private IPartyService _partyService;
        private readonly IMapper _mapper;
        public PartyController(IPartyService partyService, IMapper mapper) {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(Party)}";
            _tempData = new TempDataViewModel();
            _partyService = partyService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index() {
            
            return View((await _partyService.GetAllWithHeader()).ToList());
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
        //    return View(_partyService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}

        public async Task<IActionResult> AddOrUpdate(long? id) {
            PartyViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<PartyViewModel>(await _partyService.GetById(id.Value));
            }
            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, PartyViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<Party>(vm);
                    if (!id.HasValue) {
                        // create
                        await _partyService.Create(m);
                    } else {
                        //update
                        await _partyService.Update(m);
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
                    await _partyService.Delete(await _partyService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            } catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }

    }
}