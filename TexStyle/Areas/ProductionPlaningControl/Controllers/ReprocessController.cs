using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class ReprocessController : Controller
    {

        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(Reprocess)}";

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;

        public ReprocessController(IUnitOfWork uow, IMapper mapper)
        {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(Reprocess)}";
            _tempData = new TempDataViewModel();
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(long? id)
        {
            try
            {
                var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

                ViewBag.pPCPlanningId = id.Value;
                var list = await _uow.ReprocessService.GetReprocessesByLpsId(id.Value);

                ViewBag.filter = filter;
                return View(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpGet]
        //public IActionResult Index([FromQuery] FilterOptions options) {
        //    var today = DateTime.Now;
        //    var startDate = new DateTime(today.Year, today.Month, 1);
        //    var endDate = startDate.AddMonths(1).AddDays(-1);
        //    if (!options.sd.HasValue || !options.ed.HasValue) {
        //        options.sd = startDate;
        //        options.ed = endDate;
        //    }
        //    ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
        //    return View(_uow.ReprocessService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}

        public async Task<IActionResult> AddOrUpdate(long? id, long? ppcplanningid)
        {
            try
            {

                var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

                //  var ReworkActivityList = ReworkActivities.GetAll.ToSelectList();

                ReprocessViewModel vm = new ReprocessViewModel();
                if (ppcplanningid.HasValue)
                {
                    vm.PPCPlanningId = ppcplanningid.Value;

                }
                else
                {
                    vm = _mapper.Map<ReprocessViewModel>(await _uow.ReprocessService.GetById(id.Value));
                    vm.AvailableLpsKgs = vm.Kgs;

                   // ReworkActivityList.Find(x => Convert.ToInt64(x.Value) == vm.ReworkActivityId).Selected = true;
                }

                //  ViewBag.ReworkActivityList = ReworkActivityList;
                ViewBag.filter = filter;

                return View($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, ReprocessViewModel model)
        {
            try
            {
                var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

                var m = _mapper.Map<Reprocess>(model);

                if (!id.HasValue)
                {
                   // var filter = _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId())).FirstOrDefault();
                    if (filter.IsYarn == true)
                    {
                        await _uow.ReprocessService.Create(m);
                    }
                    if (filter.IsYarn == false)
                    {

                        await _uow.ReprocessService.CreateFabric(m);
                    }
                    // m.Count = _uow.ReprocessService.GetReprocessesByLpsId(m.PPCPlanningId.Value, getAll:true).Count + 1;
           

                }
                else
                {
                    if (filter.IsYarn == true)
                    {
                        await _uow.ReprocessService.Update(m);
                    }
                    if (filter.IsYarn == false)
                    {

                        await _uow.ReprocessService.UpdateFabric(m);
                    }

                    
                }

                return RedirectToAction(nameof(Index), new { id = m.PPCPlanningId });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> CopyIgpData(long? id)
        {
            var igpDetailList = new List<InwardGatePassDetail>();
            if (id.HasValue)
            {
                var igp = await _uow.IGPService.GetById(id.Value);
                if (igp.IsReprocessed == true)
                {

                    igpDetailList = igp?.InwardGatePassDetails.ToList();

                    var ppc = await _uow.ReprocessService.GetAll();

                    igpDetailList.ForEach(d => {
                        d.AvailableLpsKgs = d.NetWeightInKg;

                        ppc.Where(p => p.InwardGatePassDetailId == d.Id).ToList().ForEach(p => {
                            d.AvailableLpsKgs -= p.Kgs;
                        });

                    });
                }


            }
            return PartialView(igpDetailList);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col)
        {
            try
            {
                if (id.HasValue)
                {
                    var res = await _uow.ReprocessService.GetById(id.Value);
                    await _uow.ReprocessService.Delete(res/*new Reprocess { Id = id.Value }*/);
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception ez)
            {
                throw ez;
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
    }
}