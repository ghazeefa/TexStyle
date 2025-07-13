using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.Areas.ChemicalStore.Controllers {
    [Area(AreaConstants.CHEMICAL_STORE.Name)]

    public class LocalPurchaseInTrController : Controller {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        private readonly IOptions<CompanySettings> _companySettings;

        public LocalPurchaseInTrController(IUnitOfWork uow, IMapper mapper, IOptions<CompanySettings> companySettings) {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
            _companySettings = companySettings;
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
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value , ChemicalTransactions.LocalPurchase));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {

            var partyList = (await _uow.PartyService.GetAll()).ToSelectList();
            var igpList= (await _uow.DyeChemicalTrService.Vw_Chemical_UncopiedGatePasINViewModel(Convert.ToInt64(( await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOCAL_PURCHASE_IN)).Id))).ToSelectList(nameof(GateTr.Sno));


           // var igpList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LOCAL_PURCHASE_IN).Id).ToSelectList(nameof(GateTr.Sno));

            TrViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<TrViewModel>(await _uow.DyeChemicalTrService.GetById(id.Value));

                partyList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                igpList.Find(x => Convert.ToInt64(x.Value) == vm.GateTrId).Selected = true;
            }
            ViewBag.partyList = partyList;
            ViewBag.gateIgpList = igpList;

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrViewModel vm) {
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid) {
                try {
                    //var abc = JsonConvert.DeserializeObject<List<RateDetail>>(list);
                    var m = _mapper.Map<DyeChemicalTr>(vm);
                    m.TrType = ChemicalTransactions.LocalPurchase;
                    var lc = new DyeChemicalTr(); //used to get  dyechemical id
                    if (!id.HasValue) {
                        // create

                        lc = await _uow.DyeChemicalTrService.Create(m);
                        vm.Id = lc.Id;
                        await AddDetail(lc.GateTrId.Value, lc.Id);  // one is gatetrid and other is chemicalid

                        
                        //var ld = _uow.DyeChemicalTrDetailService.GetById(m.Id);
                        //foreach (var a in abc)
                        //{
                        //    ld.Rate = a.rate;
                        //}

                        //_uow.DyeChemicalTrDetailService.Update(ld);

                       
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update
                        var oldo = await _uow.DyeChemicalTrService.GetById(m.Id);
                        
                        lc = await _uow.DyeChemicalTrService.Update(m);
                        //if (oldo.TrDate != lc.TrDate) _uow.TrLinkerMasterService.UpdateTrMaster(null, null, m.TrDate, m.Id, null, ChemicalTransactions.LocalPurchase);

                        var o = await _uow.DyeChemicalTrService.GetById(lc.Id);


                        if (lc.GateTrId != oldo.GateTrId)
                        {
                            await Task.WhenAll(o.DyeChemicalTrDetails.Select(async de =>
                            {
                                await _uow.DyeChemicalTrDetailService.Delete(de);
                            }));

                            await AddDetail(lc.GateTrId.Value, lc.Id);
                        }
                    }
                    _tempData.MSG = "Successfully Updated";
                    
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
               var detail= await _uow.DyeChemicalTrDetailService.Create(new DyeChemicalTrDetail
                {
                    QtyCr = d.QtyCr,
                    QtyDr = d.QtyDr,
                    Rate = d.Rate.Value,
                    ChemicalId = d.ChemicalId,
                    Packet = d.Packet,
                    DyeId = d.DyeId,
                    DyeChemicalTrId = HeaderId,
                    GateTrDetailId = d.Id,
                    IsDr=true
                   
                });
                
                //TrLinkerMaster linker = new 
              //  ();
                //linker.TrStatus = LinkerMasterTrStatus.Debit;
                //linker.DyeId = d.DyeId;
                //linker.ChemicalId = d.ChemicalId;
                //linker.DyeChemicalTrId = HeaderId;
                //linker.DyeChemicalTrDetailId = detail.Id;
                //linker.Date = _uow.DyeChemicalTrService.GetById(Convert.ToInt64(HeaderId)).TrDate;
                //_uow.TrLinkerMasterService.Create(linker);
            }
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

        public async Task<IActionResult> Details(long Id)
        {
            
            var m = await _uow.DyeChemicalTrService.GetById(Id);
            var gateIGPTypeList = (await _uow.GateTrService.GetAll()).ToSelectList(nameof(GateTr.Sno));

            if (m != null) {
                if (m.GateTrId != null) gateIGPTypeList.Find(x => Convert.ToInt64(x.Value) == m.GateTrId).Selected = true;
            }
           

            ViewBag.gateIGPTypeList = gateIGPTypeList;

            return View(m);
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
        public async Task<IActionResult> LocalPurchaseInTrDetailReport(long id)
        {
            try
            {
                var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
                if (filter == null) return NotFound("Report filter doesn't have any record.");

                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);                
                ViewBag.CompanyName = _companySettings.Value.CompanyName;
                ViewBag.BranchName = filter.BranchName;
                ViewBag.reportTitle = nameof(LCImportInTr);
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

        //[HttpPost]
        //public IActionResult UpdateRatess(string list, TrDetailViewModel vm)
        //{
        //    try
        //    {
        //        var abc = JsonConvert.DeserializeObject<List<RateDetail>>(list);
        //        //var lc = _uow.DyeChemicalTrDetailService.GetById(vm.Id);
        //        //lc.Rate = abc.FirstOrDefault().rate;
        //        // Gate Tr Detail Update(List<RateDetail>)
        //        _uow.GateTrDetailService.UpdateRates(abc);
        //       // _uow.DyeChemicalTrDetailService.Update(lc);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //    return RedirectToAction("Index", "LocalPurchaseInTr", new { Id = vm.DyeChemicalTrId });

        //}






        //[HttpPost]
        //public JsonResult UpdateRatess(long Id, decimal rate)
        //{
        //    //Rate Update in Gate Tr 
        //    if (Id != 0)
        //    {


        //        var id = _uow.GateTrDetailService.GetById(Id);
        //        //ld.Rate = a.rate;
        //        //id.Rate = rate;
        //        //id.DyeId = Id;
        //        RateDetail rd = new RateDetail();
        //        id.Id = Id;
        //        id.Rate = rate;

        //        _uow.GateTrDetailService.Update(id);

        //        //GateTrDetail tr = new GateTrDetail();
        //        //var ch = _uow.GateTrDetailService.GetAll();
        //        //tr.Id = ch.FirstOrDefault().Id;
        //        //tr.Rate = ch.FirstOrDefault().Rate;
        //        //_uow.GateTrDetailService.Update(tr);
        //    }

        //    return Json(new { msg = "s" });
        //}




    }
}
