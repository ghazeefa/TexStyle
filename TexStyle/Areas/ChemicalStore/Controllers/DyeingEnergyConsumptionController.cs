using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Core.CS;
using TexStyle.Extensions;
using TexStyle.ViewModels;

namespace TexStyle.Areas.ChemicalStore.Controllers
{

        [Area("ChemicalStore")]
        public class DyeingEnergyConsumptionController : Controller
        {
            private readonly IUnitOfWork _uow;
            private readonly TempDataViewModel _tempData;

        public DyeingEnergyConsumptionController(IUnitOfWork uow)
            {
                this._uow = uow;
            _tempData = new TempDataViewModel();
            }

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
            return View(await _uow.DyeingEnergyConsumptionService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
            }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            DyeingEnergyConsumption m = null;
            if (id.HasValue)
            {
                m = await _uow.DyeingEnergyConsumptionService.GetById(id.Value);
            }
            return PartialView(m);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, DyeingEnergyConsumption vm)
        {
            DateTime providedDate = vm.Date;
            DateTime firstDayOfMonth = new DateTime(providedDate.Year, providedDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            DyeingEnergyConsumption addedData = await _uow.DyeingEnergyConsumptionService.GetEnergyConsumptionBetweenDates(firstDayOfMonth, lastDayOfMonth, vm.IsYarn);

            if (addedData != null && !vm.Id.HasValue)
            {
                ViewBag.errormsg = "An entry for this month already exists.";
                return View(vm);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (!id.HasValue)
                        {
                            // create
                            await _uow.DyeingEnergyConsumptionService.Create(vm);
                            _tempData.MSG = "Successfully Created";
                        }
                        else
                        {
                            //update
                            await _uow.DyeingEnergyConsumptionService.Update(vm);
                            _tempData.MSG = "Successfully Updated";
                        }
                    }
                    catch (Exception ex)
                    {
                        _tempData.Error = ex.Message;
                        throw ex;
                    }
                }

                return RedirectToAction("Index");
            }  
            
        }

    }
    
}