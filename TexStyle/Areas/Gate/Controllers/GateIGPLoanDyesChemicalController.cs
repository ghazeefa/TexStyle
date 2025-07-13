using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.Gate;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Gate;

namespace TexStyle.Areas.Gate.Controllers
{
    [Area(AreaConstants.GATE.Name)]
    public class GateIGPLoanDyesChemicalController : Controller
    {
        public IUnitOfWork _uow { get; set; }
        public IMapper _map { get; set; }
        public GateIGPLoanDyesChemicalController(IUnitOfWork uow, IMapper map)
        {
            _uow = uow;
            _map = map;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.GateTrService.GetAllByActivityStatus(false, true, false));
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue) {
                options.sd = startDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.GateTrService.GetBetweenDateRangeByActivityStatus(false,true,false,options.sd.Value, options.ed.Value));
        }


        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            var partylist = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateigptypelist = (await _uow.GateActivityTypeService.GetAllByStatus(false, true, false)).ToSelectList();
            // var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();
            GateTrViewModel vm = new GateTrViewModel();
            if (id != null)
            {
                //edit
                vm = _map.Map<GateTrViewModel>(await _uow.GateTrService.GetById(id.Value));
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
            var gateIGPTypeList =  (await _uow.GateActivityTypeService.GetAll()).ToSelectList();
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
            ModelState.Remove(nameof(v.Id));
            if (ModelState.IsValid)
            {

                try
                {
                    var vm = _map.Map<GateTr>(v);
                    if (id.Value != 0)
                    {
                        //edit
                        var igp = await _uow.GateTrService.GetById(Convert.ToInt64(vm.Id));
                        await _uow.GateTrService.Update(vm);
                        long gateid= Convert.ToInt64(igp.GateTrId);
                        if (igp.GateActivityTypeId != vm.GateActivityTypeId || gateid != vm.Xref)
                        {
                                var o = await _uow.GateTrService.GetById(Convert.ToInt64(id));
                                if (o != null)
                                {
                                await Task.WhenAll(o.GateTrDetails.Select(async de =>
                                {
                                    await _uow.GateTrDetailService.Delete(de);
                                }));

                                if (vm.Xref == (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_RETURN_IN)).Id)
                                { 
                                    await  AddDetail(vm.Xref, vm.Id); }
                                }
                                //if (vm.Xref != 0)
                                //{
                                //    var igpDetails = _uow.GateTrService.GetById(vm.Xref).GateTrDetails.Where(x => x.IsDeleted == false);
                                //    if (igpDetails != null)
                                //    {
                                //        foreach (var d in igpDetails)
                                //        {
                                //            _uow.GateTrDetailService.Create(new GateTrDetail
                                //            {
                                //                QtyCr = d.QtyCr,
                                //                QtyDr = d.QtyDr,
                                //                ChemicalId = d.ChemicalId,
                                //                DyeId = d.DyeId,
                                //                Description = d.Description,
                                //                GateXrefDetailId = d.Id,
                                //                GateTrId = vm.Id,
                                //                Rate = d.Rate
                                //            });
                                //        }
                                //    }
                                //}
                            }
                        }

                    //else
                    //{
                    //    vm = _map.Map<GateTr>(v);
                    //    var o = _uow.GateTrService.GetById(Convert.ToInt64(id));
                    //if (o != null)
                    //{
                    //    o.GateTrDetails.ToList().ForEach(de =>
                    //    {
                    //        _uow.GateTrDetailService.Delete(de);
                    //    });
                    //}
                    //if(vm.Xref!=0)
                    //    {  var igpDetails = _uow.GateTrService.GetById(vm.Xref).GateTrDetails.Where(x => x.IsDeleted == false);
                    //    if (igpDetails != null)
                    //    {
                    //        foreach (var d in igpDetails)
                    //        {
                    //            _uow.GateTrDetailService.Create(new GateTrDetail
                    //            {

                    //                QtyCr = d.GateTr.GateActivityTypeId== _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_RETURN_IN).Id ? d.QtyDr:d.QtyCr,
                    //                QtyDr = d.GateTr.GateActivityTypeId == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_RETURN_IN).Id ? d.QtyCr : d.QtyDr,
                    //                ChemicalId = d.ChemicalId,
                    //                DyeId = d.DyeId,
                    //                Description = d.Description,
                    //                GateXrefDetailId = d.Id,
                    //                GateTrId = vm.Id,
                    //                Rate=d.Rate
                    //            });
                    //        }
                    //    } }

                    // }
                    // }


                    // _tempData.MSG = "Successfully Updated";


                    else
                    {
                        //add
                        vm = _map.Map<GateTr>(v);
                        //if (vm.GateActivityTypeId == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_TAKEN_IN).Id)
                        //{
                        if (vm.GateActivityTypeId == (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_RETURN_IN)).Id)
                        {
                   
                            //vm.GateTrId = _uow.GateTrService.GetAllByDyeChemicalTrId(a);
                            vm.GateTrId = vm.Xref;
                        }
                            await _uow.GateTrService.CreateByActivityType(vm, false, true, false);
                        //}
                        //else
                       if (vm.GateActivityTypeId == (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_RETURN_IN)).Id)
                        {
                           await AddDetail(vm.Xref, vm.Id);
                           // _uow.GateTrService.CreateByActivityType(vm, false, true, false);

                        }


                        //   _tempData.MSG = "Successfully Created";
                    }

                    return RedirectToAction("Details", "GateIGPLoanDyesChemical", new { id = vm.Id });
                }
                                     catch (Exception ex)
                {

                    throw ex;
                }
                //return RedirectToAction("Details", "GateIGPLoanDyesChemical", new { id = vm.Id });
            }


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetOGPByGateActivity(long id)
        {
            if(id == (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_RETURN_IN)).Id)
            {
            var OGPList = (await _uow.GateTrService.GetAllByActivityId((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT)).Id)).ToSelectList(nameof(GateTr.Sno));
           // var OGPList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id).ToSelectList(nameof(GateTr.Sno));
            return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel
            {
                Name = "Xref",
                PlaceHolder = "Select OGP",
                UseDefault = false,
                SelectList = OGPList
            });
            }
            return PartialView("~/Views/Shared/_SelectList.cshtml");
        }
        public async Task AddDetail(long loanoutid, long HeaderId)
        {

                var igpDetails = (await _uow.GateTrService.GetById(loanoutid)).GateTrDetails.Where(x => x.IsDeleted == false);
                if (igpDetails != null)
                {
                    foreach (var d in igpDetails)
                    {
                        await _uow.GateTrDetailService.Create(new GateTrDetail
                        {
                            QtyCr = d.GateTr.GateActivityTypeId == (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT)).Id ? d.QtyDr : d.QtyCr,
                            QtyDr = d.GateTr.GateActivityTypeId == (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT)).Id ? d.QtyCr : d.QtyDr,
                            ChemicalId = d.ChemicalId,
                            DyeId = d.DyeId,
                            Description = d.Description,
                            GateXrefDetailId = d.Id,
                            GateTrId = HeaderId,
                            Rate = d.Rate,
                            Packet=d.Packet,
                            OGPGateTrDetailId=d.Id
                        });
                    }

            }
        }

        [HttpGet]
        public async Task<IActionResult> GateIGPLoanDyesChemicalDetailReport(long id)
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

        [HttpGet]

        public async Task<IActionResult> ChemicalDetail(long id)
        {
            var igpDetails =  (await _uow.GateTrService.GetById(id)).GateTrDetails.Where(x => x.IsDeleted == false);
            return PartialView(igpDetails);
        }



        //public IActionResult GetGateOGPLoanGivenOutDetail(long id)
        //{
        //    var igpDetails = _uow.GateTrDetailService.GetByheaderId(id);


        //    return PartialView(igpDetails);
        //}


    }
}