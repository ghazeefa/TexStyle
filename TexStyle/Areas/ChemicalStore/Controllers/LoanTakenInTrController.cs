using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Core.Gate;
using TexStyle.Core.ReportsViewModel.CS.RateDetail;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]

    public class LoanTakenInTrController : Controller {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public LoanTakenInTrController(IUnitOfWork uow, IMapper mapper) {
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
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value , ChemicalTransactions.LoanTakenIn));
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {

            var supplierList = (await _uow.PartyService.GetAll()).ToSelectList();
            // var gateIgpList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_TAKEN_IN).Id).ToSelectList(nameof(GateTr.Sno));
            var gateIgpList = (await _uow.DyeChemicalTrService.Vw_Chemical_UncopiedGatePasINViewModel(Convert.ToInt64((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_TAKEN_IN)).Id))).ToSelectList(nameof(GateTr.Sno));


            TrViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<TrViewModel>(await _uow.DyeChemicalTrService.GetById(id.Value));

                supplierList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                gateIgpList.Find(x => Convert.ToInt64(x.Value) == vm.GateTrId).Selected = true;

            }
            ViewBag.supplierList = supplierList;
            ViewBag.gateIgpList = gateIgpList;

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrViewModel vm) {
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<DyeChemicalTr>(vm);
                    var lt = new DyeChemicalTr();
                    m.TrType = ChemicalTransactions.LoanTakenIn;
                    if (!id.HasValue) {
                        // create
                       
                        lt = await _uow.DyeChemicalTrService.Create(m);
                        vm.Id = lt.Id;
                        await AddLoanTakenInDetail(m.GateTrId.Value, m.Id);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update
                      var   oldlt = await _uow.DyeChemicalTrService.GetById(m.Id);
                        lt = await _uow.DyeChemicalTrService.Update(m);
                        //if (oldlt.TransactionDate != lt.TransactionDate) _uow.TrLinkerMasterService.UpdateTrMaster(null, null, m.TransactionDate, m.Id, null, ChemicalTransactions.LoanTakenIn);

                        if (oldlt.GateTrId!=lt.GateTrId)
                        {
                            var o = await _uow.DyeChemicalTrService.GetById(lt.Id);

                            await Task.WhenAll(o.DyeChemicalTrDetails.Select(async de =>
                            {
                                await _uow.DyeChemicalTrDetailService.Delete(de);
                            }));

                            await AddLoanTakenInDetail(m.GateTrId.Value, m.Id);
                        }
                        _tempData.MSG = "Successfully Updated";
                    }


                }
                catch (Exception ex) {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return RedirectToAction(nameof(Details), new { id = vm.Id });

        }


        public async Task AddLoanTakenInDetail(long igpno , long loantakenid)
        {
            var igpDetails = (await _uow.GateTrService.GetById(igpno)).GateTrDetails.Where(x => x.IsDeleted == false);

            var tasks = igpDetails.Select(async d =>
            {
                var q = await _uow.DyeChemicalTrDetailService.GetUsedKgLoanTakenofGateTr(d.Id);
                if (Convert.ToDecimal(d.QtyDr) - q != 0)
                {
                    var detail = await _uow.DyeChemicalTrDetailService.Create(new DyeChemicalTrDetail
                    {
                        QtyCr = d.QtyCr,
                        QtyDr = d.QtyDr,
                        ChemicalId = d.ChemicalId,
                        Packet = d.Packet,
                        Rate = d.Rate.Value,
                        DyeId = d.DyeId,
                        DyeChemicalTrId = loantakenid,
                        GateTrDetailId = d.Id,
                        IsDr = true
                    });

                    // TrLinkerMaster linker = new TrLinkerMaster();
                    // linker.TrStatus = LinkerMasterTrStatus.Debit;
                    // linker.DyeId = d.DyeId;
                    // linker.ChemicalId = d.ChemicalId;
                    // linker.DyeChemicalTrId = loantakenid;
                    // linker.DyeChemicalTrDetailId = detail.Id;
                    // linker.Date = _uow.DyeChemicalTrService.GetById(Convert.ToInt64(loantakenid)).TransactionDate;
                    // _uow.TrLinkerMasterService.Create(linker);
                }
            });

            await Task.WhenAll(tasks);

}

        public async Task<IActionResult> Details(long Id) {
            var m = await _uow.DyeChemicalTrService.GetById(Id);
            var gateIGPTypeList = (await _uow.GateTrService.GetAll()).ToSelectList(nameof(GateTr.Sno));

            if (m != null) {
                if (m.GateTrId != null) gateIGPTypeList.Find(x => Convert.ToInt64(x.Value) == m.GateTrId).Selected = true;
            }

            ViewBag.gateIGPTypeList = gateIGPTypeList;


            return View(m);



        }

        public async Task<IActionResult> LoanDetail(long id)
        {
            var igpDetails =  (await _uow.GateTrService.GetById(id)).GateTrDetails.Where(x => x.IsDeleted == false);
            return PartialView(igpDetails);
        }

        public async Task<IActionResult> ChemicalDetail(long id)
        {
            var igpDetails = (await _uow.GateTrService.GetById(id)).GateTrDetails.Where(x => x.IsDeleted == false);
            return PartialView(igpDetails);
        }

        public async Task<IActionResult> GetParty(long id)
        {
            var p = await _uow.GateTrService.GetById(id);

            var result = new
            {
                id = p.PartyId,

                Name = p.Party.Name,
                Date = p.Date.ToShortDateString(),
            };



            //var p = _uow.GateTrService.GetById(id).Party;
            //var d= _uow.GateTrService.GetById(id).Date;

            return Json(result);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id) {
            try {
                if (id.HasValue) {
                    await _uow.DyeChemicalTrService.Delete(await _uow.DyeChemicalTrService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }

            return new StatusCodeResult(400);
        }


        [HttpGet]
        public async Task<IActionResult> LoanTakenInDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);
                ViewBag.reportTitle = nameof(LoanTakenInTr);
                ViewBag.reportStatus = "INWARD GATE PASS";
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }


        [HttpPost]
        public async Task<JsonResult> UpdateRatess(string list)
        {
            try
            {
                var abc = JsonConvert.DeserializeObject<List<RateDetail>>(list);
                await _uow.GateTrDetailService.UpdateRates(abc);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(new { msg = "s" });
        }
    }
}