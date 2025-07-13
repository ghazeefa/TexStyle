using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class LoanPartyGivenOutTrController : Controller {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public LoanPartyGivenOutTrController(IUnitOfWork uow, IMapper mapper) {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
        }

        public async Task<IActionResult> Index() {
            return View(await _uow.DyeChemicalTrService.GetAll());
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            //COMMENTED FOR TIME BEING
            // var endDate = startDate.AddMonths(1).AddDays(-1); 
            var startTempDate = new DateTime(2018, 1, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue) {
                options.sd = startTempDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value,ChemicalTransactions.LoanPartyGivenOut));
        }


        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {

            var supplierList = (await _uow.PartyService.GetAll()).ToSelectList();

            TrViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<TrViewModel>(await _uow.DyeChemicalTrService.GetById(id.Value));

                supplierList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
            }
            ViewBag.supplierList = supplierList;

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrViewModel vm) {
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<DyeChemicalTr>(vm);
                    m.TrType = (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT)).Id;
                    if (!id.HasValue) {
                        // create
                        await _uow.DyeChemicalTrService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update
                        var oldlt = await _uow.DyeChemicalTrService.GetById(m.Id);
                         var lt = await _uow.DyeChemicalTrService.Update(m);
                        //if (oldlt.TransactionDate != lt.TransactionDate) _uow.TrLinkerMasterService.UpdateTrMaster(null, null, m.TransactionDate, m.Id, null, ChemicalTransactions.LoanPartyGivenOut);

                        _tempData.MSG = "Successfully Updated";
                    }
                    return RedirectToAction("Details", "LoanPartyGivenOutTr", new { Id = m.Id });

                }
                catch (Exception ex) {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(long Id) {

            var m = await _uow.DyeChemicalTrService.GetById(Id);
            var supplierList = (await _uow.PartyService.GetAll()).ToSelectList();

            if (m != null) {
                supplierList.Find(x => Convert.ToInt64(x.Value) == m.PartyId).Selected = true;
            }

            ViewBag.supplierList = supplierList;
            return View(m);

        }

        [HttpGet]
        public async Task<IActionResult> LoanPartyGivenOutDetailReport(long id) {
            try {
                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);
                ViewBag.reportTitle = nameof(LoanPartyGivenOutTrController);
                ViewBag.reportStatus = "OUTWARD GATEPASS";
                return RedirectToAction("LCImportDetailReport", "LCImportInTr", planin);
            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);
                ViewBag.reportTitle = nameof(DyeChemicalTr);
                ViewBag.reportStatus = "OUTWARD GATE PASS";
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        //[HttpPost]
        //public IActionResult Delete(long? id, IFormCollection col)
        //{
        //    try
        //    {
        //        if (id.HasValue)
        //        {
        //            _uow.DyeChemicalTrService.Delete(_uow.DyeChemicalTrService.GetById(id.Value));
        //            return new StatusCodeResult(StatusCodes.Status200OK);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }
        //    return new StatusCodeResult(StatusCodes.Status404NotFound);
        //}
    }
}