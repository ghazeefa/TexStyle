using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class IssueNoteController : Controller
    {

        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/IssueNote";

        private readonly TempDataViewModel _tempData;
        private readonly IPPCPlanningService _pPCPlanningService;
        private readonly IIssueNoteService _issueNoteService;
        private readonly IIssueNoteDetailService _issueNoteDetailService;
        private readonly IYarnTypeService _yarnTypeService;
        private readonly IYarnQualityService _yarnQualityService;
        private readonly IYarnManufacturerService _yarnManufacturerService;
        private readonly IStoreLocationService _storeLocationService;
        private readonly IReprocessService _reProcessService;
        private readonly IMapper _mapper;

        public IssueNoteController(IMapper mapper,
            IIssueNoteService issueNoteService,
            IIssueNoteDetailService issueNoteDetailService,
            IYarnTypeService yarnTypeService,
            IYarnQualityService yarnQualityService,
            IYarnManufacturerService yarnManufacturerService,
            IStoreLocationService storeLocationService,
            IPPCPlanningService pPCPlanningService, IReprocessService reprocessService)
        {

            _mapper = mapper;
            _tempData = new TempDataViewModel();


            _yarnTypeService = yarnTypeService;
            _yarnQualityService = yarnQualityService;
            _yarnManufacturerService = yarnManufacturerService;
            _storeLocationService = storeLocationService;
            _reProcessService = reprocessService;
            _issueNoteService = issueNoteService;
            _issueNoteDetailService = issueNoteDetailService;
            _pPCPlanningService = pPCPlanningService;

        }
    //    [Authorize(Policy = AccountClaimKeys.ISSUE_WINDING_VIEW)]
        public async Task<IActionResult> Index(int id)
        {
            //if (id == 0)
            //    id = IssueTypes.IssueToWinding.Id;

            return View(await _issueNoteService.GetAll());
        }

        [HttpGet]
    //    [Authorize(Policy = AccountClaimKeys.ISSUE_WINDING_VIEW)]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue) {
                options.sd = startDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _issueNoteService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        }

        [HttpGet]
      //  [Authorize(Policy = AccountClaimKeys.ISSUE_WINDING_CREATE_AND_UPDATE)]
        public async Task<IActionResult> AddOrUpdate(long? id, int issueTypeId)
        {
            var vm = new IssueNoteViewModel();

            if (id.HasValue)
            {
                vm = _mapper.Map<IssueNoteViewModel>(await _issueNoteService.GetById(id.Value));
            }
            else
            {
                var item =(await _issueNoteService.GetAll()).Count();
                vm.Id = item == 0 ? 1 : ((await _issueNoteService.GetAll()).LastOrDefault().Id + 1);
                vm.IssueTypeId = issueTypeId;
            }

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute]long? id, IssueNoteViewModel vm)
        {
           // var m = null;
            //if (ModelState.IsValid)
            //{
            try
            {
                // TODO: Tell me what the hell are you doing here ? 
                //  why are you setting vm.id to null? what if it already has some id.??
                // BRUHHH!!!!!!!!!!!!!!!
                vm.Id = null;
                var m = _mapper.Map<IssueNote>(vm);
                if (!id.HasValue)
                {
                    //m.IGPDetailId = null;
                    // create
                    await _issueNoteService.Create(m);
                    _tempData.MSG = "Successfully Created";
                }
                else
                {
                    //update
                    m.Id = id.Value;
                    await _issueNoteService.Update(m);
                    _tempData.MSG = "Successfully Updated";
                } 
                return RedirectToAction(nameof(IssueNoteController.Details),  new { id = m.Id });

            }
            catch (Exception ex)
            {
                _tempData.Error = ex.Message;
                throw ex;
            }
            //}
           
           return RedirectToAction(nameof(Index), new { Id = vm.IssueTypeId });
        }
      //  [Authorize(Policy = AccountClaimKeys.ISSUE_WINDING_VIEW_DETAIL)]
        public async Task<IActionResult> Details(long id)
        {
            return View(await _issueNoteService.GetById(id));
        }


        [HttpGet]
      // [Authorize(Policy = AccountClaimKeys.ISSUE_WINDING_CREATE_AND_UPDATE_DETAIL)]
        public async Task<IActionResult> AddOrUpdateDetail([FromRoute]long? id, [FromQuery] long inId, bool IsReprocess)
        {

            var vm = new IssueNoteDetailViewModel()
            {
                Id = id.HasValue ? id.Value : 0
            };

            //var yarnTypeList = _yarnTypeService.GetAll().ToSelectList();
            //var yarnQualityList = _yarnQualityService.GetAll().ToSelectList();
            //var yarnManufacturerList = _yarnManufacturerService.GetAll().ToSelectList();
            var storeLocationList =(await _storeLocationService.GetAll()).ToSelectList();

            if (id.HasValue)
            {
                var issueDetail =await _issueNoteDetailService.GetById(id.Value);
                vm = _mapper.Map<IssueNoteDetailViewModel>(issueDetail);

                if (vm != null)
                {
                    //yarnManufacturerList.Find(x => Convert.ToInt64(x.Value) == vm.YarnManufacturerId).Selected = true;
                    //yarnQualityList.Find(x => Convert.ToInt64(x.Value) == vm.YarnQualityId).Selected = true;
                    //yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                    storeLocationList.Find(x => Convert.ToInt64(x.Value) == vm.StoreLocationId).Selected = true;
                    //vm.AvailableLpsKgs = vm.Kgs;
                }
            }
            else { vm.IssueNoteId = inId; }


            //ViewBag.yarnTypeList = yarnTypeList;
            //ViewBag.yarnQualityList = yarnQualityList;
            //ViewBag.yarnManufacturerList = yarnManufacturerList;
            ViewBag.storeLocationList = storeLocationList;
            ViewBag.isreprocessed = IsReprocess;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDetail(long? id, [FromForm] IssueNoteDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var m = _mapper.Map<IssueNoteDetail>(vm);
                    var issueNote =await _issueNoteService.GetById(vm.IssueNoteId);
                    //m.Sno = rtw.ReturnToWindingDetails.Count() + 1;

                    if (issueNote.IssueNoteDetail.Count() > 0)
                    {
                        if (issueNote.IssueNoteDetail.Where(x => x.Id == m.Id).Count() == 0)
                        {
                            await _issueNoteDetailService.Create(m);
                        }
                        else
                        {
                            await _issueNoteDetailService.Update(m);
                       
                        }
                    }
                    else
                    {

                        //m.ReprocessId = null;
                        await _issueNoteDetailService.Create(m);
                    }
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                }
            }
            return RedirectToAction(nameof(IssueNoteController.Details), "IssueNote", new { id = vm.IssueNoteId });
        }

        [HttpGet]
        public async Task<IActionResult> CopyPPCData([FromQuery] bool? isreprocess, long? id)
        {
            if (id.HasValue)
            {

                if (isreprocess == true)
                {
                    List<Reprocess> ppcp = await _reProcessService.GetReprocessesByLpsId(id.Value);

                    await Task.WhenAll(ppcp.Select(async d =>
                    {
                        d.AvailableLpsKgs = d.Kgs;
                        var issues = (await _issueNoteDetailService.GetAll()).Where(x => x.ReprocessId == d.Id).ToList();


                        issues.ForEach(e =>
                        {
                            d.AvailableLpsKgs -= e.Kgs;
                        });
                    }));
                    return PartialView(_ViewPath + "/CopyReprocessData.cshtml", ppcp);
                }
                else
                {
                    var ppcp = await _pPCPlanningService.GetById(id.Value);

                    ppcp.AvailableLpsKgs = ppcp.Kgs;

                    var issues = (await _issueNoteDetailService.GetAll()).Where(x => x.PPCPlanningId == ppcp.Id ).ToList();

                    issues.ForEach(d =>
                    {
                        ppcp.AvailableLpsKgs -= d.Kgs;
                    });

                    return PartialView(ppcp);

                }

            }

            return new StatusCodeResult(404);
        }


        [HttpPost]
      //  [Authorize(Policy = AccountClaimKeys.ISSUE_WINDING_DELETE)]
        public async Task<IActionResult> Delete([FromRoute]long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    await _issueNoteService.Delete(await _issueNoteService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }

            return new StatusCodeResult(400);
        }

        [HttpPost]
      //  [Authorize(Policy = AccountClaimKeys.ISSUE_WINDING_DELETE_DETAIL)]
        public async Task<IActionResult> DeleteDetail([FromRoute]long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    await _issueNoteDetailService.Delete(await _issueNoteDetailService.GetById(id.Value));
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