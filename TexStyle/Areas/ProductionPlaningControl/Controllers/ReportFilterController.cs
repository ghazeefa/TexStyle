using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers {
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class ReportFilterController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(ReportFilter)}";

        private readonly TempDataViewModel _tempData;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ReportFilterController(IUnitOfWork uow, IMapper mapper) {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(ReportFilter)}";
            _tempData = new TempDataViewModel();
            _uow = uow;
            _mapper = mapper;
        }

        //[HttpGet]
        //public IActionResult Index() {
        //    return View(_uow.ReportFilterService.GetAll().ToList());
        //}

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {
            //var today = DateTime.Now;
            //var startDate = new DateTime(today.Year, today.Month, 1);
            //var endDate = startDate.AddMonths(1).AddDays(-1);
            //var res = new List<ReportFilter>();
            //if (!options.sd.HasValue || !options.ed.HasValue) {
            //    options.sd = startDate;
            //    options.ed = endDate;
            //    res = _uow.ReportFilterService.GetBetweenDateRange(options.sd.Value, options.ed.Value);
            //} else {
            //    res = _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).ToList();
            //}
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            ViewBag.filter = filter;
            //ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View((await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            ReportFilterViewModel vm = new ReportFilterViewModel() { };
            if (User.Identity.IsAuthenticated) {
                vm.UserId = Convert.ToInt64(User.Identity.GetUserId());
            }

            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            ViewBag.filter = filter;
            List<SelectListItem> yarnTypeList = (await _uow.YarnTypeService.GetAll()).ToSelectList();
            List<SelectListItem> yarnQualityList =  (await _uow.YarnQualityService.GetAll()).ToSelectList();
            List<SelectListItem> yarnPartyList =  (await _uow.PartyService.GetAll()).ToSelectList();

            List<SelectListItem> FabricTypeList =  (await _uow.FabricTypesService.GetAll()).ToSelectList();
            List<SelectListItem> FabricQualityList = (await _uow.FabricQualityService.GetAll()).ToSelectList();
            List<SelectListItem> BuyerList =  (await _uow.BuyerService.GetAll()).ToSelectList();

            List<SelectListItem> analysistypeList =  (await _uow.AnalysisTypeService.GetAll()).ToSelectList();
            List<SelectListItem> buyerColorList = (await _uow.BuyerColorService.GetAll()).ToSelectList();

            List<SelectListItem> factoryPOList = (await _uow.FactoryPoService.GetFactoryPoList()).ToSelectList(nameof(FactoryPo.Po));
            //var purchaseOrderList = (await _uow.PurchaseOrderService.GetCommercialPo()).ToSelectList(nameof(PurchaseOrder.Po));

            if (id.HasValue) {
                vm = _mapper.Map<ReportFilterViewModel>(await _uow.ReportFilterService.GetById(id.Value));

                if (vm.YarnTypeId.HasValue) yarnTypeList.Find(x => Convert.ToInt32(x.Value) == vm.YarnTypeId).Selected = true;
                if (vm.YarnQualityId.HasValue) yarnQualityList.Find(x => Convert.ToInt32(x.Value) == vm.YarnQualityId).Selected = true;
                if (vm.YarnPartyId.HasValue) yarnPartyList.Find(x => Convert.ToInt32(x.Value) == vm.YarnPartyId).Selected = true;
                
                if (vm.FabricTypeId.HasValue) FabricTypeList.Find(x => Convert.ToInt32(x.Value) == vm.FabricTypeId).Selected = true;
                if (vm.FabricQualityId.HasValue) FabricQualityList.Find(x => Convert.ToInt32(x.Value) == vm.FabricQualityId).Selected = true;
                if (vm.BuyerId.HasValue) BuyerList.Find(x => Convert.ToInt32(x.Value) == vm.BuyerId).Selected = true;


                if (vm.AnalysisTypeId.HasValue) analysistypeList.Find(x => Convert.ToInt32(x.Value) == vm.AnalysisTypeId).Selected = true;
                if (vm.BuyerColorId.HasValue) buyerColorList.Find(x => Convert.ToInt32(x.Value) == vm.BuyerColorId).Selected = true;
                if (vm.FactoryPO.HasValue) factoryPOList.Find(x => Convert.ToInt32(x.Text) == vm.FactoryPO).Selected = true;

            }


            ViewBag.FactoryPOList = factoryPOList;
            ViewBag.BuyerList = BuyerList;
            ViewBag.FabricQualityList = FabricQualityList;
            ViewBag.FabricTypeList = FabricTypeList;

            ViewBag.YarnTypes = yarnTypeList;
            ViewBag.YarnQualities = yarnQualityList;
            ViewBag.YarnParties = yarnPartyList;
            ViewBag.analysistypeList = analysistypeList;
            ViewBag.BuyerColorList = buyerColorList;
            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, ReportFilterViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<ReportFilter>(vm);
                    if (!id.HasValue) {
                        // create
                        await _uow.ReportFilterService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update

                      
                        await _uow.ReportFilterService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex) {
                    _tempData.Error = ex.Message;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col) {
            try {
                if (id.HasValue) {
                    await _uow.ReportFilterService.Delete(await _uow.ReportFilterService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
    }
}