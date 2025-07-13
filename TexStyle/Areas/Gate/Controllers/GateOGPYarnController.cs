using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Gate;

namespace TexStyle.Areas.Gate.Controllers {
    [Area(AreaConstants.GATE.Name)]
    public class GateOGPYarnController : Controller {
        public IUnitOfWork _uow { get; set; }
        public IMapper _map { get; set; }
        public GateOGPYarnController(IUnitOfWork uow, IMapper map) {
            _uow = uow;
            _map = map;
        }
        //public IActionResult Index() {
        //    return View(_uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Yarn).Id));
        //}

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();


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
                return View(await _uow.GateTrService.GetBetweenDateRangeByActivityTypeId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Yarn).Id, options.sd.Value, options.ed.Value));

            }

            else
            {
                return View(await _uow.GateTrService.GetBetweenDateRangeByActivityTypeId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Fabric).Id, options.sd.Value, options.ed.Value));
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            var partylist = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateigptypelist = (await _uow.GateActivityTypeService.GetAll()).ToSelectList();
            var ogplist = (await _uow.OGPService.GetAll()).ToSelectList(nameof(OutwardGatePass.Id));
            gateigptypelist.Find(x => Convert.ToInt64(x.Value) == _uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Yarn).Id).Selected = true;
            // var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();
            GateTrViewModel vm = new GateTrViewModel();
            if (id != null) {
                //edit
                vm = _map.Map<GateTrViewModel>(await _uow.GateTrService.GetById(id.Value));
                partylist.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                //  gateigptypelist.Find(x => Convert.ToInt64(x.Value) == vm.GateActivityTypeId).Selected = true;
                ogplist.Find(x => Convert.ToInt64(x.Value) == vm.Xref).Selected = true;
                //yarntypelist.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
            } else {
                //add
                long gateActId = (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.IGP_Yarn)).Id;
                vm.GateActivityTypeId = gateActId;
                gateigptypelist.Find(x => Convert.ToInt64(x.Value) == gateActId).Selected = true;

            }


            
            ViewBag.partylist = partylist;
            ViewBag.gateigptypelist = gateigptypelist;
            vm.GateActivityTypeId = (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Yarn)).Id;
            ViewBag.ogplist = ogplist;

            // ViewBag.yarntypelist = yarntypelist;

            return PartialView(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id) {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            var m = await _uow.GateTrService.GetById(id);
            var partyList = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateIGPTypeList =  (await _uow.GateActivityTypeService.GetAll()).ToSelectList();
            var ogplist =  (await _uow.OGPService.GetAll()).ToSelectList(nameof(OutwardGatePass.Id));
            // var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();

            if (m != null) {
                partyList.Find(x => Convert.ToInt64(x.Value) == m.PartyId).Selected = true;
                gateIGPTypeList.Find(x => Convert.ToInt64(x.Value) == m.GateActivityTypeId).Selected = true;
                ogplist.Find(x => Convert.ToInt64(x.Value) == m.Xref).Selected = true;
                //yarntypelist.Find(x => Convert.ToInt64(x.Value) == m.YarnTypeId).Selected = true;
            }
            ViewBag.filter = filter;
            ViewBag.partyList = partyList;
            ViewBag.gateIGPTypeList = gateIGPTypeList;

            ViewBag.ogplist = ogplist;
            // ViewBag.yarntypelist = yarntypelist;
            return View(m);
        }

        public async Task<IActionResult> GetPartyfromPPCOgp(long id) {
            var p = (await _uow.OGPService.GetById(id)).Party;

            return Json(p);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, GateTrViewModel v) {
            var m = _map.Map<GateTr>(v);
            try {
                // var m = _map.Map<GateTr>(v);
                var ogp = new GateTr();
                if (id.Value != 0) {
                    ogp = await _uow.GateTrService.Update(m);

                    var o = await _uow.GateTrService.GetById(ogp.Id);

                    await Task.WhenAll(o.GateTrDetails.Select(async de =>
                    {
                        await _uow.GateTrDetailService.Delete(de);
                    }));

                } else {
                    ogp = await _uow.GateTrService.CreateBySno(m);
                    m.Id = ogp.Id;
                }



                // if (m.GateActivityTypeId == 8)
                var d = (await _uow.OGPService.GetById(m.Xref)).OutwardGatePassDetails;
                //else if (m.GateActivityTypeId == 9)
                //  d = _uow.OGPService.GetById(m.Xref).OutwardGatePassDetails;

                var details = new List<GateTrDetail>();

                await Task.WhenAll(d.Select(async i =>
                {
                    var newD = new GateTrDetail();

                    //if (m.GateActivityTypeId == 2)
                    //{
                    //    newD = new GateOgpDetail
                    //    {
                    //        QtyCr = i.Kgs,
                    //        YarnTypeId = i.YarnTypeId,
                    //        GateOgpId = ogp.Id
                    //    };
                    //}
                    //else
                    //{
                    newD = new GateTrDetail
                    {
                        QtyCr = i.Kgs,
                        QtyDr = 0,
                        YarnTypeId = i.YarnTypeId,
                        GateTrId = ogp.Id
                    };
                    //}

                    await _uow.GateTrDetailService.Create(newD);
                }));

                //return RedirectToAction("Details",@"~\Areas\ProductionPlaningControl\Controller\OGP", new { id = m.Id });
                return RedirectToAction("Details", "GateOGPLoanDyesChemical", new { id = m.Id });
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        [HttpGet]
        public async Task<IActionResult> GateOGPYarnDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.GateTrService.GetById(id);
                ViewBag.reportTitle = nameof(GateTr);
                ViewBag.reportStatus = "OUTWARD GATE PASS";
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        //public void CreateDetail(long? Xref, long activityid, long headerid)
        //{
        //    if (activityid == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_TAKEN_RETURN_OUT).Id)
        //    {
        //        if (Xref != 0)
        //        {
        //            var igpDetails = _uow.LoanTakenReturnOutTrService.GetById(Convert.ToInt64(Xref)).LoanTakenReturnOutTrDetails.Where(x => x.IsDeleted == false);
        //            if (igpDetails != null)
        //            {
        //                foreach (var d in igpDetails)
        //                {
        //                    _uow.GateTrDetailService.Create(new GateTrDetail
        //                    {
        //                        QtyCr = d.QtyCr,
        //                        QtyDr = d.QtyDr,
        //                        ChemicalId = d.ChemicalId,
        //                        DyeId = d.DyeId,
        //                        // Description = d.Description,
        //                        GateXrefDetailId = d.Id,
        //                        GateTrId = headerid
        //                    });
        //                }
        //            }

        //        }
        //    }
        //    else if (activityid == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id)
        //    {
        //        var igpDetails = _uow.LoanPartyGivenOutTrService.GetById(Convert.ToInt64(Xref)).LoanPartyGivenOutTrDetails.Where(x => x.IsDeleted == false);
        //        if (igpDetails != null)
        //        {
        //            foreach (var d in igpDetails)
        //            {
        //                _uow.GateTrDetailService.Create(new GateTrDetail
        //                {
        //                    QtyCr = d.QtyCr,
        //                    QtyDr = d.QtyDr,
        //                    ChemicalId = d.ChemicalId,
        //                    DyeId = d.DyeId,
        //                    // Description = d.Description,
        //                    GateXrefDetailId = d.Id,
        //                    GateTrId = headerid
        //                });
        //            }
        //        }

        //    }


        //}

        //public IActionResult GetOGPByGateActivity(long id)
        //{
        //    if (id == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_TAKEN_RETURN_OUT).Id)
        //    {
        //        var OGPList = _uow.LoanTakenReturnOutTrService.GetAll().ToSelectList();
        //        // var OGPList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id).ToSelectList(nameof(GateTr.Sno));
        //        return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel
        //        {
        //            Name = "Xref",
        //            PlaceHolder = "Select OGP",
        //            UseDefault = false,
        //            SelectList = OGPList
        //        });
        //    }
        //    else if (id == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id)
        //    {
        //        var OGPList = _uow.LoanPartyGivenOutTrService.GetAll().ToSelectList();
        //        // var OGPList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id).ToSelectList(nameof(GateTr.Sno));
        //        return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel
        //        {
        //            Name = "Xref",
        //            PlaceHolder = "Select OGP",
        //            UseDefault = false,
        //            SelectList = OGPList
        //        });

        //    }
        //    return PartialView("~/Views/Shared/_SelectList.cshtml");



        //}
    }
}