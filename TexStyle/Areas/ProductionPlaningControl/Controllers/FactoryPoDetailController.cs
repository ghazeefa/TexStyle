using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
using TexStyle.ViewModels.PPC.Forms;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class FactoryPoDetailController : Controller
    {

        private IUnitOfWork _uow;
        private IFactoryPoDetailService _FactoryPoDetailService;

        [ViewData]

        public string AreaName { get; set; }
        private readonly TempDataViewModel _tempData;
        private readonly IReportFilterService _reportFilterService;
        private readonly IMapper _mapper;
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(FactoryPoDetail)}";

        public FactoryPoDetailController(
            IFactoryPoDetailService factoryDatilPoService,
            IUnitOfWork uow, IMapper mapper,
            IReportFilterService ReportFilterService)
        {
            AreaName = "PPC Planing";
            _uow = uow;
            _mapper = mapper;
            _reportFilterService = ReportFilterService;

            _FactoryPoDetailService = factoryDatilPoService;
        }
        [HttpGet]
        [Authorize(Policy = AccountClaimKeys.PPC_IGP_DETAIL_CREATE_AND_UPDATE)]
        public async Task<IActionResult> AddOrUpdate( long? id, long? ParentId)
        {
             var filter =(await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            FactoryPoDetailViewModel vm = new FactoryPoDetailViewModel();
           


            var fabricTypeList =(await _uow.FabricTypesService.GetAll()).ToSelectList();
            var fabricQualityList = (await _uow.FabricQualityService.GetAll()).ToSelectList();
            var buyercolorlist = (await _uow.BuyerColorService.GetAll()).ToSelectList();
            if (id.HasValue)
            {
                var poDetail =await _uow.FactoryPoDetailService.GetById(id.Value);
                // vm = _mapper.Map<FactoryPoDetailViewModel>(poDetail);
                //if (vm != null)
                //{                  
           
                //}
                vm.BuyerColorId = poDetail.BuyerColorId;
                vm.FabricTypesId = poDetail.FabricTypesId;
                vm.FabricQualityId = poDetail.FabricQualityId;
                vm.Dia = poDetail.Dia;
                vm.GSM = poDetail.GSM;
                vm.NetWeightInKg = poDetail.NetWeightInKg;
                vm.TearWeightInKg = poDetail.TearWeightInKg;
                vm.Weight = poDetail.Weight;
                vm.Id = poDetail.Id;
                vm.Description = poDetail.Description;
                if (vm.FabricQualityId != 0) fabricQualityList.Find(x => Convert.ToInt64(x.Value) == vm.FabricQualityId).Selected = true;
                if (vm.FabricTypesId != 0) fabricTypeList.Find(x => Convert.ToInt64(x.Value) == vm.FabricTypesId).Selected = true;
                if (vm.BuyerColorId != 0) buyercolorlist.Find(x => Convert.ToInt64(x.Value) == vm.BuyerColorId).Selected = true;
            }
            else
            {


              //  vm.Id = id.Value;
            }
            vm.FactoryPoId = ParentId;
       
            ViewBag.fabricTypeList = fabricTypeList;
            ViewBag.buyercolorlist = buyercolorlist;
            ViewBag.fabricQualityList = fabricQualityList;
            ViewBag.filter = filter;
            //ViewBag.purchaseOrderList = purchaseOrderList;

            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, [FromForm] FactoryPoDetailViewModel vm)
        {
            var filter = (await _reportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (ModelState.IsValid)
            {
                try
                {
                    FactoryPoDetail m = new FactoryPoDetail();
                    //  m.Id = vm.Id.Value;
                    m.FabricQualityId = vm.FabricQualityId;
                    m.FabricTypesId = vm.FabricTypesId;
                    m.Description = vm.Description;
                    m.Dia = vm.Dia;
                    m.NetWeightInKg = vm.NetWeightInKg;
                    m.TearWeightInKg = vm.TearWeightInKg;
                    m.Weight = vm.Weight;
                    m.FactoryPoId = vm.FactoryPoId;
                    m.GSM = vm.GSM;
                    m.BuyerColorId = vm.BuyerColorId;
                    m.FactoryPoId = vm.FactoryPoId;

       
       
                    if (!id.HasValue)
                    {
                        // create
                        await _uow.FactoryPoDetailService.Create(m);
                        //    _tempData.MSG = "Successfully Created";

                    }
                    else
                    {
                        m.Id = vm.Id.Value;
                        //update
                        await _uow.FactoryPoDetailService.Update(m);
                   //     _tempData.MSG = "Successfully Updated";
                    }

                   
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                }
            }
            return RedirectToAction(nameof(FactoryPoController.Details), "FactoryPo", new { id = vm.FactoryPoId });
        }

        //[HttpPost]
        //[Authorize(Policy = AccountClaimKeys.PPC_IGP_DELETE)]
        //public IActionResult Delete([FromRoute] long? id, [FromQuery] long? sno, [FromQuery] long? igpId)
        //{
        //    try
        //    {
        //        if (id.HasValue && sno.HasValue)
        //        {
        //            _uow.FactoryPoDetailService.Delete(_igpDetailService.GetById(id.Value, sno.Value));
        //            return new StatusCodeResult(200);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return new StatusCodeResult(500);
        //    }
        //    return new StatusCodeResult(400);
        //}


    }
}