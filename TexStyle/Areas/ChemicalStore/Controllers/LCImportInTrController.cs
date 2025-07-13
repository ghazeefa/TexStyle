using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Core.Gate;
using TexStyle.Core.ReportsViewModel.CS.RateDetail;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class LCImportInTrController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public LCImportInTrController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uow.DyeChemicalTrService.GetAll());
        }


        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            //COMMENTED FOR TIME BEING
            // var endDate = startDate.AddMonths(1).AddDays(-1); 
            var startTempDate = new DateTime(2018, 1, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startTempDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value, ChemicalTransactions.LCImport));
        }


        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {

            var partyList = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateIgpList = (await _uow.DyeChemicalTrService.Vw_Chemical_UncopiedGatePasINViewModel(Convert.ToInt64((await _uow.GateActivityTypeService.GetByName(GateActivityTypes.LC_IMPORT_IN)).Id))).ToSelectList(nameof(GateTr.Sno));

            // var gateIgpList = _uow.GateTrService.GetAllByActivityId(_uow.GateActivityTypeService.GetByName(GateActivityTypes.LC_IMPORT_IN).Id).ToSelectList(nameof(GateTr.Sno));

            TrViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<TrViewModel>(_uow.DyeChemicalTrService.GetById(id.Value));

                partyList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                gateIgpList.Find(x => Convert.ToInt64(x.Value) == vm.GateTrId).Selected = true;
            }
            ViewBag.partyList = partyList;
            ViewBag.gateIgpList = gateIgpList;

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrViewModel vm)
        {
            ModelState.Remove(nameof(vm.Id));
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<DyeChemicalTr>(vm);
                    m.TrType = ChemicalTransactions.LCImport;
                    var lc = new DyeChemicalTr();
                    if (!id.HasValue)
                    {
                        // create

                        lc = await _uow.DyeChemicalTrService.Create(m);

                        vm.Id = lc.Id;
                        _tempData.MSG = "Successfully Created";
                        AddDetail(lc.GateTrId.Value, lc.Id);
                        //return RedirectToAction(nameof(AddDetail), new { GateTrId = 0, HeaderId = lc.Id });
                    }

                    else
                    {
                        //update
                        var oldo = await _uow.DyeChemicalTrService.GetById(m.Id);
                        lc = await _uow.DyeChemicalTrService.Update(m);
                        //update linker master Date


                        //if (oldo.TransactionDate != lc.TransactionDate) _uow.TrLinkerMasterService.UpdateTrMaster(null, null, m.TransactionDate, m.Id, null, ChemicalTransactions.LCImport);

                        var o = await _uow.DyeChemicalTrService.GetById(lc.Id);
                        if (oldo.GateTrId != o.GateTrId)
                        {
                            await Task.WhenAll(o.DyeChemicalTrDetails.Select(async de =>
                            {
                                // await _uow.Service.DeletebyTRType(Convert.ToInt64(de.DyeChemicalTrId.Value), de.Id, ChemicalTransactions.LCImport);
                                await _uow.DyeChemicalTrDetailService.Delete(de);
                                // delete linker tr
                            }));

                            AddDetail(lc.GateTrId.Value, lc.Id);
                        }



                        _tempData.MSG = "Successfully Updated";
                    }



                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return RedirectToAction(nameof(Details), new { id = vm.Id });
        }

        public async Task<IActionResult> AddDetail(long GateTrId, long HeaderId)
        {
            if (GateTrId != 0)
            {
                var igpDetails = (await _uow.GateTrService.GetById(GateTrId)).GateTrDetails.Where(x => x.IsDeleted == false);

                foreach (var d in igpDetails)
                {
                    var detail = await _uow.DyeChemicalTrDetailService.Create(new DyeChemicalTrDetail
                    {
                        QtyCr = d.QtyCr,
                        QtyDr = d.QtyDr,
                        ChemicalId = d.ChemicalId,
                        Packet = d.Packet,
                        Rate = d.Rate.Value,
                        DyeId = d.DyeId,
                        DyeChemicalTrId = HeaderId,
                        GateTrDetailId = d.Id,
                        //Description = d.Description,
                        IsDr = true
                        //add linker detail 

                    }) ;
                    //TrLinkerMaster linker = new TrLinkerMaster();
                    //   linker.TrStatus = LinkerMasterTrStatus.Debit;
                    //   linker.DyeId = d.DyeId;
                    //   linker.ChemicalId = d.ChemicalId;
                    //   linker.DyeChemicalTrId = HeaderId;
                    //   linker.DyeChemicalTrDetailId = detail.Id;
                    //   linker.Date = _uow.DyeChemicalTrService.GetById(Convert.ToInt64(HeaderId)).TransactionDate;
                    //   _uow.TrLinkerMasterService.Create(linker);
                }
            }
            var m = await _uow.DyeChemicalTrService.GetById(HeaderId);
            return View(m);
        }
        public async Task<IActionResult> ChemicalDetail(long id)
        {
            var igpDetails = (await _uow.GateTrService.GetById(id)).GateTrDetails.Where(x => x.IsDeleted == false);
            return PartialView(igpDetails);
        }
        public async Task<IActionResult> GetParty(long id)
        {



            var p = await _uow.GateTrService.GetById(id);

            var result = new
            {
                id = p.PartyId,

                Name = p.Party.Name,
                Date = p.Date.ToShortDateString(),
            };



            //var p = _uow.GateTrService.GetById(id).Party;
            //var d= _uow.GateTrService.GetById(id).Date;

            return Json(result);

        }



        public async Task<IActionResult> Details(long Id)
        {

            var m = await _uow.DyeChemicalTrService.GetById(Id);
            var gateIGPTypeList = (await _uow.GateTrService.GetAll()).ToSelectList(nameof(GateTr.Sno));

            if (m != null)
            {
                if (m.GateTrId != null) gateIGPTypeList.Find(x => Convert.ToInt64(x.Value) == m.GateTrId).Selected = true;
            }

            ViewBag.gateIGPTypeList = gateIGPTypeList;


            return View(m);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col)
        {
            try
            {
                if (id.HasValue)
                {
                    await _uow.DyeChemicalTrService.Delete(await _uow.DyeChemicalTrService.GetById(id.Value));
                    return new StatusCodeResult(StatusCodes.Status200OK);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        [HttpGet]
        public async Task<IActionResult> LCImportDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);
                ViewBag.reportTitle = nameof(DyeChemicalTr);
                ViewBag.reportStatus = "INWARD GATE PASS";
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateRatess(string list)
        {
            try
            {
                var abc = JsonConvert.DeserializeObject<List<RateDetail>>(list);
                await _uow.GateTrDetailService.UpdateRates(abc);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(new { msg = "s" });



        }
    }
}