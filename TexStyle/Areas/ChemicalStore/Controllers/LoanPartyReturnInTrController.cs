using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Core.Gate;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]

    public class LoanPartyReturnInTrController : Controller {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public LoanPartyReturnInTrController(IUnitOfWork uow, IMapper mapper) {
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
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value,ChemicalTransactions.LoanPartyReturnIn));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {

            var partyList = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateIgpList = (await _uow.GateTrService.GetAllByActivityId((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_RETURN_IN)).Id)).ToSelectList(nameof(GateTr.Sno));

            TrViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<TrViewModel>(await _uow.DyeChemicalTrService.GetById(id.Value));

                partyList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                gateIgpList.Find(x => Convert.ToInt64(x.Value) == vm.GateTrId).Selected = true;
            }
            ViewBag.partyList = partyList;
            ViewBag.gateIgpList = gateIgpList;

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrViewModel vm) {
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<DyeChemicalTr>(vm);
                    m.TrType = ChemicalTransactions.LoanPartyReturnIn;
                    var lc = new DyeChemicalTr();

                    if (!id.HasValue) {
                        // create
                        lc = await _uow.DyeChemicalTrService.Create(m);
                        vm.Id = lc.Id;
                        _tempData.MSG = "Successfully Created";
                        AddDetail(lc.GateTrId.Value, lc.Id);
 
                    } else {
                        //update

                        var oldo = await _uow.DyeChemicalTrService.GetById(m.Id);
                        lc = await _uow.DyeChemicalTrService.Update(m);
                        //update linker master Date


                      //  if (oldo.TransactionDate != lc.TransactionDate) _uow.TrLinkerMasterService.UpdateTrMaster(null, null, m.TransactionDate, m.Id, null, ChemicalTransactions.LoanPartyReturnIn);

                        var o = await _uow.DyeChemicalTrService.GetById(lc.Id);
                        if (oldo.GateTrId != o.GateTrId)
                        {
                            o.DyeChemicalTrDetails.ToList().ForEach(de => {
                               // _uow.TrLinkerMasterService.DeletebyTRType(Convert.ToInt64(de.DyeChemicalTrId.Value), de.Id, ChemicalTransactions.LoanPartyReturnIn);
                                _uow.DyeChemicalTrDetailService.Delete(de);
                                //delete linker tr

                            });
                            AddDetail(lc.GateTrId.Value, lc.Id);
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


        public async Task AddDetail(long GateTrId, long HeaderId)
        {

            var igpDetails = (await _uow.GateTrService.GetById(GateTrId)).GateTrDetails.Where(x => x.IsDeleted == false);

            foreach (var d in igpDetails)
            {
             var detail = await _uow.DyeChemicalTrDetailService.Create(new DyeChemicalTrDetail
                {
                    QtyCr = d.QtyCr,
                    QtyDr = d.QtyDr,
                    ChemicalId = d.ChemicalId,
                    DyeId = d.DyeId,
                    DyeChemicalTrId = HeaderId,
                    Rate = Convert.ToDecimal(d.Rate),
                    IsDr=true
                });
            
            //TrLinkerMaster linker = new TrLinkerMaster();
            //    linker.TrStatus = LinkerMasterTrStatus.Debit;
            //    linker.DyeId = d.DyeId;
            //    linker.ChemicalId = d.ChemicalId;
            //    linker.DyeChemicalTrId = HeaderId;
            //    linker.DyeChemicalTrDetailId = detail.Id;
            //    linker.Date = _uow.DyeChemicalTrService.GetById(Convert.ToInt64(HeaderId)).TransactionDate;
            //    _uow.TrLinkerMasterService.Create(linker);
            }
        }

        public async Task<IActionResult> Details(long Id) => View(await _uow.DyeChemicalTrService.GetById(Id));

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
        public async Task<IActionResult> LoanPartyReturnInDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);
                ViewBag.reportTitle = nameof(LoanPartyReturnInTr);
                ViewBag.reportStatus = "INWARD GATE PASS";
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }

}