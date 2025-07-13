using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Core.Gate;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class LoanTakenReturnOutTrController : Controller {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public LoanTakenReturnOutTrController(IUnitOfWork uow, IMapper mapper) {
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
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value,ChemicalTransactions.LoanTakenReturnOut));
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            //var gateIgpList = _uow.DyeChemicalTrService.GetAllByTrType(ChemicalTransactions.LoanTakenIn).ToSelectList();
            var LoanTakenInList = (await _uow.DyeChemicalTrService.Vw_LoanReturnOut_PartiallyDispatchedViewModel(Convert.ToInt64((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_TAKEN_IN)).Id))).ToSelectList(nameof(GateTr.Sno));

            

            var supplierList = (await _uow.PartyService.GetAll()).ToSelectList();

            TrViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<TrViewModel>(_uow.DyeChemicalTrService.GetById(id.Value));
                LoanTakenInList.Find(x => Convert.ToInt64(x.Value) == vm.DyeChemicalXrefTrId).Selected = true;
                supplierList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
            }
            ViewBag.supplierList = supplierList;
            ViewBag.LoanTakenInList = LoanTakenInList;
            return PartialView(vm);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrViewModel vm)
        {
            //ModelState.Remove(nameof(vm.Id));
            //if (ModelState.IsValid) {
            //    try {
            //        var m = _mapper.Map<DyeChemicalTr>(vm);
            //        var lt = new DyeChemicalTr();
            //        if (!id.HasValue) {
            //            // create
            //            m.TrType = ChemicalTransactions.LoanTakenReturnOut;
            //            lt = _uow.DyeChemicalTrService.Create(m);
            //            vm.Id = lt.Id;

            //            AddDetail(vm.DyeChemicalXrefTrId.Value, lt.Id);
            //            _tempData.MSG = "Successfully Created";
            //        } else {
            //            //update
            //            var oldlt= _uow.DyeChemicalTrService.GetById(m.Id);
            //            lt = _uow.DyeChemicalTrService.Update(m);
            //            //if (oldlt.TransactionDate != lt.TransactionDate) _uow.TrLinkerMasterService.UpdateTrMaster(null, null, m.TransactionDate, m.Id, null, ChemicalTransactions.LoanTakenReturnOut);

            //            if (oldlt.DyeChemicalXrefTrId!= lt.DyeChemicalXrefTrId)
            //            {   var o = _uow.DyeChemicalTrService.GetById(lt.Id);

            //            o.DyeChemicalTrDetails.ToList().ForEach(de => {
            //                //_uow.TrLinkerMasterService.DeletebyTRType(Convert.ToInt64(de.LoanTakenReturnOutTrId.Value), de.Id, ChemicalTransactions.LoanTakenReturnOut);

            //                _uow.DyeChemicalTrDetailService.Delete(de);
            //            });
            //            AddDetail(vm.DyeChemicalXrefTrId.Value, lt.Id);
            //            }



            //            _tempData.MSG = "Successfully Updated";
            //        }

            //        //_uow.GateTrService.GetById(l);

            //        //take the loan taken in


            //        return RedirectToAction("Details", "LoanTakenReturnOutTr", new { Id = m.Id });

            //        //if (!id.HasValue)
            //        //{
            //        //    // create
            //        //    _uow.DyeChemicalTrService.Create(m);
            //        //    _tempData.MSG = "Successfully Created";
            //        //}
            //        //else
            //        //{
            //        //    //update
            //        //    _uow.DyeChemicalTrService.Update(m);
            //        //    _tempData.MSG = "Successfully Updated";
            //        //}
            //    }
            //    catch (Exception ex) {
            //        _tempData.Error = ex.Message;
            //        throw ex;
            //    }
            //}
            //return RedirectToAction(nameof(Index));

    
            return RedirectToAction("Index", "LoanTakenReturnOutTr");

        }

        public async Task AddDetail(long loaninId, long HeaderId)
        {

            var igpDetails = (await _uow.DyeChemicalTrService.GetById(HeaderId)).DyeChemicalTrDetails.Where(x => x.IsDeleted == false);

            foreach (var d in igpDetails)
            {
                var q = await _uow.DyeChemicalTrDetailService.GetUsedKgLoanTakenofDyeChemicalTr(d.Id);
                if (Convert.ToDecimal(d.QtyDr) - q != 0)
                {
                var detail=    _uow.DyeChemicalTrDetailService.Create(new DyeChemicalTrDetail
                   {
                        IsDr= false,
                        QtyCr = d.QtyDr - q,
                        QtyDr = d.QtyCr,
                        ChemicalId = d.ChemicalId,
                        DyeId = d.DyeId,
                        Packet = d.Packet,
                        Rate = d.Rate,
                        DyeChemicalTrId = HeaderId,
                        DyeChemicalXrefDetailTrId = d.Id,

                    });
                    //TrLinkerMaster linker = new TrazsLinkerMaster();
                    //linker.TrStatus = LinkerMasterTrStatus.Credit;
                    //linker.DyeId = d.DyeId;
                    //linker.ChemicalId = d.ChemicalId;
                    //linker.LoanTakenReturnOutTrId = HeaderId;
                    //linker.LoanTakenReturnOutTrDetailId = detail.Id;
                    //linker.Date = _uow.DyeChemicalTrService.GetById(Convert.ToInt64(HeaderId)).TransactionDate;
                    //_uow.TrLinkerMasterService.Create(linker);
                }
            }
        }

        public async Task<IActionResult> ChemicalDetails(long id)
        {
            var returnout = await _uow.DyeChemicalTrService.DyeChemicalLoanReturnOut_RemainingBlncService(id);
            //var m = _mapper.Map<DyeChemicalTrDetailViewModel>(returnout);
            
            return PartialView(returnout);
        }


        public async Task<IActionResult> GetParty(long id)
        {
            var p = await _uow.DyeChemicalTrService.GetById(id);
            long gateid = p.GateTr.Sno;
            var result = new
            {
                Id = p.Party.Id,
                Name = p.Party.Name,
                GId = gateid

            };

            return Json(result);
        }

        public async Task<IActionResult> Details(long Id) 
        {
            //var any =_uow
            var a = await _uow.DyeChemicalTrService.GetById(Id);
            return View(a); 
        }

        [HttpGet]
        public async Task<IActionResult> LoanTakenReturnOutDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);
                ViewBag.reportTitle = nameof(LCImportInTr);
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

        [HttpPost]
        public async Task<IActionResult> AddQuantity(string list)
         {
            try
            {
                var check = await _uow.DyeChemicalTrService.GetAllByTrType(9);
                ////int max = (int)_uow.DyeChemicalTrService.GetGetAllByTrTypeAll(9).Max(x => x.No);
                var listdata = JsonConvert.DeserializeObject<List<UpdateDr>>(list).ToList();


                string qtystring = "";
                foreach (var a in listdata)
                {
                    qtystring = qtystring + Convert.ToString(a.DetailId) + '-' + Convert.ToString(a.QtyDr) + ',';
                }

                var id = _uow.DyeChemicalTrDetailService.DyeChemicalUpdateDr(listdata.FirstOrDefault().id,
              listdata.FirstOrDefault().FairPrice == null ? 0 : listdata.FirstOrDefault().FairPrice,
              listdata.FirstOrDefault().IgpRefNo == null ? 0 : listdata.FirstOrDefault().IgpRefNo,
              qtystring,
              Convert.ToInt32(ChemicalTransactions.LoanTakenReturnOut),
              listdata.FirstOrDefault().InvoiceNo == null ? 0 : listdata.FirstOrDefault().InvoiceNo,
              listdata.FirstOrDefault().Dtreno == null ? 0 : listdata.FirstOrDefault().Dtreno,
              listdata.FirstOrDefault().InvoiceDate == null ? default(DateTime).Date : listdata.FirstOrDefault().InvoiceDate);


                //    var listdata = JsonConvert.DeserializeObject<List<AddRecord>>(list).ToList();



                //    string qtystring = "";
                //foreach (var a in listdata)
                //{





                //    var id = _uow.PPCPlanningService.AddRecord(a.id,
                //       a.DetailId == 0 ? 0 : a.DetailId,
                //       a.PurchaseOrderId == 0 ? 0 : a.PurchaseOrderId,
                //       a.ProductionTypeId == 0 ? 0 : a.ProductionTypeId,
                //       a.IssuedDate == null ? default(DateTime).Date : a.IssuedDate,
                //        a.IsConfirmed,


                //    a.QtyDr == 0 ? 0 :a.QtyDr,
                //        a.LotNo == 0 ? 0 : a.LotNo
                //       );


                //}


                //ChemicalTransactions.LoanTakenReturnOut

                //var afd = listdata.FirstOrDefault().id;
                //var ab = listdata.FirstOrDefault().FairPrice;
                //var aa = listdata.FirstOrDefault().IgpRefNo;
                //var abh = qtystring;
                //var agh = Convert.ToInt32(ChemicalTransactions.LoanTakenReturnOut);
                //var asf = listdata.FirstOrDefault().InvoiceNo;
                //var aga = listdata.FirstOrDefault().Dtreno;
                //var ahh = listdata.FirstOrDefault().InvoiceDate;





                //var id=    _uow.DyeChemicalTrDetailService.DyeChemicalUpdateDr(listdata.FirstOrDefault().id,
                //listdata.FirstOrDefault().FairPrice== null? 0 : listdata.FirstOrDefault().FairPrice,
                //listdata.FirstOrDefault().IgpRefNo== null ? 0 : listdata.FirstOrDefault().IgpRefNo,
                //qtystring ,
                //Convert.ToInt32(ChemicalTransactions.LoanTakenReturnOut),
                //listdata.FirstOrDefault().InvoiceNo == null ? 0 : listdata.FirstOrDefault().InvoiceNo, 
                //listdata.FirstOrDefault().Dtreno == null ? 0 : listdata.FirstOrDefault().Dtreno, 
                //listdata.FirstOrDefault().InvoiceDate== null ? default(DateTime).Date: listdata.FirstOrDefault().InvoiceDate);



                //var m = _mapper.Map<DyeChemicalTr>(vm);
                //var lt = new DyeChemicalTr();
                //return View(_uow.DyeChemicalTrService.GetById(id));
                //TempData["Id"] = id;
                //return RedirectToAction("Details", "LoanTakenReturnOutTr", new { Id = id });
                //_uow.GateTrDetailService.UpdateRates(abc);


                //return RedirectToAction("Index", "PPCPlanning", new { area = "ppc" });
                return RedirectToAction("Index", "PPCPlanning");
            }
            catch (Exception ex)
            {

                throw ex;
            }

           //eturn Json(new { msg = "s" });

           
        }
        // [HttpPost]
        //public IActionResult AddLPSQuantity(string list)
        // {
        //    try
        //    {
        //        //if(list.FirstOrDefault(){


        //        //}

        //        var listdata = JsonConvert.DeserializeObject<List<AddRecord>>(list).ToList();
        //        string qtystring = "";
        //        foreach(var a in listdata)
        //        {
        //            qtystring = qtystring+ Convert.ToString(a.DetailId) + '-' + Convert.ToString(a.QtyDr)+',';
        //        }







        //        return RedirectToAction("Details", "LoanTakenReturnOutTr", new { Id = id });
        //        //_uow.GateTrDetailService.UpdateRates(abc);
        //        return RedirectToAction("Index", "LoanTakenReturnOutTr");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //   //eturn Json(new { msg = "s" });


        //}


    }
}