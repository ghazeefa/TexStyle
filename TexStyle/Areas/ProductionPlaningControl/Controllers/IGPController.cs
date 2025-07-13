using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TexStyle.ApplicationServices.Interfaces.IGate;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class 
        IGPController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/IGP";

        private readonly TempDataViewModel _tempData;
        private readonly IActivityTypeService _orderActivityTypeService;
        private readonly IYarnTypeService _yarnTypeService;
        private readonly IYarnQualityService _yarnQualityService;
        private readonly IYarnManufacturerService _yarnManufacturerService;
        private readonly IBagMarkingService _bagMarkingService;
        private readonly IConeMarkingService _coneMarkingService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPartyService _partyService;
        private readonly IGateTrService _gateTrService;
      //  private readonly IGateOgpService _gateOgpService;
        private readonly IIGPService _igpService;
        private readonly IIGPDetailService _igpDetailService;
        private readonly IMapper _mapper;
        private readonly IGateActivityTypeService _gateActivityTypeService;
        private readonly IReportFilterService _reportFilterService;
        private readonly IFabricQualityService _fabricQualityService;
        private readonly IFabricTypesService _fabricTypeService;
        private readonly IRollMarkingService _rollMarkingService; 
        private readonly IknittingPartyService _knittingPartyService;

        public IGPController(IMapper mapper,
            IGateTrService gateTrService,
         //   IGateOgpService gateogpService,

            IIGPService igpService,
            IIGPDetailService igpDetailService,
            IActivityTypeService orderActivityTypeService,
            IYarnTypeService yarnTypeService,
            IYarnQualityService yarnQualityService,
            IYarnManufacturerService yarnManufacturerService,
            IBagMarkingService bagMarkingService,
            IConeMarkingService coneMarkingService,
            IPurchaseOrderService purchaseOrderService,
            IPartyService partyService,
            IGateActivityTypeService gateActivityTypeService,
                    IReportFilterService ReportFilterService,
                      IFabricQualityService fabricQualityService,
        IFabricTypesService fabricTypeService,
        IRollMarkingService rollMarkingService,
        IknittingPartyService knittingPartyService
            ) {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / Inward Gate Pass";


            _mapper = mapper;
            _gateTrService = gateTrService;
            //_gateOgpService = gateogpService;
            _igpService = igpService;
            _igpDetailService = igpDetailService;
            _orderActivityTypeService = orderActivityTypeService;
            _yarnTypeService = yarnTypeService;
            _yarnQualityService = yarnQualityService;
            _yarnManufacturerService = yarnManufacturerService;
            _bagMarkingService = bagMarkingService;
            _coneMarkingService = coneMarkingService;
            _purchaseOrderService = purchaseOrderService;
            _partyService = partyService;
            _gateActivityTypeService = gateActivityTypeService;
            _reportFilterService = ReportFilterService;
            _fabricQualityService = fabricQualityService;
            _fabricTypeService = fabricTypeService;
            _rollMarkingService = rollMarkingService;
           _knittingPartyService = knittingPartyService;

            _tempData = new TempDataViewModel();
        }

        //public IActionResult Index() {

        //    try {
        //        var lst = _igpService.GetAll();
        //        lst.Reverse();
        //        return View(lst);
        //    } catch (Exception ex) {
        //        throw ex;
        //    }
        //}

        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_IGP_VIEW)]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {

            var filter =(await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
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

                return View(await _igpService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
            }
             else
            {

                return View(await _igpService.GetBetweenDateRangeFabric(options.sd.Value, options.ed.Value));
            }

           
        }

        [Authorize(Policy = AccountClaimKeys.PPC_IGP_DETAIL_VIEW)]
        public async Task<IActionResult> Details(long id)
        
        {

            var filter =(await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();



            var m =await _igpService.GetById(id);

            if (m == null) return RedirectToAction(nameof(Index));
            var knittingPartyList =(await _knittingPartyService.GetAll()).ToSelectList();

         //   var processActivityTypeList = _orderActivityTypeService.GetAll().ToSelectList();

            var bagMarkingList =(await _bagMarkingService.GetAll()).ToSelectList();
            var coneMarkingList =(await _coneMarkingService.GetAll()).ToSelectList();
            var yarnTypeList =(await _yarnTypeService.GetAll()).ToSelectList();
            var yarnQualityList =(await _yarnQualityService.GetAll()).ToSelectList();

            var fabricTypeList =(await _fabricTypeService.GetAll()).ToSelectList();
            var fabricQualityList =(await _fabricQualityService.GetAll()).ToSelectList();
            var rollMarkingList =(await _rollMarkingService.GetAll()).ToSelectList();
            var processActivityTypeList =(await _orderActivityTypeService.GetAll()).ToSelectList();
            // var gateogpList = _gateOgpService.GetAll().ToSelectList();
            var gateigpList =(await _gateTrService.GetAll()).ToSelectList(nameof(GateTr.Sno));

            if (m != null) {
              //  processActivityTypeList.Find(x => Convert.ToInt64(x.Value) == m.ActivityTypeId).Selected = true;
                if(m.GateTrId.HasValue)
                //gateigpList.Find(x => Convert.ToInt64(x.Value) == m.GateIgpId).Selected = true;
                gateigpList.Find(x => Convert.ToInt64(x.Value) == m.GateTrId).Selected = true;
                //processActivityTypeList.Find(x => Convert.ToInt64(x.Value) == m.ActivityTypeId).Selected = true;
            }
            //bagMarkingList.Find(x => Convert.ToInt64(x.Value) == m.BagMarkingId).Selected = true;
            //coneMarkingList.Find(x => Convert.ToInt64(x.Value) == m.ConeMarkingId).Selected = true;
            //yarnTypeList.Find(x => Convert.ToInt64(x.Value) == m.YarnTypeId).Selected = true;
            //yarnQualityList.Find(x => Convert.ToInt64(x.Value) == m.YarnQualityId).Selected = true;

            //ViewBag.ogpList = gateogpList;
            ViewBag.gateIgpList = gateigpList;
            ViewBag.bagMarkingList = bagMarkingList;
            ViewBag.coneMarkingList = coneMarkingList;
            ViewBag.yarnTypeList = yarnTypeList;
            ViewBag.yarnQualityList = yarnQualityList;
           // ViewBag.processActivityTypeList = processActivityTypeList;
            ViewBag.rollMarkingList = rollMarkingList;
            ViewBag.fabricTypeList = fabricTypeList;
       
            ViewBag.fabricQualityList = fabricQualityList;
            ViewBag.knittingPartyList = knittingPartyList;
            ViewBag.processActivityTypeList = processActivityTypeList;

            ViewBag.filter = filter;

            return View(m);
        }

        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_IGP_CREATE_AND_UPDATE)]

        public async Task<IActionResult> AddOrUpdate(long? id) {
            var filter =(await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            IGPViewModel vm = new IGPViewModel();
          //  var processActivityTypeList = _orderActivityTypeService.GetAll().ToSelectList();
            var bagMarkingList =(await _bagMarkingService.GetAll()).ToSelectList();
            var coneMarkingList = (await _coneMarkingService.GetAll()).ToSelectList();
            var yarnTypeList = (await _yarnTypeService.GetAll()).ToSelectList();
            var yarnQualityList = (await _yarnQualityService.GetAll()).ToSelectList();
            var partyTypeList = (await _partyService.GetAll()).ToSelectList();
            var fabricTypeList = (await _fabricTypeService.GetAll()).ToSelectList();
            var fabricQualityList = (await _fabricQualityService.GetAll()).ToSelectList();
            var rollMarkingList = (await _rollMarkingService.GetAll()).ToSelectList();
            //  var gateogpList = _gateOgpService.GetAll().ToSelectList();
            //   var gateigpList = _gateIgpService.GetAll().ToSelectList();
            //  var idact= _gateActivityTypeService.GetByName(GateActivityTypes.IGP_Yarn);
            if (filter.IsYarn == true)
            {
                var gateigpList =(await _gateTrService.GetAllByActivityId((await _gateActivityTypeService.GetByName(GateActivityTypes.IGP_Yarn)).Id)).ToSelectList(nameof(GateTr.Sno));
                if (id.HasValue)
                {
                    vm = _mapper.Map<IGPViewModel>(await _igpService.GetById(id.Value));

                  //  processActivityTypeList.Find(x => Convert.ToInt64(x.Value) == vm.ActivityTypeId).Selected = true;
                    partyTypeList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                    //if (vm.GateOGPId != null)
                    //gateogpList.Find(x => Convert.ToInt64(x.Value) == vm.GateOGPId).Selected = true;
                    if (vm.GateTrId != null)
                        gateigpList.Find(x => Convert.ToInt64(x.Value) == vm.GateTrId).Selected = true;
                    //bagMarkingList.Find(x => Convert.ToInt64(x.Value) == vm.BagMarkingId).Selected = true;
                    //coneMarkingList.Find(x => Convert.ToInt64(x.Value) == vm.ConeMarkingId).Selected = true;
                    //yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                    //yarnQualityList.Find(x => Convert.ToInt64(x.Value) == vm.YarnQualityId).Selected = true;
                }
                else
                {
                    var item =(await _igpService.GetAll()).Count;
                    vm.Id = item == 0 ? 1 : ((await _igpService.GetAll()).LastOrDefault().Id + 1);
                }

                //ViewBag.ogpList = gateogpList;
                ViewBag.gateIgpList = gateigpList;
               // ViewBag.processActivityTypeList = processActivityTypeList;
                ViewBag.bagMarkingList = bagMarkingList;
                ViewBag.coneMarkingList = coneMarkingList;
                ViewBag.yarnTypeList = yarnTypeList;
                ViewBag.yarnQualityList = yarnQualityList;
                ViewBag.partyTypeList = partyTypeList;

                return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
            }
            else
            {
                var gateigpList =(await _gateTrService.GetAllByActivityId((await _gateActivityTypeService.GetByName(GateActivityTypes.IGP_Fabric)).Id)).ToSelectList(nameof(GateTr.Sno));

                if (id.HasValue)
                {
                    vm = _mapper.Map<IGPViewModel>(await _igpService.GetById(id.Value));

                  //  processActivityTypeList.Find(x => Convert.ToInt64(x.Value) == vm.ActivityTypeId).Selected = true;
                    partyTypeList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                    //if (vm.GateOGPId != null)
                    //gateogpList.Find(x => Convert.ToInt64(x.Value) == vm.GateOGPId).Selected = true;
                    if (vm.GateTrId != null)
                        gateigpList.Find(x => Convert.ToInt64(x.Value) == vm.GateTrId).Selected = true;
                    //bagMarkingList.Find(x => Convert.ToInt64(x.Value) == vm.BagMarkingId).Selected = true;
                    //coneMarkingList.Find(x => Convert.ToInt64(x.Value) == vm.ConeMarkingId).Selected = true;
                    //yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                    //yarnQualityList.Find(x => Convert.ToInt64(x.Value) == vm.YarnQualityId).Selected = true;
                }
                else
                {
                    var item =(await _igpService.GetAll()).Count; 
                    vm.Id = item == 0 ? 1 : ((await _igpService.GetAll()).LastOrDefault().Id + 1);
                }

                //ViewBag.ogpList = gateogpList;
                ViewBag.gateIgpList = gateigpList;
                //ViewBag.processActivityTypeList = processActivityTypeList;
                ViewBag.bagMarkingList = bagMarkingList;
                ViewBag.coneMarkingList = coneMarkingList;
                ViewBag.yarnTypeList = yarnTypeList;
                ViewBag.yarnQualityList = yarnQualityList;
                ViewBag.partyTypeList = partyTypeList;
                ViewBag.filter = filter;

                return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
            }


            //var processTypeList = _pro

         
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, IGPViewModel vm) {

            var filter =(await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            //if (ModelState.IsValid) {
            InwardGatePassDetail d = new InwardGatePassDetail();
            InwardGatePass igp = new InwardGatePass();
            try {
                vm.Id = null;
                var m = _mapper.Map<InwardGatePass>(vm);    
                var md= await _gateTrService.GetById(Convert.ToInt64(m.GateTrId));
                if (!id.HasValue) 
                {

               //     var filter = _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).FirstOrDefault();
                    //m.IGPDetailId = null;
                    // create

                    m.PartyId = md.PartyId;
                    m.CreatedOn = DateTime.Now;
                    if (md.IsReturnFromParty == true) 
                        m.IsReturnfromParty = true;
                    if (filter.IsYarn == true)
                    {
                        var igpm = _igpService.Create(m);
                        foreach (var igpd in md.GateTrDetails)

                        {
                            //var filter = _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).FirstOrDefault();

                        }
                    }
                    else
                    {
                      

                        var igpf =await _igpService.CreateFabric(m);

                        await Task.WhenAll(md.GateTrDetails.Select(async igpd =>
                        {
                            await _igpDetailService.Create(new InwardGatePassDetail
                            {
                                InwardGatePassId = igpf.Id,
                                TearWeightInKg = 0,
                                NetWeightInKg = Convert.ToDecimal(igpd.QtyDr),

                                NoOfRolls = Convert.ToDecimal(igpd.NoOfRolls),
                                FabricTypesId = Convert.ToInt64(igpd.FabricTypeId)

                            });

                        }));
                    }

 
                    _tempData.MSG = "Successfully Created";
                } 
                
                else {
                    //update

                    //ab yaha par wo again set karwa raha hai aysa hi hai? yess mam buss pir yahi set karwa dain condiation laga k 
                    //aur yaha parr
                    m.Id = id.Value;
                    m.PartyId = md.PartyId;
                    var o =await _igpService.GetById(m.Id);
                    if (md.IsReturnFromParty == true)
                    { 
                        m.IsReturnfromParty = true;
                        if (md.IsWithoutOGP == true)
                        { 
                            m.IsReWaxRecheck = true; 
                            m.IsWithoutOGP= true; 
                            m.IsReprocessed = false; 
                        }
                        else
                        {
                            m.IsReWaxRecheck = false;
                            m.IsWithoutOGP = false;
                            m.IsReprocessed = false;
                        }
                    

                    }
                    else
                    {
                        m.IsReWaxRecheck = false; 
                        m.IsReprocessed = false;
                        m.IsWithoutOGP = false;
                        m.IsReturnfromParty = false; 

                    }
                    if (filter.IsYarn==true)
                    {

                        igp =await _igpService.Update(m);
                    }
                    else
                    {
                        await _igpService.UpdateFabric(m);
                    }
                  
                  

                    if(o.GateTrId != igp.GateTrId)
                    {
                        //m.IsReWaxRecheck = false;
                        //m.IsReprocessed = false;

                        await Task.WhenAll(o.InwardGatePassDetails.Select(async de =>
                        {
                            await _igpDetailService.Delete(de);
                        }));

                        await Task.WhenAll(md.GateTrDetails.Select(async igpd =>
                        {
                            if (filter.IsYarn == false)
                            {
                                await _igpDetailService.Create(new InwardGatePassDetail
                                {
                                    InwardGatePassId = igp.Id,
                                    NetWeightInKg = Convert.ToDecimal(igpd.QtyDr),
                                    NoOfRolls = Convert.ToDecimal(igpd.NoOfRolls),
                                    FabricTypesId = Convert.ToInt64(igpd.FabricTypeId)

                                });

                            }

                            else
                            {
                                await _igpDetailService.Create(new InwardGatePassDetail
                                {
                                    InwardGatePassId = igp.Id,
                                    NetWeightInKg = Convert.ToDecimal(igpd.QtyDr),
                                    Bags = Convert.ToDecimal(igpd.Bags),
                                    YarnTypeId = Convert.ToInt64(igpd.YarnTypeId)

                                });
                            }
                        }));
                    }
                   
                                    
                    _tempData.MSG = "Successfully Updated";
                }


                return RedirectToAction(nameof(IGPController.Details), "IGP", new { id = m.Id });


            }
            catch (Exception ex) {
                _tempData.Error = ex.Message;
            }
            //}

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Policy = AccountClaimKeys.PPC_IGP_DELETE)]
        public async Task<IActionResult> Delete(long? id, IFormCollection col) {
            try {
                if (id.HasValue) 
                {
                    await _igpService.Delete(await _igpService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            } catch (Exception) 
            {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }

        [HttpGet]
        public async Task<IActionResult> IGPDetailReport(long id)
        {
            if (id == 0) return BadRequest();
            var igp =await _igpService.GetById(id);
            
            return View(igp);
        }
    }
}