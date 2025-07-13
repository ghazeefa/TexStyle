using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class OGPController : Controller {
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/OGP";

        private IMapper _mapper;
        private IUnitOfWork _uow;
        private IOGPService _oGPService;
        private IOGPDetailService _OGPDetailService;
        private IYarnTypeService _yarnTypeService;
        private IActivityTypeService _ProcessActivityTypeService;
        private IReprocessService _reProcessService;
        private IPPCPlanningService _pPCPlanningService;
        private readonly IReportFilterService _reportFilterService;
        private readonly IFabricQualityService _fabricQualityService;
        private readonly IFabricTypesService _fabricTypeService;        
        private readonly IReturnNoteDetailService _returnNoteDetailService;
        private readonly IBuyerColorService _buyerColorService;
        private readonly IBuyerService _buyerService;
        private readonly IknittingPartyService _knittingPartyService;
        public long? YarnTypeId { get; set; }
        private readonly IOptions<CompanySettings> _companySettings;

        public OGPController(IOGPService oGPService, IYarnTypeService yarnTypeService,
            IActivityTypeService processActivityTypeService, IFabricQualityService fabricQualityService, 
            IFabricTypesService fabricTypeService, IReportFilterService ReportFilterService,
            IknittingPartyService knittingPartyService, IOGPDetailService OGPDetailService, IReturnNoteDetailService returnNoteDetailServic, IBuyerColorService buyerColorService,
            IBuyerService buyerService,  IUnitOfWork uow, IMapper mapper, IPPCPlanningService pPCPlanningService, IReprocessService reprocessService, IOptions<CompanySettings> companySettings) {
            _mapper = mapper;
            _uow = uow;
            _oGPService = oGPService;
            _OGPDetailService = OGPDetailService;
            _yarnTypeService = yarnTypeService;
            _ProcessActivityTypeService = processActivityTypeService;
            _pPCPlanningService = pPCPlanningService;
            _reProcessService = reprocessService;
            _reportFilterService = ReportFilterService;
            _fabricQualityService = fabricQualityService;
            _fabricTypeService = fabricTypeService;
            _knittingPartyService = knittingPartyService;
            _returnNoteDetailService = returnNoteDetailServic;
            _buyerColorService =  buyerColorService;
            _buyerService = buyerService;
            _companySettings = companySettings;
        }

        //public IActionResult Index() {
        //    try {
        //        var ogplist = _oGPService.GetAll();

        //        return View(ogplist);
        //    }
        //    catch (Exception ex) {

        //        throw ex;
        //    }

        //}

        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_OGP_VIEW)]
        //public IActionResult Index([FromQuery] FilterOptions options) {
        //    var filter = _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).FirstOrDefault();


        //    var today = DateTime.Now;
        //    var startDate = new DateTime(today.Year, today.Month, 1);
        //    var endDate = startDate.AddMonths(1).AddDays(-1);
        //    if (!options.sd.HasValue || !options.ed.HasValue) {
        //        options.sd = startDate;
        //        options.ed = endDate;
        //    }


        //    ViewBag.filter = filter;
        //    ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
        //    if(filter.IsYarn == true)
        //    {
        //         return View(_oGPService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //        //return View(_oGPService.GetbteweenRange_OGP(options.sd.Value, options.ed.Value, Convert.ToInt32(User.Identity.GetUserId())));
        //    }
        //    else
        //    {
        //       // return View(_oGPService.GetBetweenDateRangeFabric(options.sd.Value, options.ed.Value));

        //        return View(_oGPService.GetbteweenRange_OGP(options.sd.Value, options.ed.Value, Convert.ToInt32(User.Identity.GetUserId())));
        //    }



        //}






        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {

            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startDate;
                options.ed = endDate;
            }

            ViewBag.filter = filter;

            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };


            if (filter.IsYarn == true)
            {
                return View(await _oGPService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
            }
            else
            {
               
                return View(await _oGPService.GetBetweenDateRangeFabric(options.sd.Value, options.ed.Value));
            }


        }


        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_OGP_CREATE_AND_UPDATE)]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            var ProcessTypeList = (await _ProcessActivityTypeService.GetAll()).ToSelectList();
            var PartyList = (await _uow.PartyService.GetAll()).ToSelectList();
            var YarnTypeList = (await _yarnTypeService.GetAll()).ToSelectList();
            var fabricTypeList = (await _fabricTypeService.GetAll()).ToSelectList();
            var buyerlist = (await _uow.BuyerService.GetAll()).ToSelectList();

            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            var vm = new OGPViewModel();

            if (id.HasValue) {
                vm = _mapper.Map<OGPViewModel>(await _oGPService.GetById(id.Value));

                try {
                    //fabricTypeList.Find(x => Convert.ToInt64(x.Value) == vm.FabricTypeId).Selected = true;
                    //YarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                    ProcessTypeList.Find(x => Convert.ToInt64(x.Value) == vm.ActivityTypeId).Selected = true;
                    PartyList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                }
                catch (Exception) {
                }
            }
            ViewBag.buyerlist = buyerlist;
            ViewBag.ProcessTypeList = ProcessTypeList;
            ViewBag.PartyList = PartyList;
            ViewBag.YarnTypeList = YarnTypeList;
            ViewBag.fabricTypeList = fabricTypeList;
            ViewBag.filter = filter;

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, [FromForm]OGPViewModel vm) {
            try {                                                                                             
                var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

                var m = _mapper.Map<OutwardGatePass>(vm);
                if (id.HasValue) {

                    if(filter.IsYarn == true)
                    {
                        vm.FabricTypeId = null;
                        await _oGPService.Update(m);
                    }
                    else
                    {               
                        vm.YarnTypeId = null;
                        await _oGPService.UpdateFabric(m);
                    }
                    
                } 
                
                else {

                    if (filter.IsYarn == true)
                    {

                                          
                     
                        var ogp= await _oGPService.Create(m);
                        await _uow.GateTrService.CreateBySno(new GateTr
                        {
                            IGPReffNo = ogp.OGPReffNO,
                            Xref = ogp.Id,
                            IsYarn = true,
                            BranchId = 1,
                            Date = ogp.OgpDate,
                            PartyId = ogp.PartyId,
                            NICNo = ogp.IDCard,
                            VehicleNo = ogp.VehicleNo,
                            GateActivityTypeId = 2,
                            BillityNo=ogp.BilityNo
                        });
                    }
                    else
                    {
                        m.YarnTypeId = null;
                   

                        var ogp = await _oGPService.CreateFabric(m);
                        await _uow.GateTrService.CreateBySno(new GateTr
                        {

                            IGPReffNo = ogp.OGPReffNO,
                            Xref = ogp.Id,
                            IsYarn = false,
                            BranchId = 2,
                            Date = ogp.OgpDate,
                            PartyId = ogp.PartyId,
                            NICNo = ogp.IDCard,
                            VehicleNo = ogp.VehicleNo,
                            GateActivityTypeId = 16,
                            BillityNo=ogp.BilityNo});

                    }

                }
                return RedirectToAction(nameof(OGPController.Details),  new { id = m.Id });

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> CopyReturnNoteData(long id) {

            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            ViewBag.filter = filter;
            return PartialView((await _uow.ReturnNoteService.GetById(id)).ReturnNoteDetails.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> CopyPPCData(long? id, long? OGPId ) {
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            ViewBag.filter = filter;
            var v = await _oGPService.GetById(OGPId.Value);
            bool reprocessed = v.IsReprocessed;
            bool recheck =  v.IsReCheck;
            bool returntoparty =  v.IsReturnToParty;
            bool afterfinishing =  v.IsAfterFinishing;
            bool forfinishing =  v.IsForFinishing;
            bool comercial =  v.IsAfterComercialFinishing;
            if (id.HasValue) {
                if (reprocessed == true) {
                    List<Reprocess> ppcp = await _reProcessService.GetReprocessesByLpsId(id.Value);
                    
                    await Task.WhenAll(ppcp.Select(async d =>
                    {
                        d.AvailableLpsKgs = d.Kgs;
                        var issues = (await  _uow.OGPDetailService.GetAll()).Where(x => x.ReprocessId == d.Id).ToList();
                        issues.ForEach(e =>
                        {
                            d.AvailableLpsKgs -= e.Kgs;
                        });
                    }));

                    return PartialView(_ViewPath + "/CopyReprocessData.cshtml", ppcp);
                }
                else

                 if (afterfinishing == true || comercial == true)
                {
                    var igp = (await _uow.IGPService.GetById(id.Value)).InwardGatePassDetails.ToList();

                    List<InwardGatePassDetail> inwardGatePassDetails = new List<InwardGatePassDetail>();

                    await Task.WhenAll(igp.Select(async d =>
                    {
                        var ppcp = await _uow.IGPDetailService.GetById(d.Id);
                        var ppcdata = await _uow.OGPDetailService.GetByIGPDetailId(ppcp.Id);
                        ppcp.AvailableLpsKgs = ppcp.NetWeightInKg;


                        ppcdata.ForEach(ig =>
                        {
                            ppcp.AvailableLpsKgs -= ig.Kgs;
                        });

                        inwardGatePassDetails.Add((ppcp));
                    }));

                    return PartialView(_ViewPath + "/CopyIGPData.cshtml", inwardGatePassDetails);
                }




                if (recheck == true || returntoparty)
                {//LPS an d IGP no both store in Xreferance column
                 //  var ppcp = _uow.IGPService.GetById(id.Value);
                    var igp = (await _uow.IGPService.GetById(id.Value)).InwardGatePassDetails.ToList();
         
                    List<InwardGatePassDetail> inwardGatePassDetails = new List<InwardGatePassDetail>();

                    await Task.WhenAll(igp.Select(async d =>
                    {
                        var ppcp = await _uow.IGPDetailService.GetById(d.Id);
                        var ppcdata = await _uow.PPCPlanningService.GetListbyIgpDetailId(ppcp.Id);

                        ppcp.AvailableLpsKgs = ppcp.NetWeightInKg;

                        await Task.WhenAll(ppcdata.Select(async f =>
                        {
                            ppcp.AvailableLpsKgs -= f.Kgs;
                            ppcp.InwardGatePassId = f.InwardGatePassDetailId;

                            var issues = await _uow.OGPDetailService.GetByIGPDetailId(ppcp.Id);
                            ////().Where(x => x.InwardGatePassDetailId == f.Id && x.OutwardGatePass.IsReCheck == true || x.OutwardGatePass.IsReturnToParty == true).ToList();

                            issues.ForEach(e =>
                            {
                                ppcp.AvailableLpsKgs -= e.Kgs;
                            });
                        }));

                        inwardGatePassDetails.Add((ppcp));

                    }));
             
                    return PartialView(_ViewPath + "/CopyIGPData.cshtml", inwardGatePassDetails);
                }

                else
                {
                   var rn = await _returnNoteDetailService.GetByXreffId(id.Value);
                    var ppcp = await _pPCPlanningService.GetById(id.Value);
                    //if (rn.Kgs==0)
                    //{
                    //    return new StatusCodeResult(404);
                    //}

                    ppcp.AvailableLpsKgs = ppcp.Kgs;

                    var issues = (await _uow.OGPDetailService.GetAll()).Where(x => x.PPCPlanningId == ppcp.Id && x.OutwardGatePass.IsReprocessed==false && x.OutwardGatePass.IsReprocessed == false).ToList();

                    issues.ForEach(d => {
                        ppcp.AvailableLpsKgs -= d.Kgs;
                    });

                    if (ppcp.IsYarn == true)
                    {
                        if (ppcp.YarnTypeId == v.YarnTypeId)
                        {
                            return PartialView(_ViewPath + "/CopyPPCData.cshtml", ppcp);
                        }
                    }
                    else
                    {
                       return PartialView(_ViewPath + "/CopyPPCData.cshtml", ppcp);
                    }

                }
            }

            return new StatusCodeResult(404);
        }
        [HttpGet]
        public async Task<IActionResult> CopyFactoryPoData(long? id, long? OGPId)
        {
            var igpDetailList = new List<InwardGatePassDetail>();
            var factorypodetail = new List<FactoryPoDetail>();
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (id.HasValue)
            {
                var factory = await _uow.FactoryPoService.GetById(id.Value);
               // var igp = _uow.IGPService.GetById(id.Value);
                //if (igp.IsReprocessed == false && igp.IsReWaxRecheck == false && igp.IsWithoutOGP == false && igp.IsAfterFinishing == false && igp.IsForComercialFinishing == false && igp.IsForFinishing == false)
                //{
                    factorypodetail = factory?.FactoryPoDetail.ToList();
                  // igpDetailList = igp?.InwardGatePassDetails.ToList()
                        //.Where(x => x.IsDeleted == false).ToList();

                    var ppc = await _uow.PPCPlanningService.GetAll();
                    var OGP = await _uow.OGPDetailService.GetAll();


                factorypodetail.ForEach(d =>
                {
                    d.NetWeightInKg = d.NetWeightInKg;

                   

                    OGP.Where(p => p.FactoryPoDetailId == d.Id).ToList().ForEach(p =>
                    {
                        d.NetWeightInKg -= p.Kgs;
                    });


                });
            }
        
        
       //     ViewBag.Po = Po;

            return PartialView(factorypodetail);

        }

        //[Authorize(Policy = AccountClaimKeys.PPC_OGP_DETAIL_VIEW)]
        public async Task<IActionResult> Details(long id)  {
            var m = await _oGPService.GetById(id);
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            if (m == null) return RedirectToAction(nameof(Index));

            
            var processActivityTypeList = (await _ProcessActivityTypeService.GetAll()).ToSelectList();
            var yarnTypeList = (await _yarnTypeService.GetAll()).ToSelectList();
            var fabricTypeList = (await _fabricTypeService.GetAll()).ToSelectList();

            var PartyList = (await _uow.PartyService.GetAll()).ToSelectList();

            processActivityTypeList.Find(x => Convert.ToInt64(x.Value) == m.ActivityTypeId).Selected = true;
            ////yarnTypeList.Find(x => Convert.ToInt64(x.Value) == m.YarnTypeId).Selected = true;
            PartyList.Find(x => Convert.ToInt64(x.Value) == m.PartyId).Selected = true;


            ViewBag.yarnTypeList = yarnTypeList;
            ViewBag.processActivityTypeList = processActivityTypeList;
            ViewBag.PartyList = PartyList;
            ViewBag.fabricTypeList = fabricTypeList;
            ViewBag.filter = filter;
            return View(m);
        }

        [HttpGet]
      //  [Authorize(Policy = AccountClaimKeys.PPC_OGP_CREATE_AND_UPDATE_DETAIL)]
        public async Task<IActionResult> AddOrUpdateDetails([FromRoute]long? id, [FromQuery] long ogpId) {
     
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var vm = new OGPDetailViewModel() {
                Id = id.HasValue ? id.Value : 0
            };
            var yarnTypeList =  (await _yarnTypeService.GetAll()).ToSelectList();
            var fabricTypeList = (await _fabricTypeService.GetAll()).ToSelectList();
          
            var buyerColorList = (await _buyerColorService.GetAll()).ToSelectList();
            var buyerList = (await _buyerService.GetAll()).ToSelectList();


            var fabricqualityList =  (await _uow.FabricQualityService.GetAll()).ToString();
            //var buyerColorList = _buyerColorService.GetAll().ToString();
            //var buyerList = _buyerService.GetAll().ToString();



            
            var ogpheader = await _oGPService.GetById(ogpId);
            if (id.HasValue) {
                var ogpDetail = await _OGPDetailService.GetById(id.Value);
                vm = _mapper.Map<OGPDetailViewModel>(ogpDetail);
                if (vm != null) {
                    if (filter.IsYarn == true)
                    {
                        yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                    }
                    else
                    {
                        fabricTypeList.Find(x => Convert.ToInt64(x.Value) == vm.FabricTypesId).Selected = true;
                    }
                }
            }

            vm.OutwardGatePassId = ogpId;
        //    var Po = _uow.FactoryPoService.GetAll().ToSelectList();
         //   ViewBag.Po = Po;
            ViewBag.ogpheader = ogpheader;
            ViewBag.yarnTypeList = yarnTypeList;
            ViewBag.fabricTypeList = fabricTypeList;
            ViewBag.fabricqualityList = fabricqualityList;
            ViewBag.buyerColorList = buyerColorList;
            ViewBag.buyerList = buyerList;
            ViewBag.filter = filter;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDetails(long? id, [FromForm] OGPDetailViewModel vm) {
            try {
              

                var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

                var m = _mapper.Map<OutwardGatePassDetail>(vm);
             
                var ogp = await _oGPService.GetById(vm.OutwardGatePassId.Value);
                m.Amount = vm.Rate * vm.Kgs;


                var ppc = await  _uow.PPCPlanningService.GetById(Convert.ToInt64(vm.Xref));
                //&& vm.PPCPlanningId != null
                if (ogp.IsReCheck==false && ogp.IsReprocessed==false && ogp.IsReturnToParty==false && ogp.IsAfterComercialFinishing == false && ogp.IsAfterFinishing == false && vm.Xref!= 0 && ogp.IsYarn==true )


                {

                    
                    if (id != null && id != 0)
                    {

                        var ogpdet= await _uow.OGPDetailService.GetById(Convert.ToInt64(id));
                        var lps = await _uow.PPCPlanningService.GetById(Convert.ToInt64(ogpdet.PPCPlanningId.Value));
                        m.PPCPlanningId = ogpdet.PPCPlanningId;
                        if (lps.IsYarn == true)
                        {
                            m.Description = $"{lps.PurchaseOrder.Po},{lps.LotNo}, {lps.BuyerColor.Buyer.Name}, {lps.BuyerColor.Name}, {lps.YarnType.Name}, {lps.YarnQuality.Name}";
                            await _OGPDetailService.Update(m);
                        }
                        //else
                        //{
                        
                        //    m.Description = $"{lps.PurchaseOrder.Po},{lps.LotNo}, {lps.BuyerColor.Buyer.Name}, {lps.BuyerColor.Name}, {lps.FabricType.Name} ";
                        //    _OGPDetailService.Update(m);
                        //}
                     
                    }
                    else
                    {
                      var   lps = await _uow.PPCPlanningService.GetById(Convert.ToInt64(vm.Xref));

                        if (lps.IsYarn == true)
                        {
                            m.Description = $"{lps.PurchaseOrder.Po},{lps.LotNo}, {lps.BuyerColor.Buyer.Name}, {lps.BuyerColor.Name}, {lps.YarnType.Name}, {lps.YarnQuality.Name}";

                        }
                        //else
                        //{
                        //    m.Description = $"{lps.PurchaseOrder.Po},{lps.LotNo}, {lps.BuyerColor.Buyer.Name}, {lps.BuyerColor.Name}, {lps.FabricType.Name} ";

                        //}
                        //m.Description = $"{lps.PurchaseOrder.Po},{lps.LotNo}, {lps.BuyerCo                    lor.Buyer.Name}, {lps.BuyerColor.Name}, {lps.YarnType.Name}, {lps.YarnQuality.Name}";
                        m.PPCPlanningId = vm.Xref;



                    }

                  
                }
                else if(ogp.IsReprocessed == true)
                {
                    var reprocess = await _uow.ReprocessService.GetById(Convert.ToInt64(vm.ReprocessId));
                   // m.Description = $"{reprocess.PPCPlanning.PurchaseOrder.Po},{reprocess.PPCPlanning.LotNo}, {reprocess.PPCPlanning.BuyerColor.Buyer.Name}, {reprocess.PPCPlanning.BuyerColor.Name}, {reprocess.PPCPlanning.YarnType.Name}, {reprocess.PPCPlanning.YarnQuality.Name}";
                    m.ReprocessId = reprocess.Id;
                    var lps = await _uow.PPCPlanningService.GetById(Convert.ToInt64(vm.Xref));
                    m.PPCPlanningId = vm.Xref;

                    if (lps.IsYarn == true)
                    {
                        m.Description = $"{lps.PurchaseOrder.Po},{lps.LotNo}, {lps.BuyerColor.Buyer.Name}, {lps.BuyerColor.Name}, {lps.YarnType.Name}, {lps.YarnQuality.Name}";

                    }
                    else
                    {
                        m.Description = $"{lps.PurchaseOrder.Po},{lps.LotNo}, {lps.BuyerColor.Buyer.Name}, {lps.BuyerColor.Name}, {lps.FabricType.Name} ";

                    }
                }
                else if(ogp.IsReturnToParty == true || ogp.IsReCheck == true || ogp.IsAfterComercialFinishing==true || ogp.IsAfterFinishing==true)
                {

                    if (id != null && id != 0)
                    {
                        var t = await _uow.IGPDetailService.GetById(vm.InwardGatePassDetailId.Value);

                        var xmi = await _uow.IGPService.GetById(t.InwardGatePassId.Value);

                        if (filter.IsYarn == true)
                        {
                            m.Description = $"{t.BuyerPO}, {t.LotNo}, {t.YarnQuality.Name}";
                        }
                        else
                        {
                            m.Description = $"{xmi.Buyer?.Name}, {t.BuyerPO}, {t.LotNo}, {t.FabricQuality.Name}, {"Dia: "}, {t.Dia}, {"GSM: "} {t.GSM}";
                        }

                        //m.Description = $"{xmi.Buyer?.Name}, {t.BuyerPO}, {t.LotNo}, {t.FabricQuality.Name}, {"Dia: "}, {t.Dia}, {"GSM: "} {t.GSM}";

                        m.InwardGatePassDetailId = t.Id;

                        await _OGPDetailService.Update(m);
                      
                    }

                    else
                    {
                        var t = await _uow.IGPDetailService.GetById(vm.InwardGatePassDetailId.Value);

                        var xmi = await _uow.IGPService.GetById(t.InwardGatePassId.Value);
                        if (filter.IsYarn == true)
                        {
                            m.Description = $"{t.BuyerPO}, {t.LotNo}, {t.YarnQuality.Name}";
                        }
                        else
                        {
                            m.Description = $"{xmi.Buyer?.Name}, {t.BuyerPO}, {t.LotNo}, {t.FabricQuality.Name}, {"Dia: "}, {t.Dia}, {"GSM: "} {t.GSM}";
                        }
                        m.InwardGatePassDetailId = t.Id;


                    }





                    //if (i.IsYarn == true)
                    //{
                    //    m.Description = $"{t.FirstOrDefault().BuyerPO},{t.FirstOrDefault().LotNo},{t.FirstOrDefault().LotNo},{t.FirstOrDefault().LotNo},{t.FirstOrDefault().LotNo},{t.FirstOrDefault().LotNo},";

                    //}
                    //else
                    //{
                    //    m.Description = $"{i.Buyer.Name},{i.Party.Name}, {lps.BuyerColor.Buyer.Name}, {lps.BuyerColor.Name}, {lps.FabricType.Name} ";

                    //}




                }
                else
                {
                    //if (filter.IsYarn == false)
                    //{
                    //    var ppccp = _uow.PPCPlanningService.GetById(vm.Xref);
                    //    var ppcccp = _uow.PPCPlanningService.GetById(vm.Xref);
                    //    var buyer = _uow.BuyerService.GetById(vm.BuyerId.Value);
                    //    var buyercolor = _uow.BuyerColorService.GetById(vm.BuyerColorId.Value);
                    //    m.Description = $"{buyer.Name}, {buyercolor.Name}, {vm.LotNo}, {vm.PO}";
                    //}
                    if (filter.IsYarn == false)
                    {

                      //  var factpodet= _uow.FactoryPoDetailService.GetById(vm.Xref);
                        //var factpo = await _uow.FactoryPoService.GetById(vm.Xref);
                        var buyer1 = await _uow.BuyerService.GetById(vm.BuyerId.Value);
                        var buyercolor1 = await _uow.BuyerColorService.GetById(vm.BuyerColorId.Value);
                        m.Description = $"{buyer1.Name}, {buyercolor1.Name}, {vm.LotNo}, {vm.PO}, {"Po:"} {vm.PO}";
                        var any= await _uow.ReturnNoteService.CheckLotNo(vm.BuyerId, vm.LotNo);
                        //m.FactoryPoDetailId = await _uow.FactoryPoDetailService.GetPoDetailId(vm.PO, vm.BuyerColorId.Value, vm.FabricTypesId);
                        m.PPCPlanningId = vm.Xref;

                    }


                }
                if (id == 0)
                {
                    m.Amount = vm.Rate * vm.Kgs;
                    
                  var ogpdet = await _OGPDetailService.Create(m);

                    if (filter.IsYarn == true)
                    {

                        var header = await _uow.GateTrService.GetOGPById(ogpdet.OutwardGatePassId.Value);

                        await _uow.GateTrDetailService.Create(new GateTrDetail
                        {
                            GateTrId = header.Id,
                            YarnTypeId = ogpdet.YarnTypeId,
                            QtyCr = ogpdet.Kgs,
                            NoOfRolls = ogpdet.NoOfRolls,
                            FabricTypeId = null
                        });
                    }
                    else
                    {
                        var header = await _uow.GateTrService.GetOGPById(ogpdet.OutwardGatePassId.Value);

                           await _uow.GateTrDetailService.Create(new GateTrDetail
                    {
                               GateTrId= header.Id,
                        FabricTypeId = ogpdet.FabricTypesId,
                        QtyCr = ogpdet.Kgs,
                        NoOfRolls = ogpdet.NoOfRolls,
                        YarnTypeId = null
                    }) ;



                    }
                 


                }
                else
                {
                    var detail = await _OGPDetailService.GetById(id.Value);
                    detail.IsComplete = vm.IsComplete;
                    await _OGPDetailService.Update(detail);
                }

                //if (ogp.OutwardGatePassDetails.Count() > 0) {
                //    if (ogp.OutwardGatePassDetails.Where(x => x.Id == m.Id).Count() == 0) {
                //        var d = _OGPDetailService.Create(m);
                //        ogp.OutwardGatePassDetails.Add(d);
                //        _oGPService.Update(ogp);
                //    }

                //    else
                //    {
                //        _OGPDetailService.Update(m);
                //    }
                //}
                //else {
                //    var d = _OGPDetailService.Create(m);
                //    ogp.OutwardGatePassDetails.Add(d);
                //    _oGPService.Update(ogp);

                //}
            }
            catch (Exception ex) {
                throw ex;
            }

            return RedirectToAction(nameof(OGPController.Details), "OGP", new { id = vm.OutwardGatePassId });
        }

        [HttpPost]
       // [Authorize(Policy = AccountClaimKeys.PPC_OGP_DELETE_DETAIL)]
        public async Task<IActionResult> DeleteDetails(long id) {
            try {
                await _OGPDetailService.Delete(await _OGPDetailService.GetById(id));
                return new StatusCodeResult(200);
            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }
        }


        [HttpGet]
        public async Task<IActionResult> OGPDetailReport(long id)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (filter == null) return NotFound("Report filter doesn't have any record.");
            var data = await _uow.OGPService.GetById(id);
            var r = new OutwardGatePassReportViewModel();

            r.CompanyName = _companySettings.Value.CompanyName;
            r.CompanyBranch = filter.BranchName;
            r.Id = data.Id;
            r.OgpDate = data.OgpDate;
            r.IsReturnToParty = data.IsReturnToParty;
            r.InvoiceNo = data.InvoiceNo;
            r.InvoiceAmount = data.InvoiceAmount;
            r.InvoiceDescription = data.InvoiceDescription;
            r.IsConfirm = data.IsConfirm;
            r.IsForFinishing = data.IsForFinishing;
            r.IsAfterFinishing = data.IsAfterFinishing;
            r.IsReprocessed = data.IsReprocessed;
            r.IsCancel = data.IsCancel;
            r.IsReWaxRecheck = data.IsReWaxRecheck;
            r.IsAfterComercialFinishing = data.IsAfterComercialFinishing;
            r.ActivityTypeId = data.ActivityTypeId;
            r.ActivityType = data.ActivityType;
            r.YarnTypeId = data.YarnTypeId;
            r.YarnType = data.YarnType;
            r.ReceivingPerson = data.ReceivingPerson;
            r.VehicleNo = data.VehicleNo;
            r.IDCard = data.IDCard;
            r.IsReCheck = data.IsReCheck;
            r.PartyId = data.PartyId;
            r.Party = data.Party;
            r.OutwardGatePassDetails = data.OutwardGatePassDetails;
            r.FabricTypeId = data.FabricTypeId;
            r.FabricTypes = data.FabricTypes;
            r.BranchId = data.BranchId;
            r.Branches = data.Branches;
            r.IsYarn = data.IsYarn;
            r.OGPReffNO = data.OGPReffNO;
            r.BilityNo = data.BilityNo;
            r.SerialNo = data.SerialNo;
            r.TotalWeight = data.TotalWeight;
            r.LotNo = data.LotNo;
            r.BuyerId = data.BuyerId;
            r.Buyer = data.Buyer;


        string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            return new ViewAsPdf(r)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            };
        }
    }
}