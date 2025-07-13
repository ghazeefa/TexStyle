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
using TexStyle.ViewModels.CS.Forms;

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class StoreReturnNoteController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public StoreReturnNoteController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
        }

        public IActionResult Index()
        {
            return View(_uow.DyeChemicalTrService.GetAll());
        }

        [HttpGet]
        public IActionResult Index([FromQuery] FilterOptions options)
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            //COMMENTED FOR TIME BEING
            // var endDate = startDate.AddMonths(1).AddDays(-1); 
            var startTempDate = new DateTime(2018, 1, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startTempDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd")};
            return View(_uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value , ChemicalTransactions.StoreReturnNote));
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            var localpurchaseList = (await _uow.DyeChemicalTrService.GetAllByTrType(ChemicalTransactions.LocalPurchase)).ToSelectList();
            var supplierList = (await _uow.PartyService.GetAll()).ToSelectList();

            TrViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<TrViewModel>(await _uow.DyeChemicalTrService.GetById(id.Value));
                localpurchaseList.Find(x => Convert.ToInt64(x.Value) == vm.DyeChemicalXrefTrId).Selected = true;
                supplierList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
            }
            ViewBag.supplierList = supplierList;
            ViewBag.localpurchaseList = localpurchaseList;

            return PartialView(vm);
        }

        public async Task<IActionResult> GetParty(long id)
        {
            var p = await _uow.DyeChemicalTrService.GetById(id);

            var result = new
            {
                Id = p.Party.Id,
                Name = p.Party.Name,
                GId = p.GateTr.Sno
            };

            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrViewModel vm)
        {
            ModelState.Remove(nameof(vm.Id));
            ModelState.Remove(nameof(vm.GateTrId));
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<DyeChemicalTr>(vm);
                    var sn = new DyeChemicalTr();
                    if (!id.HasValue)
                    {
                        // create
                        m.TrType = 12;
                        sn = await _uow.DyeChemicalTrService.Create(m);
                        vm.Id = sn.Id;
                        _tempData.MSG = "Successfully Created";

                        AddStoreReturnnoteDetail(sn.DyeChemicalXrefTrId.Value, sn.Id);
                    }
                    else
                    {
                        //update
                        var oldsn = await _uow.DyeChemicalTrService.GetById(m.Id);
                        sn = await _uow.DyeChemicalTrService.Update(m);
                      //  if (oldsn.TransactionDate != sn.TransactionDate) _uow.TrLinkerMasterService.UpdateTrMaster(null, null, m.TransactionDate, m.Id, null, ChemicalTransactions.TrViewModel);

                        if (oldsn.DyeChemicalXrefTrId != m.DyeChemicalXrefTrId)
                        {
                         var o = await _uow.DyeChemicalTrService.GetById(sn.Id);
                            o.DyeChemicalTrDetails.ToList().ForEach(de => {
                           //     _uow.TrLinkerMasterService.DeletebyTRType(Convert.ToInt64(de.DyeChemicalTrId.Value), de.Id, ChemicalTransactions.DyeChemicalTr);
                                _uow.DyeChemicalTrDetailService.Delete(de);
                        });
                            AddStoreReturnnoteDetail(sn.DyeChemicalXrefTrId.Value, sn.Id);
                        }
                        _tempData.MSG = "Successfully Updated";
                    }

                    //_uow.GateTrService.GetById(l);

                    //take the loan taken in
         

                    return RedirectToAction("Details", "StoreReturnNote", new { Id = m.Id });
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(long Id)
        {
            var chemId = await _uow.DyeChemicalTrService.GetById(Id);
           return View(chemId);
        }
        
        public void AddStoreReturnnoteDetail(long localpurchaseid, long storenoteid)
        {
            //var igpDetails = _uow.DyeChemicalTrService.GetById(localpurchaseid).DyeChemicalTrService.Where(x => x.IsDeleted == false);

            //foreach (var d in igpDetails)
            //{
            //    var q = _uow.DyeChemicalTrDetailService.GetUsedKgLoanTakenofGateTr(d.Id);
            //    if (Convert.ToDecimal(d.QtyDr) - q != 0)
            //    {
            //      var detail=  _uow.DyeChemicalTrDetailService.Create(new DyeChemicalTrDetail
            //        {
            //            QtyCr = d.QtyDr - q,
            //            QtyDr = d.QtyCr,
            //            ChemicalId = d.ChemicalId,
            //            DyeId = d.DyeId,
            //            Rate = d.Rate,
            //            Packet = d.Packet,
            //            DyeChemicalTrId = storenoteid,
            //            DyeChemicalXrefDetailTrId = d.Id,

            //        });
                    
            //        //linker = new TrLinkerMaster();
            //        //linker.TrStatus = LinkerMasterTrStatus.Credit;
            //        //linker.DyeId = d.DyeId;
            //        //linker.ChemicalId = d.ChemicalId;
            //        //linker.StoreReturnNoteId = storenoteid;
            //        //linker.StoreReturnNoteDetailId = detail.Id;
            //        //linker.Date = _uow.LCImportInTrService.GetById(Convert.ToInt64(storenoteid)).TransactionDate;
            //        //_uow.TrLinkerMasterService.Create(linker);
            //    }
            //}

        }
        //[HttpGet]
        //public IActionResult LoanTakenReturnOutDetailReport(long id)
        //{
        //    try
        //    {
        //        if (id == 0) return BadRequest();
        //        var planin = _uow.LCImportInTrService.GetById(id);
        //        ViewBag.reportTitle = nameof(LCImportInTr);
        //        ViewBag.reportStatus = "INWARD GATE PASS";
        //        return View(planin);
        //    }
        //    catch (Exception)
        //    {
        //        return new StatusCodeResult(500);
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> StoreReturnNoteDetailReport(long id)
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
    }
}