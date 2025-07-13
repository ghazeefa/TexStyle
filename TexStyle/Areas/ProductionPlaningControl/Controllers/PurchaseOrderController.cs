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

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class PurchaseOrderController : Controller {

        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(PurchaseOrder)}";

        private readonly IYarnTypeService _yarnTypeService;
        private readonly IYarnQualityService _yarnQualityService;
        private readonly IPartyService _partyService;
        private readonly IBuyerService _buyerService;
        private readonly IBuyerColorService _buyerColorService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly ISeasonService _seasonService;
        private readonly IMapper _mapper;
        private readonly IReportFilterService _reportFilterService;
        private readonly IFabricTypesService _FabricTypesService;
        private readonly IFabricQualityService _FabricQualityService;
 


        public PurchaseOrderController(
            IYarnTypeService yarnTypeService,
            IYarnQualityService YarnQualityService,
            IPartyService partyService,
            IBuyerService buyerService,
            IBuyerColorService buyerColorService,
            IPurchaseOrderService purchaseOrderService, ISeasonService seasonService,
        IMapper mapper,
         IReportFilterService ReportFilterService,
                 IFabricQualityService FabricQualityService, IFabricTypesService FabricTypesService) {

            _yarnTypeService = yarnTypeService;
            _yarnQualityService = YarnQualityService;
            _partyService = partyService;
            _buyerService = buyerService;
            _buyerColorService = buyerColorService;
            _purchaseOrderService = purchaseOrderService;
            _seasonService = seasonService;
            _mapper = mapper;
            _reportFilterService = ReportFilterService;
            _FabricTypesService = FabricTypesService;
            _FabricQualityService = FabricQualityService;


        }


        // private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(BuyerColor)}/";

        //public IActionResult Index() {
        //    return View(_purchaseOrderService.GetAll().ToList());
        //}
        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_PURCHASE_ORDER_VIEW)]
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
                return View(await _purchaseOrderService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
            }
            else
            {

                return View(await _purchaseOrderService.GetBetweenDateRangeFabric(options.sd.Value, options.ed.Value));
            }

           


            
        }


        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_PURCHASE_ORDER_CREATE_AND_UPDATE)]
        public async Task<IActionResult> AddOrUpdate(long? id) {

            var filter =  (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

     
           

            PurchaseOrderViewModel vm = new PurchaseOrderViewModel();
            var FabricTypesList = (await _FabricTypesService.GetAll()).ToSelectList();
            var FabricQualityList =  (await _FabricQualityService.GetAll()).ToSelectList();
            var yarnTypeList = (await _yarnTypeService.GetAll()).ToSelectList();
            var yarnQualityList = (await _yarnQualityService.GetAll()).ToSelectList();
            var partyList = (await _partyService.GetAll()).ToSelectList();
            var seasonList = (await _seasonService.GetAll()).ToSelectList();
            var buyerColorList = (await _buyerColorService.GetAll()).ToSelectList();
            var buyerList = ((await _buyerService.GetAll())).ToSelectList();
            if (id.HasValue) {
                var m = await _purchaseOrderService.GetById(id.Value);
                vm = _mapper.Map<PurchaseOrderViewModel>(m);
                 buyerColorList = (await _buyerColorService.GetAll()).Where(x=>x.BuyerId == m.BuyerColor.BuyerId).ToList().ToSelectList();
                 buyerList =  (await _buyerService.GetAll()).Where(x => x.PartyId == m.BuyerColor.Buyer.PartyId).ToList().ToSelectList();

                if (m != null) {
                    if(filter.IsYarn==true)
                    {
                        yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                        yarnQualityList.Find(x => Convert.ToInt64(x.Value) == vm.YarnQualityId).Selected = true;
                    }
                    else
                    {

                        FabricQualityList.Find(x => Convert.ToInt64(x.Value) == vm.FabricQualityId).Selected = true;
                        
                        FabricTypesList.Find(x => Convert.ToInt64(x.Value) == vm.FabricTypeId).Selected = true;


                    }

                    buyerColorList.Find(x => Convert.ToInt64(x.Value) == vm.BuyerColorId).Selected = true;                 
                    buyerList.Find(x => Convert.ToInt64(x.Value) == m.BuyerColor.BuyerId).Selected = true;
                    partyList.Find(x => Convert.ToInt64(x.Value) == m.BuyerColor.Buyer.PartyId).Selected = true;
                    seasonList.Find(x => Convert.ToInt64(x.Value) == vm.SeasonId).Selected = true;
                }

            }
        

            ViewBag.buyerColorList = buyerColorList;
            ViewBag.buyerList = buyerList;

            ViewBag.yarnTypeList = yarnTypeList;

            ViewBag.yarnQualityList = yarnQualityList;

            ViewBag.partyList = partyList;
            ViewBag.seasonList = seasonList;

            ViewBag.FabricTypesList = FabricTypesList;
            ViewBag.FabricQualityList = FabricQualityList;

            ViewBag.filter = filter;

            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, [FromForm] PurchaseOrderViewModel vm) {
            if (ModelState.IsValid) {
                var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();



                try
                {
                    var m = _mapper.Map<PurchaseOrder>(vm);



                    if (!id.HasValue) {


                        if (filter.IsYarn == true)


                        {
                            
                            vm.FabricQualityId = null;
                            vm.FabricTypeId = null; 

                            await _purchaseOrderService.Create(m);
                        }
                        else
                        {
                            //createfabric
                          
                            vm.YarnQualityId = null;
                            vm.YarnTypeId = null;

                            await _purchaseOrderService.CreateFabric(m);
                        }



                        // create

                        //_tempData.MSG = "Successfully Created";
                    } else {
                        //update
                        await _purchaseOrderService.Update(m);
                        //  _tempData.MSG = "Successfully Updated";
                    }
                } 
                catch (Exception ex) {
                    //  _tempData.Error = ex.Message;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Policy = AccountClaimKeys.PPC_PURCHASE_ORDER_DELETE)]
        public async Task<IActionResult> Delete(long? id, IFormCollection col) {
            try {
                if (id.HasValue) {
                    await _purchaseOrderService.Delete(await _purchaseOrderService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            } catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }

        public async Task<IActionResult> GetBuyersByPartyId(long id) {
            var BuyerSelectList = (await _partyService.GetById(id)).Buyers.ToSelectList();
            return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel {
                Name = "BuyerId",
                PlaceHolder = "Select Buyer",
                UseDefault = true,
                SelectList = BuyerSelectList
            });
        }
        public async Task<IActionResult> GetColorsByBuyerId(long id) {
            var BuyerColorSelectList =  (await _buyerService.GetById(id)).BuyerColors.ToSelectList();
            return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel {
                Name = "BuyerColorId",
                PlaceHolder = "Select Buyer Color",
                UseDefault = true,
                SelectList = BuyerColorSelectList
            });
        }

    }
}