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

namespace TexStyle.Areas.Gate.Controllers {
    [Area(AreaConstants.GATE.Name)]
    public class GateIGPDyesChemicalController : Controller {
        public IUnitOfWork _uow { get; set; }
        public IMapper _map { get; set; }
        public GateIGPDyesChemicalController(IUnitOfWork uow, IMapper map) {
            _uow = uow;
            _map = map;
        }
        //public IActionResult Index() {
        //    return View(_uow.GateTrService.GetAllByActivityStatus(true, false, false));
        //}
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
            return View(await _uow.GateTrService.GetBetweenDateRangeByActivityStatus(true, false, false, options.sd.Value, options.ed.Value));
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            var partylist = (await _uow.PartyService.GetAll()).ToSelectList();
            var gateigptypelist = (await _uow.GateActivityTypeService.GetAllByStatus(true, false, false)).ToSelectList();
            // var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();
            GateTrViewModel vm = new GateTrViewModel();
            if (id != null) {
                //edit
                vm = _map.Map<GateTrViewModel>(await _uow.GateTrService.GetById(id.Value));
                partylist.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
                gateigptypelist.Find(x => Convert.ToInt64(x.Value) == vm.GateActivityTypeId).Selected = true;
                //yarntypelist.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
            } else {
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
        public async Task<IActionResult> Details(long id) {
            var m = await _uow.GateTrService.GetById(id);
            var partyList =  (await _uow.PartyService.GetAll()).ToSelectList();
            var gateIGPTypeList =  (await _uow.GateActivityTypeService.GetAll()).ToSelectList();
            // var yarntypelist = _uow.YarnTypeService.GetAll().ToSelectList();

            if (m != null) {
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
        public async Task<IActionResult> AddOrUpdate(long? id, GateTrViewModel v) {
            ModelState.Remove(nameof(v.Id));
            if (ModelState.IsValid) {

                try {
                    GateTr vm = new GateTr();
                    if (id.Value != 0) {
                        //edit
                        vm = _map.Map<GateTr>(v);
                        await _uow.GateTrService.Update(vm);

                        // _tempData.MSG = "Successfully Updated";
                    } else {
                        //add
                        vm = _map.Map<GateTr>(v);

                        await _uow.GateTrService.CreateByActivityType(vm, true, false, false);
                        //   _tempData.MSG = "Successfully Created";
                        return RedirectToAction("Details", "GateIGPDyesChemical", new { id = vm.Id });
                    }
                }
                catch (Exception ex) {

                    throw ex;
                }

            }


            return RedirectToAction();
        }
        [HttpGet]
        public async Task<IActionResult> GateIGPDyesChemicalDetailReport(long id)
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
    }
}