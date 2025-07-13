using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.YD;
using TexStyle.ViewModels;
using TexStyle.ViewModels.YD;

namespace TexStyle.Areas.YarnDyeing.Controllers {
    [Area(AreaConstants.YARN_DYEING.Name)]
    public class CostingController : Controller {
        private readonly TempDataViewModel _tempData;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CostingController(IUnitOfWork uow, IMapper mapper) {
            _tempData = new TempDataViewModel();
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            return View((await _uow.CostingService.GetAll()).ToList());
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
        //    return View(_uow.CostingService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            CostingViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<CostingViewModel>(await _uow.CostingService.GetById(id.Value));
            }
            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, CostingViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<Costing>(vm);
                    if (!id.HasValue) {
                        // create
                        await _uow.CostingService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update
                        await _uow.CostingService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                } catch (Exception ex) {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id) {
            try {
                var o =await _uow.CostingService.GetById(id);

                if (o == null)
                    return new StatusCodeResult(StatusCodes.Status404NotFound);

                await _uow.CostingService.Delete(o);

                if (o.IsDeleted == true)
                    return new StatusCodeResult(StatusCodes.Status200OK);
            } catch (Exception ex) {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                throw ex;
            }

            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
    }
}