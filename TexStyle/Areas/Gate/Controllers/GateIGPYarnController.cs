using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.Gate;
using TexStyle.Core.PPC;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Gate;

namespace TexStyle.Areas.Gate.Controllers
{
    [Area(AreaConstants.GATE.Name)]
    public class GateIGPYarnController : Controller
    {
        public IUnitOfWork _uow { get; set; }
        public IMapper _map { get; set; }
        public GateIGPYarnController(IUnitOfWork uow , IMapper map)
        {
            _uow = uow;
            _map = map;
        }
        //public IActionResult Index()
        //{
        //    return View(_uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.IGP_Yarn).Id));
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
            if  (filter.IsYarn==true)
            {
                return View(await _uow.GateTrService.GetBetweenDateRangeByActivityTypeId((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.IGP_Yarn)).Id, options.sd.Value, options.ed.Value));            
           }
            else
            {
                return View(await _uow.GateTrService.GetBetweenDateRangeByActivityTypeId((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.IGP_Fabric)).Id, options.sd.Value, options.ed.Value));
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var partylist = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateigptypelist = (await _uow.GateActivityTypeService.GetAll()).ToSelectList(); 
            var buyerlist= (await _uow.BuyerService.GetAll()).ToSelectList();
            var gatePassTypeList = (await _uow.GatePassTypeService.GetAll()).ToSelectList();
        //  var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();
            GateTrViewModel vm = new GateTrViewModel();
            if (id != null)
            {
                //edit
                vm = _map.Map<GateTrViewModel>(await _uow.GateTrService.GetById(id.Value));
                if (vm.PartyId != 0) partylist.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                if (vm.BuyerId != 0) buyerlist.Find(x => Convert.ToInt64(x.Value) == vm.BuyerId).Selected = true;
                if (vm.GateActivityTypeId != 0) gateigptypelist.Find(x => Convert.ToInt64(x.Value) == vm.GateActivityTypeId).Selected = true;
               if(vm.IsWithoutOGP==false )
                {  var gatetrist = (await _uow.GateTrService.GetAllByActivityId((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Yarn)).Id)).ToSelectList();
                if (vm.GateTrId != null) gatetrist.Find(x => Convert.ToInt64(x.Value) == vm.GateTrId).Selected = true;
                //    if (vm.YarnTypeId == 0) yarntypelist.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                ViewBag.gatetrist = gatetrist; }
               else
                { vm.IsReturnFromParty = false; }            
            }
            else
            {    
                if (filter.IsYarn == true)
                {
                    //add
                    long gateActId = (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.IGP_Yarn)).Id;
                    vm.GateActivityTypeId = gateActId;
                    gateigptypelist.Find(x => Convert.ToInt64(x.Value) == gateActId).Selected = true;
                }
                else
                {
                long gateActId = (await _uow.GateActivityTypeService.GetByName(GateActivityTypes.IGP_Fabric)).Id;
                vm.GateActivityTypeId = gateActId;
                gateigptypelist.Find(x => Convert.ToInt64(x.Value) == gateActId).Selected = true;                    
                }
            }
            ViewBag.buyerlist = buyerlist;
            ViewBag.partylist = partylist;
            ViewBag.gateigptypelist = gateigptypelist;
            ViewBag.gatePassTypeList = gatePassTypeList;
            ViewBag.filter = filter;
            //  ViewBag.yarntypelist = yarntypelist;

            return PartialView(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var buyerlist = (await _uow.BuyerService.GetAll()).ToSelectList();
            var m = await _uow.GateTrService.GetById(id);
            var partyList = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateIGPTypeList = (await _uow.GateActivityTypeService.GetAll()).ToSelectList();
            var gatetrist = (await _uow.GateTrService.GetAllByActivityId((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Yarn)).Id)).ToSelectList(nameof(GateTr.Sno));
            // var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();
            var gatePassTypeList = (await _uow.GatePassTypeService.GetAll()).ToSelectList();
            if (m != null)
            {
                if (m.PartyId != null) partyList.Find(x => Convert.ToInt64(x.Value) == m.PartyId).Selected = true;
                if (m.GateActivityTypeId != null) gateIGPTypeList.Find(x => Convert.ToInt64(x.Value) == m.GateActivityTypeId).Selected = true;
                //if (m.GateTrId != null) gatetrist.Find(x => Convert.ToInt64(x.Value) == m.GateTrId).Selected = true;
                ////if (m.GateTrId != null) gatetrist.Find(x => Convert.ToInt64(x.Value) == m.GateTrId).Selected = true;

                //  if (m.YarnTypeId!= null) yarntypelist.Find(x => Convert.ToInt64(x.Value) == m.YarnTypeId).Selected = true;
            }
            if (m.IsYarn == true)
            {
                if (m.BuyerId != null) buyerlist.Find(x => Convert.ToInt64(x.Value) == m.BuyerId).Selected = true;
            }
            ViewBag.buyerlist = buyerlist;
            ViewBag.partyList = partyList;
            ViewBag.gateIGPTypeList = gateIGPTypeList;   
            ViewBag.gateIGPTypeList = gateIGPTypeList;
             ViewBag.gatetrist = gatetrist;
            ViewBag.gatePassTypeList = gatePassTypeList;
            ViewBag.filter = filter;
            //ViewBag.yarntypelist = yarntypelist;
            return View(m);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, GateTrViewModel v)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            ModelState.Remove(nameof(v.Id));
            if (ModelState.IsValid)
            {
                try
                {
                    GateTr vm = new GateTr();
                     if (id.Value != 0)
                    {
                        //edit
                        vm = _map.Map<GateTr>(v);
                        if (vm.IsWithoutOGP == true) vm.IsReturnFromParty = true;
                        if (vm.GateTrId!= (await _uow.GateTrService.GetById(vm.Id)).GateTrId)
                        {
                           var ogpdetail = await _uow.GateTrService.GetById(vm.Id);

                            await Task.WhenAll(ogpdetail.GateTrDetails.Select(async d =>
                            {
                               await _uow.GateTrDetailService.Delete(d);
                            }));
                        }
                        if (filter.IsYarn == true)
                        {
                            await _uow.GateTrService.Update(vm);
                        }
                        else
                        {
                            await _uow.GateTrService.UpdateFabric(vm); //AddOrUpdatefabricdata

                        }
                        // _tempData.MSG = "Successfully Updated";
                    }
                    else
                    {
                        //add
                        vm = _map.Map<GateTr>(v);
                        if (vm.IsWithoutOGP == true) 
                            vm.IsReturnFromParty = true;
                        //var igpf = _igpService.CreateFabric(m);

                        //foreach (var igpd in md.GateTrDetails)

                        //{
                        //    _igpDetailService.Create(new InwardGatePassDetail
                        //    {
                        //        InwardGatePassId = igpf.Id,
                        //        TearWeightInKg = 0,
                        //        NetWeightInKg = Convert.ToDecimal(igpd.QtyDr),

                        //        NoOfRolls = Convert.ToDecimal(igpd.NoOfRolls),
                        //        FabricTypesId = Convert.ToInt64(igpd.FabricTypeId)

                        //    });
                        
                           var gate= await _uow.GateTrService.CreateBySno(vm);

                        if (vm.IsConfirm == true)
                        {
                            if (vm.IsReturnFromParty==true)
                            {
                                if (filter.IsYarn == true)
                                {
                                    await _uow.IGPService.Create(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = null,
                                        IsReWaxRecheck=true,
                                        IsReturnfromParty=true

                                    }
                                );
                                }
                            }
                            else if (vm.IsWithoutOGP == true)
                            {
                                if (filter.IsYarn == true)
                                {
                                    await _uow.IGPService.Create(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = null,

                                        IsWithoutOGP = true

                                    }
                                );
                                }                             
                                }
                            
                            else if (vm.IsReprocessed == true)
                            {
                                if (filter.IsYarn == true)
                                {
                                    await _uow.IGPService.Create(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = null,

                                        IsReprocessed = true

                                    }
                                );
                                }
                                else
                                {
                                    await _uow.IGPService.CreateFabric(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = Convert.ToInt64(gate.BuyerId),

                                        IsReprocessed = true
                                    }
                                );
                                }
                            }
                            else if (vm.IsForFinishing == true)
                            {                           
                                    await _uow.IGPService.CreateFabric(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = Convert.ToInt64(gate.BuyerId),

                                        IsForFinishing = true
                                    }
                                );                               
                            }
                            else if (vm.IsAfterFinishing == true)
                            {
                                    await _uow.IGPService.CreateFabric(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = Convert.ToInt64(gate.BuyerId),

                                        IsAfterFinishing = true
                                    }
                                );
                            }
                            else if (vm.IsForComercialFinishing == true)
                            {
                                    await _uow.IGPService.CreateFabric(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = Convert.ToInt64(gate.BuyerId),

                                        IsForComercialFinishing = true
                                    }
                                );

                                
                            }

                            else 
                            {

                                if (filter.IsYarn == true)
                                {
                                    await _uow.IGPService.Create(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = null

                                    }
                                );
                                }
                                else
                                {
                                    await _uow.IGPService.CreateFabric(new InwardGatePass
                                    {
                                        IgpDate = gate.Date,
                                        GateTrId = Convert.ToInt64(gate.Id),
                                        PartyId = Convert.ToInt64(gate.PartyId),
                                        GateReffId = gate.IGPReffNo,
                                        BilityNo = gate.BillityNo,
                                        BuyerId = Convert.ToInt64(gate.BuyerId)
                                    }
                                );

                                }



                            }


                           
                        }

                        //   _tempData.MSG = "Successfully Created";
                    }
                    if (vm.GateTrId != null)
                    {
                        var ogpdetail = await _uow.GateTrService.GetById(Convert.ToInt64(vm.GateTrId));
                        if (filter.IsYarn==true)
                        {
                            await Task.WhenAll(ogpdetail.GateTrDetails.Select(async d =>
                            {
                                var xy = await _uow.GateTrDetailService.Create(new GateTrDetail
                                {
                                    QtyCr = d.QtyDr,
                                    QtyDr = d.QtyCr,
                                    //FabricTypesId null,
                                    YarnTypeId = d.YarnTypeId,
                                    Description = d.Description,
                                    // GateXrefDetailId = d.Id,
                                    GateTrId = vm.Id,
                                    Rate = d.Rate

                                });
                            }));
                        }
                        else
                        {
                            //ogpdetail.GateTrDetails.ToList().ForEach(d =>
                            //{


                            //var xy=    _uow.GateTrDetailService.Create(new GateTrDetail
                            //    {
                            //        QtyCr = d.QtyDr,
                            //        QtyDr = d.QtyCr,
                            //        YarnTypeId = null,
                            //        FabricTypeId= d.FabricTypeId,
                            //        Description = d.Description,
                            //        NoOfRolls =d.NoOfRolls,
                            //        // GateXrefDetailId = d.Id,
                            //        GateTrId = vm.Id,
                            //        Rate = d.Rate

                            //    }); 
                            //});





                        }
                       
                    }
                    return RedirectToAction("Details", "GateIGPYarn", new { id = vm.Id });
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            return RedirectToAction("Details", "GateIGPYarn", new { id = v.Id });
        }

        public async Task<IActionResult> OGPDetails(long id)
        {
            //id = 351478;
            var OGPDETAIL = (await _uow.GateTrService.GetById(id)).GateTrDetails.Where(x => x.IsDeleted == false);
            //var m = _mapper.Map<DyeChemicalTrDetailViewModel>(returnout);++

            return PartialView(OGPDETAIL);
        }

        public async Task<IActionResult> GetOGPById(int value)
        {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            if (filter.IsYarn == true)
            {
                var gateTrList = (await _uow.GateTrService.GetAllByActivityId((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Yarn)).Id)).ToSelectList(nameof(GateTr.Sno));
                return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel
                {
                    Name = "GateTrId",
                    PlaceHolder = "Select OGP NO",
                    UseDefault = true,
                    SelectList = gateTrList
                });
            }

            else
            {
                var gateTrList = (await _uow.GateTrService.GetAllByActivityId((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.OGP_Fabric)).Id)).ToSelectList(nameof(GateTr.Sno));
                return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel
                {
                    Name = "GateTrId",
                    PlaceHolder = "Select OGP NO",
                    UseDefault = true,
                    SelectList = gateTrList
                });


            }
           

    }
        [HttpGet]
        public async Task<IActionResult> GateIGPYarnDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.GateTrService.GetById(id);
                ViewBag.reportTitle = nameof(GateTr);
                ViewBag.reportStatus = "INWARD GATE PASS";
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        //public IActionResult GetOGPByGateActivity(long id)
        //{
        //    if (id == _uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_RETURN_IN).Id)
        //    {
        //        var OGPList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LOAN_PARTY_GIVEN_OUT).Id).ToSelectList(nameof(GateTr.Sno));
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
        //[HttpPost]
        //public IActionResult Delete(long? id)
        //{
        //    try
        //    {
        //        if (id.HasValue)
        //        {
        //            _uow.GateIgpDetailService.Delete(_uow.GateIgpDetailService.GetById(id.Value));
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