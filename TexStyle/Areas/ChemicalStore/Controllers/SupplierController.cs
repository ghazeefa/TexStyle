using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class SupplierController : Controller {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;

        public SupplierController(IUnitOfWork uow, IMapper mapper) {
            _tempData = new TempDataViewModel();
            _uow = uow;
            _mapper = mapper;

        }
        public IActionResult Index() {
            return View(_uow.SupplierService.GetAll());
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
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(_uow.SupplierService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        }


        [HttpGet]
        public IActionResult AddOrUpdate(long? id) {
            SupplierViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<SupplierViewModel>(_uow.SupplierService.GetById(id.Value));
            }
            return PartialView(vm);
        }
        [HttpPost]
        public IActionResult AddOrUpdate(long? id, SupplierViewModel vm) {
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<Supplier>(vm);
                    if (!id.HasValue) {
                        // create
                        _uow.SupplierService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update
                        _uow.SupplierService.Update(m);
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
        public IActionResult Delete(long? id, IFormCollection col) {
            try {
                if (id.HasValue) {
                    _uow.SupplierService.Delete(_uow.SupplierService.GetById(id.Value));
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