using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class ReturnNoteDetailController : Controller
    {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/ReturnNoteDetail";

        private readonly TempDataViewModel _tempData;
        private readonly IPPCPlanningService _pPCPlanningService;
        private readonly IYarnTypeService _yarnTypeService;
        private readonly IYarnQualityService _yarnQualityService;
        private readonly IYarnManufacturerService _yarnManufacturerService;
        private readonly IStoreLocationService _storeLocationService;
        private readonly IIssueNoteService _issueNoteService;
        private readonly IReturnNoteService _returnNoteService;
        private readonly IReturnNoteDetailService _returnNoteDetailService;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ReturnNoteDetailController(
            IMapper mapper,
            IYarnTypeService yarnTypeService,
            IYarnQualityService yarnQualityService,
            IYarnManufacturerService yarnManufacturerService,
            IStoreLocationService storeLocationService,
            IIssueNoteService issueNoteService,
            IReturnNoteService returnToWindingService,
            IReturnNoteDetailService returnToWindingDetailService,
            IPPCPlanningService pPCPlanningService,
            IUnitOfWork uow
            )
        {

            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / Return Note Details";

            _mapper = mapper;
            _yarnTypeService = yarnTypeService;
            _yarnQualityService = yarnQualityService;
            _yarnManufacturerService = yarnManufacturerService;
            _storeLocationService = storeLocationService;
            _issueNoteService = issueNoteService;
            _returnNoteService = returnToWindingService;
            _returnNoteDetailService = returnToWindingDetailService;
            _pPCPlanningService = pPCPlanningService;
            _uow = uow;
            _tempData = new TempDataViewModel();
        }

        [HttpGet]
      //  [Authorize(Policy = AccountClaimKeys.PRODUCTION_RECEIVED_CREATE_AND_UPDATE_DETAIL)]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, [FromQuery] long? rnId)
        {
            var vm = new ReturnNoteDetailViewModel()
            {
                Id = id.HasValue ? id.Value : 0
            };

            //var yarnTypeList = _yarnTypeService.GetAll().ToSelectList();
            //var yarnQualityList = _yarnQualityService.GetAll().ToSelectList();
            var storeLocationList = (await _storeLocationService.GetAll()).ToSelectList();
            var yarnManufacturerList =  (await _yarnManufacturerService.GetAll()).ToSelectList();
            if (rnId != null)
            { 
                var returnNote = await _returnNoteService.GetById(rnId.Value); 
                ViewBag.reprocessed = returnNote.IsReprocessed; 
            }

            if (id.HasValue)
            {
                var rtwDetail = await _returnNoteDetailService.GetById(id.Value);
                vm = _mapper.Map<ReturnNoteDetailViewModel>(rtwDetail);
                vm.AvailableLpsKgs = vm.Kgs;
                if (vm != null)
                {
                    try
                    {

                        //yarnQualityList.Find(x => Convert.ToInt64(x.Value) == vm.YarnQualityId).Selected = true;
                        //yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                        storeLocationList.Find(x => Convert.ToInt64(x.Value) == vm.StoreLocationId).Selected = true;
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            var rn = await _returnNoteService.GetById(rnId.Value);
            vm.ReturnNoteId = rnId;
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            //   vm.ReturnTypeId = rn.ReturnTypeId;
            //vm.Reprocessed = rn.IsReprocessed;
            //ViewBag.yarnTypeList = yarnTypeList;
            //ViewBag.yarnQualityList = yarnQualityList;

            ViewBag.storeLocationList = storeLocationList;
            ViewBag.yarnManufacturerList = yarnManufacturerList;
            ViewBag.filter = filter;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, [FromForm] ReturnNoteDetailViewModel vm)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                if (vm.StoreLocationId == -1)
                {
                    vm.StoreLocationId = null;
                }

                    var m = _mapper.Map<ReturnNoteDetail>(vm);
                m.EcruKgs = vm.EcurKgs;
                    var rn = await _returnNoteService.GetById(vm.ReturnNoteId.Value);
                //m.Sno = rtw.ReturnToWindingDetails.Count() + 1;
                var allppc = _returnNoteDetailService.GetByXreffId(vm.PPCPlanningId.Value);
               

                if (rn.ReturnNoteDetails.Count() > 0)
                    {
                        if (rn.ReturnNoteDetails.Where(x => x.Id == m.Id).Count() == 0)
                        {
                            await _returnNoteDetailService.Create(m);
                        }
                        else
                        {
                            await _returnNoteDetailService.Update(m);
                        }
                    }
                    else
                    {
                        await _returnNoteDetailService.Create(m);
                    }
                }
                catch (Exception ex)
                {
                throw ex;
                    _tempData.Error = ex.Message;
                }
            //}
            return RedirectToAction(nameof(ReturnNoteController.Details), "ReturnNote", new { id = vm.ReturnNoteId });
        }

        [HttpPost]
      //  [Authorize(Policy = AccountClaimKeys.PRODUCTION_RECEIVED_DELETE_DETAIL)]
        public async Task<IActionResult> Delete([FromRoute]long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    await _returnNoteDetailService.Delete(await _returnNoteDetailService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }

            return new StatusCodeResult(400);
        }

        [HttpGet]
        public async Task<IActionResult> CopyIssueData(long? id)
        {
            if (id.HasValue)
            {
                var i = await _issueNoteService.GetById(id.Value);
                return PartialView(i);
            }

            return new StatusCodeResult(404);
        }

        [HttpGet]
        public async Task<IActionResult> CopyReprocessData(long? id)
        {
            if (id.HasValue)
            {
                List<Reprocess> ppcp = (await _uow.PPCPlanningService.GetById(id.Value)).Reprocesses.ToList();

                await Task.WhenAll(ppcp.Select(async d =>
                {
                    d.AvailableLpsKgs = d.Kgs;
                    var reproces = (await _uow.ReturnNoteDetailService.GetAll()).Where(x => x.ReprocessId == d.Id).ToList();

                    reproces.ForEach(e =>
                    {
                        d.AvailableLpsKgs -= e.Kgs;
                    });
                }));

                return PartialView(_ViewPath + "/CopyReprocessData.cshtml", ppcp);
                //return PartialView();
            }

            return new StatusCodeResult(404);
        }

        [HttpGet]
        public async Task<IActionResult> CopyPPCData(long? id)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            if (id.HasValue)
            {
                var allppc = await _returnNoteDetailService.GetByXreffId(id.Value);
               
                    if (allppc.Count > 0)
                    {

                        throw new Exception();
                    }



                

                //var count = allppc.ToString().Count();
                var ppcp = await _pPCPlanningService.GetById(id.Value);

                ppcp.AvailableLpsKgs = ppcp.Kgs;

                var issues = (await _uow.ReturnNoteDetailService.GetAll()).Where(x => x.PPCPlanningId == ppcp.Id).ToList();

                issues.ForEach(d =>
                {
                    ppcp.AvailableLpsKgs -= d.Kgs;
                });
                ViewBag.filter = filter;
                //if (count ==0)
                //{
              
                    return PartialView(ppcp);
                
                
            }
            
            return new StatusCodeResult(404);
        }
    }
}