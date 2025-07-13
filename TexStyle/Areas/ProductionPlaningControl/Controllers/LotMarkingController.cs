using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC.Forms;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class LotMarkingController : Controller
    {


        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(knittingParty)}";

        private readonly TempDataViewModel _tempData;
        public IUnitOfWork _uow { get; set; }
        public IMapper _map { get; set; }
        public LotMarkingController(IUnitOfWork uow, IMapper map)
        {
            _uow = uow;
            _map = map;
        }



        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View((await _uow.LotMarkingService.GetAll()).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            LotMarkingViewModel vm = null;
            if (id.HasValue)
            {
                vm = _map.Map<LotMarkingViewModel>(await _uow.LotMarkingService.GetById(id.Value));
            }
            var ppc= (await _uow.PPCPlanningService.GetAllFabric()).ToSelectList();
            ViewBag.LPS = ppc;
            return PartialView("AddOrUpdate", vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetIssunaceDocument()
        {
            LotNumber vm = null;
           
            return PartialView("GetIssunaceDocument", vm);
        }

        [HttpGet]
       
            public async Task<IActionResult> IssunaceDocument(LotNumber vm)
            {
                var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
                if (filter == null) return NotFound("Report filter doesn't have any record.");

                var rec = await _uow.PPCPlanningService.IssuanceRecord1Repository_P9ViewModel(vm.LotNo);

                var res = new List<P9IssunaceRecordReportlViewModel>();
                var title = "Ecru Yarn Stock Report";


                var gb = rec.GroupBy(x => x.LotNo).ToList();
                gb.ToList().ForEach(x =>
                {

                    var r = new P9IssunaceRecordReportlViewModel();

                    //r.Date = x.Key.ToString("MM-dd-yyyy");
                    r.DateFrom = DateTime.Now.ToShortDateString();
                    r.DateTo = Convert.ToDateTime(filter.DateTo.Value).ToShortDateString();
                    r.LotNo = x.FirstOrDefault().LotNo;



                    x.ToList().ForEach(b =>
                    {
                        var detailLPSItem = new P9IssunaceRecordReportDetailViewModel();



                        detailLPSItem.LPSNo = b.LPSNo;
                        detailLPSItem.Kgs = b.Kgs;
                        detailLPSItem.NoOfRolls = b.NoOfRolls;
                        detailLPSItem.Status = b.Status;


                        r.P9IssunaceRecordReportDetailViewModel.Add(detailLPSItem);

                    }

                    );
                    res.Add(r);
                });

                //res = res.OrderBy(x => x.Date).ToList();



                ViewBag.ReportDurationTitle = $"For The Period ({(filter.DateFrom.HasValue ? filter.DateFrom.Value.ToString("dd-MMM-yyyy") : "Not Set")}) To ({(filter.DateTo.HasValue ? filter.DateTo.Value.ToString("dd-MMM-yyyy") : "Not Set")})";
                //    return View(res);
                ViewData["Message"] = "Your contact page.";
                string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
                ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
                return new ViewAsPdf(res)
                {
                    PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    PageMargins = { Left = 4 },
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    CustomSwitches = footer
                };

                // return View(res);
            
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, LotMarkingViewModel vm)
        {
            //if (ModelState.IsValid)
            //{
                try
            {
                    LotMarking m = new LotMarking();
                m.PPCPlanningId = vm.PPCPlanningId;
                m.Kgs = vm.Kgs;
                m.NoOfRolls = 0;
                m.RollNo = 0;
                m.IsIssued = vm.IsIssued;
                m.IssuanceDate = vm.IssuanceDate;
                //var m = _map.Map<LotMarking>(vm);
                if (!id.HasValue) {
                    //var max = _uow.LotMarkingService.GetAll().Max(x => x.RollNo);

                    //m.RollNo = max;
                    //for (var i = 1; i <= vm.NoOfRolls; i++)
                    //    {
                    //        m.IsIssued = true;
                    //        m.Kgs = vm.Kgs;
                    //        m.PPCPlanningId = vm.PPCPlanningId;
                    //        m.NoOfRolls = vm.NoOfRolls;
                    //        m.RollNo += 1;
                    //    if (m.Id != 0)
                    //    {
                    //        m.Id = 0;
                   

                        await _uow.LotMarkingService.Create(m);
                    }
                        

                        //_tempData.MSG = "Successfully Created";
                    
                    else
                    {
                        
                       
                        //update
                        await _uow.LotMarkingService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                }
            //}

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col)
        {
            try
            {
                if (id.HasValue)
                {
                    await _uow.LotMarkingService.Delete(await _uow.LotMarkingService.GetById(id.Value));
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