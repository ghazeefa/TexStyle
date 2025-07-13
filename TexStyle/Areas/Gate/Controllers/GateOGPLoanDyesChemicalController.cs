using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Core.Gate;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Gate;

namespace TexStyle.Areas.Gate.Controllers {
    [Area(AreaConstants.GATE.Name)]
    public class GateOGPLoanDyesChemicalController : Controller
    {
        public IUnitOfWork _uow { get; set; }
        public IMapper _map { get; set; }
        public GateOGPLoanDyesChemicalController(IUnitOfWork uow, IMapper map)
        {
            _uow = uow;
            _map = map;
        }
        //public IActionResult Index() {
        //    return View(_uow.GateTrService.GetAllByActivityStatus(false, false, true));
        //}

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.GateTrService.GetBetweenDateRangeByActivityStatus(false, false, true, options.sd.Value, options.ed.Value));
        }



        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            var partylist = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateigptypelist = (await _uow.GateActivityTypeService.GetAllByStatus(false, false, true)).ToSelectList();
            // var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();
            GateTrViewModel vm = new GateTrViewModel();
            if (id != null)
            {
                //edit
                vm = _map.Map<GateTrViewModel>((await _uow.GateTrService.GetById(id.Value)));
                partylist.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                gateigptypelist.Find(x => Convert.ToInt64(x.Value) == vm.GateActivityTypeId).Selected = true;
                //yarntypelist.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
            }
            else
            {
                //add
                //long gateActId = _uow.GateActivityTypeService.GetByName(GateActivityTypes.IGP_Yarn).Id;
                //vm.GateActivityTypeId = gateActId;
                //gateigptypelist.Find(x => Convert.ToInt64(x.Value) == gateActId).Selected = true;

            }



            ViewBag.partylist = partylist;
            ViewBag.gateigptypelist = gateigptypelist;
            // ViewBag.yarntypelist = yarntypelist;

            return PartialView(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var m = await _uow.GateTrService.GetById(id);
            var partyList = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateIGPTypeList = (await _uow.GateActivityTypeService.GetAll()).ToSelectList();
            // var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();

            if (m != null)
            {
                partyList.Find(x => Convert.ToInt64(x.Value) == m.PartyId).Selected = true;
                gateIGPTypeList.Find(x => Convert.ToInt64(x.Value) == m.GateActivityTypeId).Selected = true;
                //yarntypelist.Find(x => Convert.ToInt64(x.Value) == m.YarnTypeId).Selected = true;
            }

            ViewBag.partyList = partyList;
            ViewBag.gateIGPTypeList = gateIGPTypeList;
            // ViewBag.yarntypelist = yarntypelist;
            return View(m);
        }


        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, GateTrViewModel v)
        {
            var m = _map.Map<GateTr>(v);
            dynamic d = 1;
            //getting detail of respective type
            //////if (m.GateActivityTypeId == 8)
            //////{  //d = _uow.LoanPartyGivenOutTrService.GetById(m.Xref).LoanPartyGivenOutTrDetails;
            m.GetDyeChemicalTrId = m.Xref;
            //}

            //else if (m.GateActivityTypeId == 9)
            //{
            //    // d = _uow.LoanTakenReturnOutTrService.GetById(m.Xref).LoanTakenReturnOutTrDetails;
            //    m.GetDyeChemicalTrId = m.Xref;
            //}

            try
            {
                // var m = _map.Map<GateTr>(v);
                var ogp = new GateTr();
                if (id.Value != 0)
                {
                    //update
                    var oldogp = await _uow.GateTrService.GetById(m.Id);
                    ogp = await _uow.GateTrService.Update(m);
                    if (oldogp.GateActivityTypeId != ogp.GateActivityTypeId || oldogp.GetDyeChemicalTrId != ogp.GetDyeChemicalTrId || oldogp.GetDyeChemicalTrId != ogp.GetDyeChemicalTrId)
                    {
                        var o = await _uow.GateTrService.GetById(ogp.Id);

                        await Task.WhenAll(o.GateTrDetails.Select(async de =>
                        {
                            await _uow.GateTrDetailService.Delete(de);
                        }));

                        await CreateDetail(m.Xref, m.GateActivityTypeId.Value, ogp.Id);
                    }

                }
                else
                {
                    //add
                    ogp = await _uow.GateTrService.CreateByActivityType(m, false, false, true);
                    m.Id = ogp.Id;
                    await CreateDetail(m.Xref, m.GateActivityTypeId.Value, ogp.Id);
                }

                var details = new List<GateTrDetail>();

                //foreach (var i in d) {
                //    var newD = new GateTrDetail();
                //    newD = new GateTrDetail {
                //        QtyCr = i.QtyCr,
                //        QtyDr = i.QtyDr,
                //        DyeId = i.DyeId,
                //        ChemicalId = i.ChemicalId,
                //        GateTrId = ogp.Id,
                //       Rate =i.Rate
                //    };
                //    _uow.GateTrDetailService.Create(newD);

                //}
                return RedirectToAction("Details", "GateOGPLoanDyesChemical", new { id = m.Id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction(nameof(Details), new { id = m.Id });

        }


        public async Task CreateDetail(long? Xref, long activityid, long headerid)
        {
            //if (activityid == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_TAKEN_RETURN_OUT).Id) {
            if (Xref != 0)
            {
                var igpDetails = (await _uow.DyeChemicalTrService.GetById(Convert.ToInt64(Xref))).DyeChemicalTrDetails.Where(x => x.IsDeleted == false);
                if (igpDetails != null)
                {

                    await Task.WhenAll(igpDetails.Select(async d =>
                    {
                        await _uow.GateTrDetailService.Create(new GateTrDetail
                        {
                            QtyCr = d.QtyCr,
                            QtyDr = d.QtyDr,
                            ChemicalId = d.ChemicalId,
                            DyeId = d.DyeId,
                            Packet = d.Packet,
                            Rate = d.Rate,
                            // GateXrefDetailId = d.Id,
                            GateTrId = headerid,
                            DyeChemicalTrDetailId = d.Id,
                        });
                    }));

                }
            }

        }
        //} else if (activityid == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id) {
        //    var igpDetails = _uow.DyeChemicalTrService.GetById(Convert.ToInt64(Xref)).Dye.Where(x => x.IsDeleted == false);
        //    if (igpDetails != null) {
        //        foreach (var d in igpDetails) {
        //            _uow.GateTrDetailService.Create(new GateTrDetail {
        //                QtyCr = d.QtyCr,
        //                QtyDr = d.QtyDr,
        //                ChemicalId = d.ChemicalId,
        //                DyeId = d.DyeId,
        //                Packet = d.Packet,
        //                Rate = d.Rate,
        //                GateXrefDetailId = d.Id,
        //                GateTrId = headerid,
        //                DyeChemicalTrDetailId=d.Id
        //            });
        //        }
        //    }

    public async Task<IActionResult> GetOGPByGateActivity(long id) 
        {
            long trtype;
            if (id == (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_TAKEN_RETURN_OUT)).Id)
            {
                trtype = ChemicalTransactions.LoanTakenReturnOut;
            }
            else
            {
                trtype = ChemicalTransactions.LoanPartyGivenOut;
            }

          var OGPList = (await _uow.DyeChemicalTrService.GetAllByTrType(trtype)).ToSelectList(nameof(GateTr.Sno));
            
        // var OGPList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id).ToSelectList(nameof(GateTr.Sno));
        return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel {
            Name = "Xref",
            PlaceHolder = "Select OGP",
            UseDefault = false,
            SelectList = OGPList
        }); 
            //} else if (id == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id) {
            //    var OGPList = _uow.LoanPartyGivenOutTrService.GetAll().ToSelectList();
            //    // var OGPList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id).ToSelectList(nameof(GateTr.Sno));
            //    return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel {
            //        Name = "Xref",
            //        PlaceHolder = "Select OGP",
            //        UseDefault = false,
            //        SelectList = OGPList
            //    });

            //}
            //return PartialView("~/Views/Shared/_SelectList.cshtml");

          }
        [HttpGet]
        public async Task<IActionResult> GateOGPLoanDyesChemicalDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.GateTrService.GetById(id);
                ViewBag.reportTitle = nameof(DyeChemicalTr);
                ViewBag.reportStatus = "OUTWARD GATE PASS";
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
    }
