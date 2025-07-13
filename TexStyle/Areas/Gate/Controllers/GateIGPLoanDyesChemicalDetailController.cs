using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.Gate;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Gate;

namespace TexStyle.Areas.Gate.Controllers
{
    [Area(AreaConstants.GATE.Name)]
    public class GateIGPLoanDyesChemicalDetailController : Controller
    {
        public IUnitOfWork _uow { get; set; }
        public IMapper _map { get; set; }
        private readonly TempDataViewModel _tempData;
        public GateIGPLoanDyesChemicalDetailController(IUnitOfWork uow, IMapper map)
        {
            _uow = uow;
            _map = map;

        }

        public async Task<IActionResult> Index(long? id) {
            return View(await _uow.GateTrDetailService.GetAll());
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
            return View(await _uow.GateTrDetailService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id, long? GateTrId)
        {
            var dyeList = (await _uow.DyeService.GetAll()).ToSelectList();
            var chemicalList =  (await _uow.ChemicalService.GetAll()).ToSelectList();
            // var yarnTypeList = _uow.YarnTypeService.GetAll().ToSelectList();
            GateTrDetailViewModel vm = new GateTrDetailViewModel();

            if (id != null)
            {
                //edit
                vm = _map.Map<GateTrDetailViewModel>(await _uow.GateTrDetailService.GetById(id.Value));
                if (vm.DyeId != null) dyeList.Find(x => Convert.ToInt64(x.Value) == vm.DyeId).Selected = true;
                if (vm.ChemicalId != null) chemicalList.Find(x => Convert.ToInt64(x.Value) == vm.ChemicalId).Selected = true;
                vm.AvailableKgs = vm.QtyDr.Value;
                //if (vm.YarnTypeId != null) yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
            }
            else
            {
                vm.GateTrId = GateTrId.Value;
            }


            ViewBag.dyeList = dyeList;
            ViewBag.chemicalList = chemicalList;
            //ViewBag.yarnTypeList = yarnTypeList;

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, GateTrDetailViewModel v)
        {
            GateTrDetail vm = new GateTrDetail();
            if (ModelState.IsValid)
            {

                try
                {

                    if (id.Value != 0)
                    {
                        //edit
                        vm = _map.Map<GateTrDetail>(v);
                        vm.FabricTypeId = null;
                        vm.YarnTypeId = null; 
                        await _uow.GateTrDetailService.Update(vm);

                        //_tempData.MSG = "Successfully Updated";
                    }
                    else
                    {
                        //add
                        vm = _map.Map<GateTrDetail>(v);

                        await _uow.GateTrDetailService.Create(vm);
                        // _tempData.MSG = "Successfully Created";
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }


            return RedirectToAction("Details", "GateIGPLoanDyesChemical", new { id = vm.GateTrId });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    await _uow.GateTrDetailService.Delete(await _uow.GateTrDetailService.GetById(id.Value));
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