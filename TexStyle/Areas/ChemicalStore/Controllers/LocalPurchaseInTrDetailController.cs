using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]

    public class LocalPurchaseInTrDetailController : Controller {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public LocalPurchaseInTrDetailController(IUnitOfWork uow, IMapper mapper) {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
        }

        public async Task<IActionResult> Index() {
            return View(await _uow.DyeChemicalTrDetailService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue) {
                options.sd = startDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.DyeChemicalTrDetailService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        }


        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id, long ParentId)
        {

            var chemicalList = (await _uow.ChemicalService.GetAll()).ToSelectList();
            var dyeList = (await _uow.DyeService.GetAll()).ToSelectList();


            TrDetailViewModel vm = new TrDetailViewModel();
            if (id.HasValue)
            {
                vm = _mapper.Map<TrDetailViewModel>(await _uow.DyeChemicalTrDetailService.GetById(id.Value));

                if (vm.ChemicalId.HasValue)
                    chemicalList.Find(x => Convert.ToInt64(x.Value) == vm.ChemicalId).Selected = true;
                if (vm.DyeId.HasValue)
                    dyeList.Find(x => Convert.ToInt64(x.Value) == vm.DyeId).Selected = true;

            }

            ViewBag.chemicalList = chemicalList;
            ViewBag.dyeList = dyeList;


            vm.DyeChemicalTrId = ParentId;
            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrDetailViewModel vm)
        {
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<DyeChemicalTrDetail>(vm);

                    if (!id.HasValue)
                    {
                        // create
                        await _uow.DyeChemicalTrDetailService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update
                        await _uow.DyeChemicalTrDetailService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return RedirectToAction("Details", "LocalPurchaseInTr", new { Id = vm.DyeChemicalTrId });
        }



        [HttpPost]
        public async Task<IActionResult> Update(TrDetailViewModel vm)
         {
            try
            {
                var lc = await _uow.DyeChemicalTrDetailService.GetById(vm.Id);
                lc.Rate = vm.Rate.Value;
                await _uow.DyeChemicalTrDetailService.Update(lc);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("Details", "LocalPurchaseInTr", new { Id = vm.DyeChemicalTrId });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    await _uow.DyeChemicalTrDetailService.Delete(await _uow.DyeChemicalTrDetailService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
    }
}