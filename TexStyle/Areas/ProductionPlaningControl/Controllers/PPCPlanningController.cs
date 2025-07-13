using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class PPCPlanningController : Controller {

        private IUnitOfWork _uow;
        private IPPCPlanningService _PPCPlanningService;

        [ViewData]

        public string AreaName { get; set; }
        private readonly TempDataViewModel _tempData;
        private readonly IReportFilterService _reportFilterService;
        private readonly IMapper _mapper;
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(PPCPlanning)}";

        public PPCPlanningController(
            IPPCPlanningService pPCPlanningService,
            IUnitOfWork uow, IMapper mapper,
            IReportFilterService ReportFilterService) {
            AreaName = "PPC Planing";
            _uow = uow;
            _mapper = mapper;
            _reportFilterService = ReportFilterService;

            _PPCPlanningService = pPCPlanningService;
        }

        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_PLANING_VIEW)]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {

            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue) {
                options.sd = startDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            ViewBag.filter = filter;

            if (filter.IsYarn == true)
            {
                return View(await _uow.PPCPlanningService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
            }
            else
            {
                var AA = await _uow.PPCPlanningService.GetBetweenDateRangeFabric(options.sd.Value, options.ed.Value);
                return (View(AA));
            }

        }
        [HttpPost]
        public async Task<IActionResult> Reprocess(long? id, IFormCollection col) {
            return View();
        }
        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_PLANING_CREATE)]
        [Authorize(Policy = AccountClaimKeys.PPC_PLANING_EDIT)]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var partyList = (await _uow.PartyService.GetAll()).ToSelectList();
            var buyerlist = (await _uow.BuyerService.GetAll()).ToSelectList();
            var buyerColorList = (await _uow.BuyerColorService.GetAll()).ToSelectList();
            var yarnTypeList = (await _uow.YarnTypeService.GetAll()).ToSelectList();
            var yarnQualityList = (await _uow.YarnQualityService.GetAll()).ToSelectList();
            var yarnManufacturerList = (await _uow.YarnManufacturerService.GetAll()).ToSelectList();

            var fabricTypeList = (await _uow.FabricTypesService.GetAll()).ToSelectList();
            var fabricTypesList = (await _uow.FabricTypesService.GetAll()).ToSelectList();
            var fabricQualityList = (await _uow.FabricQualityService.GetAll()).ToSelectList();

            var purchaseOrderList = (await _uow.PurchaseOrderService.GetCommercialPo()).ToSelectList(nameof(PurchaseOrder.Po));
            var orderActivityList = (await _uow.ProductionTypeService.GetAll()).ToSelectList();

            var knittingPartyList = (await _uow.knittingPartyService.GetAll()).ToSelectList();
          
            long lpsNo = 0;
            PPCPlanningViewModel vm = new PPCPlanningViewModel();
            var _showReprocess = false;
            // edit
            if (id.HasValue && id != 0) {
       

                var m = await _uow.PPCPlanningService.GetById(id.Value);
                var igp = await _uow.IGPDetailService.GetById(m.InwardGatePassDetailId.Value);
                //   PPCPlanning p = new PPCPlanning();

                var ppc = await _uow.PPCPlanningService.GetAll();

                m.AvailableLpsKgs = igp.NetWeightInKg;

                ppc.Where(p => p.InwardGatePassDetailId == igp.Id).ToList().ForEach(p => {
                    m.AvailableLpsKgs -= p.Kgs;
                });
                m.AvailableLpsKgs = +m.Kgs;

                //igpDetailList.ForEach(d => {
                //    d.AvailableLpsKgs = d.NetWeightInKg;
                //    m.Where(p => p.InwardGatePassDetailId == d.Id).ToList().ForEach(p => {
                //        d.AvailableLpsKgs -= p.Kgs;
                //    });


                //var m = _uow.PPCPlanningService.GetById(id.Value);
                vm = _mapper.Map<PPCPlanningViewModel>(m);
                lpsNo = vm.Id.Value;
                vm.IGPNo = m.InwardGatePassDetail?.InwardGatePassId;
                partyList.Find(x => Convert.ToInt64(x.Value) == m.BuyerColor.Buyer.PartyId).Selected = true;
                buyerlist.Find(x => Convert.ToInt64(x.Value) == m.BuyerColor.BuyerId).Selected = true;
                buyerColorList.Find(x => Convert.ToInt64(x.Value) == vm.BuyerColorId).Selected = true;
                if (filter.IsYarn == true)
                {
                    yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                    yarnQualityList.Find(x => Convert.ToInt64(x.Value) == vm.YarnQualityId).Selected = true;
                    yarnManufacturerList.Find(x => Convert.ToInt64(x.Value) == vm.YarnManufacturerId).Selected = true;
                }
                else
                {
                    fabricTypeList.Find(x => Convert.ToInt64(x.Value) == vm.FabricTypesId).Selected = true;
                    fabricQualityList.Find(x => Convert.ToInt64(x.Value) == vm.FabricQualityId).Selected = true;
                    knittingPartyList.Find(x => Convert.ToInt64(x.Value) == vm.KnitingPartyId).Selected = true;
                }
                purchaseOrderList.Find(x => Convert.ToInt64(x.Value) == vm.PurchaseOrderId).Selected = true;
                orderActivityList.Find(x => vm.ProductionTypeId == Convert.ToInt64(x.Value)).Selected = true;

                ViewBag.buyerlist = buyerlist;
                ViewBag.buyerColorList = buyerColorList;
                _showReprocess = true;

            }

            else {
                var ppcPlaning = (await _uow.PPCPlanningService.GetAll()).LastOrDefault();
                lpsNo = (lpsNo == 0 && ppcPlaning == null) ? 1 : ++ppcPlaning.Id;


            }
            //else if (@ViewData["IGPNo"]!= null )
            //{
            //    return PartialView($"{_ViewPath}/{nameof(CopyIgpData)}.cshtml", vm);
            //}
            //ViewBag.Issuance = issue.IsIssued;
            if (id.HasValue && id != 0 && filter.IsYarn==false)
            {

                var issue = await _uow.LotMarkingService.GetByLPSNo(id.Value);
                //if (issue==null)
                //{
                //    ViewBag.Issuance = false;

                //}
             

            }

            ViewBag.filter = filter;
            ViewBag.ShowReprocess = _showReprocess;
            ViewBag.partyList = partyList;
            ViewBag.yarnTypeList = yarnTypeList;
            ViewBag.yarnQualityList = yarnQualityList;
            ViewBag.yarnManufacturerList = yarnManufacturerList;

            ViewBag.knittingPartyList = knittingPartyList;

            ViewBag.fabricTypeList = fabricTypeList;
            ViewBag.fabricTypesList = fabricTypesList;
            ViewBag.fabricQualityList = fabricQualityList;
            ViewBag.purchaseOrderList = purchaseOrderList;
            ViewBag.orderActivityList = orderActivityList;
            ViewBag.buyerlist = buyerlist;
            ViewBag.LpsNo = lpsNo;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute] long? id, [FromForm] PPCPlanningViewModel vm) {
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var m = _mapper.Map<PPCPlanning>(vm);


            if (ModelState.IsValid && vm.Kgs != 0)
            {

                if (id.HasValue)
                {
                    //m.IssuedDate = DateTime.Now;
                    //m.IssuedDate=m.IssuedDate.
                    m.Id = id.Value;
                    if (filter.IsYarn == true)
                    {
                        await _PPCPlanningService.Update(m);
                    }
                    else
                    {
                        await _PPCPlanningService.UpdateFabric(m);

                    }

                }

                else
                {
                    if (filter.IsYarn == true)
                    {
                        m.IsCancel = false;
                        await _PPCPlanningService.Create(m);
                    }
                    else
                    {
                        m.IsCancel = false;
                        await _PPCPlanningService.CreateFabric(m);


                    }


                }
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> CopyIgpData(long? id) {
            var igpDetailList = new List<InwardGatePassDetail>();
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (id.HasValue) {
                var igp = await _uow.IGPService.GetById(id.Value);
                if (igp.IsReprocessed == false && igp.IsReWaxRecheck == false && igp.IsWithoutOGP == false && igp.IsAfterFinishing == false && igp.IsForComercialFinishing == false && igp.IsForFinishing == false)
                {

                    igpDetailList = igp?.InwardGatePassDetails.ToList()
                        .Where(x => x.IsDeleted == false).ToList();

                    var ppc = await _uow.PPCPlanningService.GetAll();
                    var OGP = await _uow.OGPDetailService.GetAll();

                    igpDetailList.ForEach(d => {
                        d.AvailableLpsKgs = d.NetWeightInKg;

                        ppc.Where(p => p.InwardGatePassDetailId == d.Id).ToList().ForEach(p => {
                            d.AvailableLpsKgs -= p.Kgs;
                        });

                        OGP.Where(p => p.InwardGatePassDetailId == d.Id).ToList().ForEach(p => {
                            d.AvailableLpsKgs -= p.Kgs;
                        });


                    });
                }
            }
            ViewBag.filter = filter;
            return PartialView(igpDetailList);

        }

        [HttpGet]
        public async Task<IActionResult> GetCKL1POData(long po)
        {
            var filter =  (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var data = (await _uow.PPCPlanningService.CKL1POViewModelRepository(po)).ToList();
            if (data==null)
            {
                return PartialView("Server Connectivty  Issue Try Again Later");
            }

            var ppcdata = await _uow.PPCPlanningService.GetListByPoCode(po.ToString());


            data.ForEach(d =>
            {
                //d.AvailableLpsKgs = d.NetWeightInKg;

                ppcdata.Where(p =>p.DyeingWODetailID  == d.DyeingWODetailID && p.DyeingWOID==d.DyeingWOID).ToList().ForEach(p =>
                {
                    d.AvailableKgs -= Decimal.ToDouble(p.Kgs);
                });
            });
            return PartialView(data);
        }
        [HttpGet]
        public async Task<IActionResult> CopyFabricIGPData(long fabricTypesId, long fabricQualityId, long buyerId)
        {          
        var igpDerailList = await _uow.PPCPlanningService.IGPRecordFabric_P8ViewModel(buyerId,fabricTypesId,fabricQualityId);   
          

            return PartialView(igpDerailList);
        }
        [HttpPost]
        public async Task<IActionResult> AddQuantity(string list)
        {
            try
            {
                var listdata = JsonConvert.DeserializeObject<List<AddRecord>>(list).ToList();

                string qtystring = "";

                await Task.WhenAll(listdata.Select(async a =>
                {
                    var id = _uow.PPCPlanningService.AddRecord(a.id,
                       a.DetailId == 0 ? 0 : a.DetailId,
                           a.PurchaseOrderId == 0 ? 0 : a.PurchaseOrderId,

                         a.ProductionTypeId == 0 ? 0 : a.ProductionTypeId,

                       a.IssuedDate == null ? default(DateTime).Date : a.IssuedDate,

                               a.IsConfirmed,

                                      a.QtyDr == 0 ? 0 : a.QtyDr,
                                           a.LotNo == 0 ? 0 : a.LotNo,
                                      a.BuyerColorId == 0 ? 0 : a.BuyerColorId,
                                      a.FactoryPo = a.FactoryPo,
                                      a.DyeingWOID = a.DyeingWOID,
                                      a.DyeingWODetailID = a.DyeingWODetailID
                       );
                }));

                //return RedirectToAction("Index", "PPCPlanning", new { area = "ppc" });
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //eturn Json(new { msg = "s" });



        }
        [HttpPost]
        [Authorize(Policy = AccountClaimKeys.PPC_PLANING_DELETE)]
        public async Task<IActionResult> Delete(long? id, IFormCollection col) {
            try {
                if (id.HasValue) {
                    await _uow.PPCPlanningService.Delete(await _uow.PPCPlanningService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
        [HttpGet]
        public async Task<IActionResult> GetPartyandBuyer(long id)
        {

            PurchaseOrder p = await  this._uow.PurchaseOrderService.GetById(id);
             long partyid = p.BuyerColor.Buyer.PartyId;
            string partyname = p.BuyerColor.Buyer.Party.Name;
            long buyerid = p.BuyerColor.BuyerId;
            string buyername = p.BuyerColor.Buyer.Name;
            var result = new { pid = partyid, pname = partyname, bid = buyerid, bname = buyername };
            return this.Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> LpsSlips(long id) {
            try {
                var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
                ViewBag.filter = filter;
                if (id == 0) return BadRequest();
                var planin = await _uow.PPCPlanningService.GetById(id);
                return View(planin);
          
            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }
        }
    }
}