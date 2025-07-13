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
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class GeneralConsumptionTrDetailController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;

        public GeneralConsumptionTrDetailController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uow.DyeChemicalTrDetailService.GetAll());
        }
        [HttpGet]
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

                    if (id.Value == 0)
                    {
                        // create
                        await _uow.DyeChemicalTrDetailService.Create(m);
                        //TrLinkerMaster linker = new TrLinkerMaster();
                        //linker.TrStatus = LinkerMasterTrStatus.Credit;
                        //linker.DyeId = m.DyeId;
                        //linker.ChemicalId = m.ChemicalId;
                        //linker.LoanPartyGivenOutTrDetailId = m.Id;
                        //linker.LoanPartyGivenOutTrId = m.LoanPartyGivenOutTrId;
                        //linker.Date = _uow.LoanPartyGivenOutTrService.GetById(Convert.ToInt64(m.LoanPartyGivenOutTrId)).TransactionDate;
                        //_uow.TrLinkerMasterService.Create(linker);

                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update
                        await _uow.DyeChemicalTrDetailService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                        var oldo = _uow.DyeChemicalTrDetailService.GetById(m.Id);
                        var newo = _uow.DyeChemicalTrDetailService.Update(m);
                        //if (oldo.DyeId != newo.DyeId || oldo.ChemicalId != newo.ChemicalId) _uow.TrLinkerMasterService.UpdateTrMaster(m.DyeId, m.ChemicalId, null, Convert.ToInt64(m.LoanPartyGivenOutTrId), m.Id, ChemicalTransactions.LoanPartyGivenOut);

                    }
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return RedirectToAction("Details", "GenralConsumptionTr", new { Id = vm.DyeChemicalTrId });
        }

        //[HttpGet]
        //public IActionResult AddOrUpdateDetail(long? trid, long? id)
        //{

        //    var chemicalList = _uow.ChemicalService.GetAll().ToSelectList();
        //    var dyeList = _uow.DyeService.GetAll().ToSelectList();
        //    TrDetailViewModel vm = new TrDetailViewModel();
        //    if (id.HasValue)
        //    {
        //        vm = _mapper.Map<TrDetailViewModel>(_uow.DyeChemicalTrDetailService.GetById(id.Value));

        //        if(vm.ChemicalId.HasValue)
        //            chemicalList.Find(x => Convert.ToInt64(x.Value) == vm.ChemicalId).Selected = true;
        //        if (vm.DyeId.HasValue)
        //            dyeList.Find(x => Convert.ToInt64(x.Value) == vm.DyeId).Selected = true;

        //    }

        //    ViewBag.chemicalList = chemicalList;
        //    ViewBag.dyeList = dyeList;
        //    vm.DyeChemicalTrId = trid;
        //    return PartialView(vm);
        //}




        //[HttpPost]
        //public IActionResult AddOrUpdate(long? id, TrDetailViewModel vm)
        //{
        //    ModelState.Remove(nameof(vm.Id));
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var m = _mapper.Map<DyeChemicalTrDetail>(vm);

        //            if (!id.HasValue)
        //            {
        //                // create
        //                _uow.DyeChemicalTrDetailService.Create(m);
        //                _tempData.MSG = "Successfully Created";
        //                //TrLinkerMaster linker = new TrLinkerMaster();
        //                //linker.TrStatus = LinkerMasterTrStatus.Debit;
        //                //linker.DyeId = m.DyeId;
        //                //linker.ChemicalId = m.ChemicalId;
        //                //linker.DyeChemicalTrDetailId = m.Id;
        //                //linker.ChemicalDilutionTrId = m.ChemicalDilutionTrId;
        //                //linker.Date = _uow.DyeChemicalTrService.GetById(Convert.ToInt64(m.ChemicalDilutionTrId)).TransactionDate;
        //                //_uow.TrLinkerMasterService.Create(linker);
        //            }
        //            else
        //            {
        //                //update
        //                //var oldo = _uow.DyeChemicalTrDetailService.GetById(m.Id);
        //                var newo = _uow.DyeChemicalTrDetailService.Update(m);
        //                //if (oldo.DyeId != newo.DyeId || oldo.ChemicalId!= newo.ChemicalId) _uow.TrLinkerMasterService.UpdateTrMaster(m.DyeId, m.ChemicalId, null,Convert.ToInt64( m.ChemicalDilutionTrId), m.Id, ChemicalTransactions.Dilution);
        //                _tempData.MSG = "Successfully Updated";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _tempData.Error = ex.Message;
        //            throw ex;
        //        }
        //    }
        //    return RedirectToAction("Details", "ChemicalDilutionTr", new { Id = vm.DyeChemicalTrId });
        //}


        [HttpPost]
        public async Task<IActionResult> Delete(long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var o = await _uow.DyeChemicalTrDetailService.GetById(id.Value);
                    await _uow.DyeChemicalTrDetailService.Delete(o);
                    //_uow.TrLinkerMasterService.DeletebyTRType(Convert.ToInt64( o.ChemicalDilutionTrId.Value), o.Id, ChemicalTransactions.Dilution);
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
        public async Task<JsonResult> GetDebitsChem(long id)
        {
               var p = (await _uow.DyeChemicalTrDetailService.GetAll()).Where(x => x.ChemicalId == id && x.IsDr == true).Select(e => new { e.Id, e.CreatedOn, e.QtyDr }).ToList();
               return Json(p);
        }
        public async Task<JsonResult> GetDebitDetail(long id)
        {
            var cre = (await _uow.DyeChemicalTrDetailService.GetAll()).Where(x => x.DrId == id && x.IsDr == false).Sum(x => x.QtyCr);
            var deb = (await _uow.DyeChemicalTrDetailService.GetAll()).Where(x => x.Id == id && x.IsDr == true).Select(z => new { z.QtyDr }).FirstOrDefault();
           
            
            var p = deb.QtyDr - cre;
            return Json(p);
        }
        public async Task<JsonResult> GetCreditsChemBalance(long? id)
        {
            var p = await _uow.DyeChemicalTrService.DyeChemicalTotal_Balance(id.Value);
            return Json(p);
        }
    }
}
