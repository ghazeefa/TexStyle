using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class IGPDetailController : Controller
    {

        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/IGPDetail";

        private readonly TempDataViewModel _tempData;
        private readonly IPurchaseOrderService _purchaseOrderService;
        //private readonly IProcessActivityTypeService _processActivityTypeService;
        private readonly IYarnManufacturerService _yarnManufacturerService;
        private readonly IYarnTypeService _yarnTypeService;
        private readonly IYarnQualityService _yarnQualityService;
        private readonly IBagMarkingService _bagMarkingService;
        private readonly IConeMarkingService _coneMarkingService;
        private readonly IStoreLocationService _storeLocationService;
        private readonly IFabricQualityService _fabricQualityService;
        private readonly IFabricTypesService _fabricTypeService;
        private readonly IRollMarkingService _rollMarkingService;
        private readonly IIGPService _igpService;
        private readonly IIGPDetailService _igpDetailService;
        private readonly IMapper _mapper;
        private readonly IReportFilterService _reportFilterService;
        private readonly IknittingPartyService _knittingPartyService;
        private readonly IActivityTypeService _orderActivityTypeService;



        public IGPDetailController(IMapper mapper,
            IIGPService igpService,

            IIGPDetailService igpDetailService,
            //IProcessActivityTypeService processActivityTypeService,
            IYarnTypeService yarnTypeService,
            IYarnQualityService yarnQualityService,
            IYarnManufacturerService yarnManufacturerService,
            IBagMarkingService bagMarkingService,
            IConeMarkingService coneMarkingService,
            IStoreLocationService storeLocationService,
            IPurchaseOrderService purchaseOrderService,
        IFabricQualityService fabricQualityService,
        IFabricTypesService fabricTypeService,
        IRollMarkingService rollMarkingService,
                IReportFilterService reportFilterService,
                IknittingPartyService knittingPartyService,
              IActivityTypeService orderActivityTypeService
            )
        {

            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / Inward Gate Pass Details";

            _mapper = mapper;
            _igpService = igpService;
            _igpDetailService = igpDetailService;
            _purchaseOrderService = purchaseOrderService;
            _yarnManufacturerService = yarnManufacturerService;
            _yarnTypeService = yarnTypeService;
            _storeLocationService = storeLocationService;
            _yarnQualityService = yarnQualityService;
            _bagMarkingService = bagMarkingService;
            _coneMarkingService = coneMarkingService;

            _fabricQualityService = fabricQualityService;
            _fabricTypeService = fabricTypeService;
            _rollMarkingService = rollMarkingService;
            _reportFilterService = reportFilterService;
            _knittingPartyService = knittingPartyService;
            _orderActivityTypeService = orderActivityTypeService;



            //_processActivityTypeService = processActivityTypeService;
            _tempData = new TempDataViewModel();
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_IGP_DETAIL_CREATE_AND_UPDATE)]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, [FromQuery] long? igpId)
        {
            var filter =(await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            IGPDetailViewModel vm = new IGPDetailViewModel()
            {
                Id = id.HasValue ? id.Value : 0
            };
            var igpheader =await _igpDetailService.GetById(igpId.Value);
            var bagMarkingList =(await _bagMarkingService.GetAll()).ToSelectList();
            var coneMarkingList = (await _coneMarkingService.GetAll()).ToSelectList();
            var yarnTypeList = (await _yarnTypeService.GetAll()).ToSelectList();
            var yarnQualityList = (await _yarnQualityService.GetAll()).ToSelectList();
            var yarnManufacturerList = (await _yarnManufacturerService.GetAll()).ToSelectList();
            var storeLocationList = (await _storeLocationService.GetAll()).ToSelectList();

            var fabricTypeList = (await _fabricTypeService.GetAll()).ToSelectList();   
            var fabricQualityList = (await _fabricQualityService.GetAll()).ToSelectList();
            var rollMarkingList = (await _rollMarkingService.GetAll()).ToSelectList();
            var knittingPartyList = (await _knittingPartyService.GetAll()).ToSelectList();
            var processActivityTypeList = (await _orderActivityTypeService.GetAll()).ToSelectList();





            //var purchaseOrderList = _purchaseOrderService.GetAll().ToSelectList();

             if (id.HasValue)
            {
                var igpDetail =await _igpDetailService.GetById(id.Value);
                vm = _mapper.Map<IGPDetailViewModel>(igpDetail);
                if (vm != null)
                {
                    if (filter.IsYarn == true)
                    {
                        if (vm.BagMarkingId != null) bagMarkingList.Find(x => Convert.ToInt64(x.Value) == vm.BagMarkingId).Selected = true;
                        if (vm.ConeMarkingId != null) coneMarkingList.Find(x => Convert.ToInt64(x.Value) == vm.ConeMarkingId).Selected = true;
                        if (vm.YarnQualityId != null) yarnQualityList.Find(x => Convert.ToInt64(x.Value) == vm.YarnQualityId).Selected = true;
                        if (vm.YarnTypeId != null) yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                        if (vm.StoreLocationId != null) yarnManufacturerList.Find(x => Convert.ToInt64(x.Value) == vm.YarnManufacturerId).Selected = true;

                    }


                    else
                    {
                        if (vm.FabricQualityId != null) fabricQualityList.Find(x => Convert.ToInt64(x.Value) == vm.FabricQualityId).Selected = true;
                        if (vm.FabricTypesId != null) fabricTypeList.Find(x => Convert.ToInt64(x.Value) == vm.FabricTypesId).Selected = true;
                        if (vm.KnitingPartyId != null) knittingPartyList.Find(x => Convert.ToInt64(x.Value) == vm.KnitingPartyId).Selected = true;



                    }
                    if (vm.ActivityTypeId != null) processActivityTypeList.Find(x => Convert.ToInt64(x.Value) == vm.ActivityTypeId).Selected = true;
                    if (vm.StoreLocationId.HasValue)
                        storeLocationList.Find(x => Convert.ToInt64(x.Value) == vm.StoreLocationId).Selected = true;

                   // processActivityTypeList.Find(x => Convert.ToInt64(x.Value) == vm.ActivityTypeId).Selected = true;
                    //purchaseOrderList.Find(x => Convert.ToInt64(x.Value) == vm.PurchaseOrderId).Selected = true;

                    //if (vm.FabricQualityId != 0) fabricQualityList.Find(x => Convert.ToInt64(x.Value) == vm.FabricQualityId).Selected = true;
                    //if (vm.FabricTypesId != 0) fabricTypeList.Find(x => Convert.ToInt64(x.Value) == vm.FabricTypesId).Selected = true;
                    //if (vm.RollMarkingId != 0) rollMarkingList.Find(x => Convert.ToInt64(x.Value) == vm.RollMarkingId).Selected = true;


                }
            }

            vm.InwardGatePassId = igpId;
            ViewBag.bagMarkingList = bagMarkingList;
            ViewBag.igpheader = igpheader;
            ViewBag.storeLocationList = storeLocationList;
            ViewBag.coneMarkingList = coneMarkingList;
            ViewBag.yarnTypeList = yarnTypeList;
            ViewBag.yarnQualityList = yarnQualityList;
            ViewBag.yarnManufacturerList = yarnManufacturerList;
            ViewBag.rollMarkingList = rollMarkingList;
            ViewBag.fabricTypeList = fabricTypeList;
            ViewBag.fabricQualityList = fabricQualityList;
            ViewBag.filter = filter;
            ViewBag.knittingPartyList = knittingPartyList;
            ViewBag.processActivityTypeList = processActivityTypeList;
            //ViewBag.purchaseOrderList = purchaseOrderList;

            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, [FromForm] IGPDetailViewModel vm)
        {
            var filter =(await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<InwardGatePassDetail>(vm);
                //    m.GrossWeightInKg = m.TearWeightInKg + m.NetWeightInKg;
                    var igp =await _igpService.GetById(vm.InwardGatePassId.Value);
                  //  m.Sno = igp.InwardGatePassDetails.Count() + 1;

                    if (igp.InwardGatePassDetails.Count() > 0)
                    {
                        if (igp.InwardGatePassDetails.Where(x => x.Id == m.Id).Count() == 0)
                        {
                            var igpDetail = _igpDetailService.Create(m);
                        }
                        else
                        {
                            if (filter.IsYarn == true)
                            {
                                m.FabricQualityId= null;
                                m.FabricTypesId= null;
                                m.knittingParty= null;
                            


                                await _igpDetailService.Update(m);
                            }

                            else
                            {
                                m.YarnManufacturerId= null;
                                m.YarnQualityId = null;
                                m.YarnTypeId = null;
                                m.ConeMarkingId = null;
                                m.BagMarkingId = null;
                           

                                await _igpDetailService.Update(m);
                            }


                            
                        }
                    }
                    else
                    {
                        var igpDetail =await _igpDetailService.Create(m);
                    }
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                }
            }
            return RedirectToAction(nameof(IGPController.Details), "IGP", new { id = vm.InwardGatePassId });
        }

        [HttpPost]
        [Authorize(Policy = AccountClaimKeys.PPC_IGP_DELETE)]
        public async Task<IActionResult> Delete([FromRoute]long? id, [FromQuery] long? sno, [FromQuery] long? igpId)
        {
            try
            {
                if (id.HasValue && sno.HasValue)
                {
                    await _igpDetailService.Delete(await _igpDetailService.GetById(id.Value, sno.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
    }
}