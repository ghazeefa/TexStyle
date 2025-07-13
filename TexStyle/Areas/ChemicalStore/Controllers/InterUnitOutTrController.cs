using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class InterUnitOutTrController : Controller
    {
        public readonly IUnitOfWork _uow;
        public readonly IMapper _map;
        public InterUnitOutTrController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _map = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _uow.DyeChemicalTrService.GetAll());
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
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value , ChemicalTransactions.InterUnitOut));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            var supplierList = (await _uow.PartyService.GetAll()).ToSelectList();
            TrViewModel vm = new TrViewModel();
            if (id != null)
            {
                //edit
                vm = _map.Map<TrViewModel>(await _uow.DyeChemicalTrService.GetById(id.Value));
                supplierList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
            }
            else
            {
                //add

            }

            ViewBag.supplierList = supplierList;
            return PartialView(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var m = await _uow.DyeChemicalTrService.GetById(id);

            // if (m == null) return RedirectToAction(nameof(Index));
            var supplierList = (await _uow.PartyService.GetAll()).ToSelectList();
            //var dyeList = _uow.DyeService.GetAll().ToSelectList();
            //var chemicalList = _uow.ChemicalService.GetAll().ToSelectList();

            if (m != null)
            {
                supplierList.Find(x => Convert.ToInt64(x.Value) == m.PartyId).Selected = true;
            }

            ViewBag.supplierList = supplierList;
            //ViewBag.dyeList = dyeList;
            //ViewBag.chemicalList = chemicalList;


            return View(m);
        }
        //public IActionResult GetChemicalRate(long id)
        //{
        //    var c = _uow.ChemicalService.GetById(id).Rate;
        //              return Json(c);
        //}
         public async Task<IActionResult> GetDyeRate(long id)
        {
            var d = (await _uow.DyeService.GetById(id)).Rate;
                      return Json(d);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, TrViewModel v)
       {
            if (ModelState.IsValid)
            {
                DyeChemicalTr vm = new DyeChemicalTr();
                vm = _map.Map<DyeChemicalTr>(v);
                vm.TrType = ChemicalTransactions.InterUnitOut;
                if (id != null)
                {
                    //edit
                    
                   
                    //var oldo = _uow.DyeChemicalTrService.GetById(vm.Id);
                    var newo = await _uow.DyeChemicalTrService.Update(vm);
                  // if (oldo.TransactionDate != newo.TransactionDate) 
                    //_uow.TrLinkerMasterService.UpdateTrMaster(null, null, vm.TransactionDate, vm.Id, null, ChemicalTransactions.InterUnitOut);

                }
                else
                {
                    //add
                    await _uow.DyeChemicalTrService.Create(vm);
                }

                return RedirectToAction("Details", "InterUnitOutTr", new { Id = vm.Id });
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateDetail(long? trid, long? id)
           
        {
            var chemicalList =  (await _uow.ChemicalService.GetAll()).ToSelectList();
            var dyeList =  (await _uow.DyeService.GetAll()).ToSelectList();
        TrDetailViewModel vm = new TrDetailViewModel();

            if (id != null)
            {
                //edit
                vm = _map.Map<TrDetailViewModel>(await _uow.DyeChemicalTrDetailService.GetById(id.Value));

                if (vm.ChemicalId.HasValue)
                    chemicalList.Find(x => Convert.ToInt64(x.Value) == vm.ChemicalId).Selected = true;
                if (vm.DyeId.HasValue)
                    dyeList.Find(x => Convert.ToInt64(x.Value) == vm.DyeId).Selected = true;

            }
            else
            { vm.DyeChemicalTrId = trid.Value; }
            ViewBag.dyeList = dyeList;
            ViewBag.chemicalList = chemicalList;
            return PartialView(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDetail(long? id, TrDetailViewModel v)
        {
            DyeChemicalTrDetail vm = new DyeChemicalTrDetail();
            vm = _map.Map<DyeChemicalTrDetail>(v);
            vm.IsDr = false;
            if (id!=0)
            {
                //edit
              //  var oldo = _uow.DyeChemicalTrDetailService.GetById(vm.Id);
                var newo = await _uow.DyeChemicalTrDetailService.Update(vm);
               // if (oldo.DyeId != newo.DyeId || oldo.ChemicalId != newo.ChemicalId) _uow.TrLinkerMasterService.UpdateTrMaster(vm.DyeId, vm.ChemicalId, null, Convert.ToInt64(vm.DyeChemicalTrId), vm.Id, ChemicalTransactions.InterUnitOut);

            }
            else
            {
                //add

                await _uow.DyeChemicalTrDetailService.Create(vm);
                //TrLinkerMaster linker = new TrLinkerMaster();
                //linker.TrStatus = LinkerMasterTrStatus.Credit;
                //linker.DyeId = vm.DyeId;
                //linker.ChemicalId = vm.ChemicalId;
                //linker.DyeChemicalTrDetailId = vm.Id;
                //linker.DyeChemicalTrId = vm.DyeChemicalTrId;
                //linker.Date = _uow.DyeChemicalTrService.GetById(Convert.ToInt64(vm.DyeChemicalTrId)).TransactionDate;
                //_uow.TrLinkerMasterService.Create(linker);

            }

            return RedirectToAction("Details", "InterUnitOutTr", new { Id = v.DyeChemicalTrId });

        }

        //public IActionResult GetRate(long id)
        //{
        //    var p = _uow.GateTrService.GetById(id).Id;


        //    return (p);
        //}


            [HttpPost]
        public async Task<IActionResult> DeleteDetail(long? id, IFormCollection col)
        {
            try
            {
                if (id.HasValue)
                {
                    var o = await _uow.DyeChemicalTrDetailService.GetById(id.Value);
                    await _uow.DyeChemicalTrDetailService.Delete(o);
                 //   _uow.TrLinkerMasterService.DeletebyTRType(Convert.ToInt64(o.DyeChemicalTrId.Value), o.Id, ChemicalTransactions.InterUnitOut);

                    return new StatusCodeResult(200);
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
        [HttpGet]
        public async Task<IActionResult> InterUnitOutTrDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);
                ViewBag.reportTitle = nameof(DyeChemicalTr);
                ViewBag.reportStatus = "OUTWARD GATE PASS";
                
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<IActionResult> GetDebits(long id)
        {
            var p = (await _uow.DyeChemicalTrDetailService.GetAll()).Where(x => x.ChemicalId == id && x.IsDr == true).Select(e => new { e.Id, e.CreatedOn, e.QtyDr }).ToList();
            return Json(p);
        }
        public async Task<IActionResult> GetDebitDetail(long id)
        {
            var cre = (await _uow.DyeChemicalTrDetailService.GetAll()).Where(x => x.DrId == id && x.IsDr == false).Sum(x => x.QtyCr);
            var deb = (await _uow.DyeChemicalTrDetailService.GetAll()).Where(x => x.Id == id && x.IsDr == true).Select(z => new { z.QtyDr }).FirstOrDefault();
            var p = deb.QtyDr - cre;
            return Json(p);
        }
    }
}