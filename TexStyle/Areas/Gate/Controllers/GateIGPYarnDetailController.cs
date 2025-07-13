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
    public class GateIGPYarnDetailController : Controller {
        public IUnitOfWork _uow { get; set; }
        public IMapper _map { get; set; }
        private readonly TempDataViewModel _tempData;
        public GateIGPYarnDetailController(IUnitOfWork uow, IMapper map) {
            _uow = uow;
            _map = map;

        }

        public async Task<IActionResult> Index(long? id) {
            return View(await _uow.GateTrDetailService.GetAll());
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
            return View(await _uow.GateTrDetailService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        }


        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id, long? ParentId) {

            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            var yarnTypeList = (await _uow.YarnTypeService.GetAll()).ToSelectList();

            var FabricTypeList = (await _uow.FabricTypesService.GetAll()).ToSelectList();
            GateTrDetailViewModel vm = new GateTrDetailViewModel();

            if (id != null)
            {
                //edit
                vm = _map.Map<GateTrDetailViewModel>(_uow.GateTrDetailService.GetById(id.Value));
                //if (vm.DyeId != null) dyeList.Find(x => Convert.ToInt64(x.Value) == vm.DyeId).Selected = true;
                //    if (vm.ChemicalId != null) chemicalList.Find(x => Convert.ToInt64(x.Value) == vm.ChemicalId).Selected = true;
                if (vm.YarnTypeId != 0) yarnTypeList.Find(x => Convert.ToInt64(x.Value) == vm.YarnTypeId).Selected = true;
                if (vm.FabricTypeId != 0) FabricTypeList.Find(x => Convert.ToInt64(x.Value) == vm.FabricTypeId).Selected = true;
           
            } 
            else {

               if(ParentId!= null) vm.GateTrId = ParentId.Value;
            }
            //ViewBag.dyeList = dyeList;
            //ViewBag.chemicalList = chemicalList;
            if ((await _uow.GateTrService.GetById(ParentId.Value)).GateTrId != null)
            { var ogop = (await _uow.GateTrService.GetById(Convert.ToInt64(ParentId.Value))).GateTrs.Sno;
                ViewBag.ogpId = ogop;
            }
            ViewBag.filter = filter;
            ViewBag.fabricTypeList = FabricTypeList;
            ViewBag.yarnTypeList = yarnTypeList;
            return PartialView(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, GateTrDetailViewModel v) {
            var filter =  (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();
            GateTrDetail vm = new GateTrDetail();
            if (ModelState.IsValid) {

                try {

                    if (id.Value != 0) {
                        //edit
                        vm = _map.Map<GateTrDetail>(v);

                        if (filter.IsYarn == true)
                            vm.FabricTypeId = null;
                        else
                            vm.YarnTypeId = null;
                        await _uow.GateTrDetailService.Update(vm);

                        //_tempData.MSG = "Successfully Updated";
                    } else {
                        //add
                        vm = _map.Map<GateTrDetail>(v);

                        if (filter.IsYarn == true)
                            vm.FabricTypeId = null;
                        else
                            vm.YarnTypeId = null;
                        var gate = await _uow.GateTrDetailService.Create(vm);

                        var igp = await _uow.IGPService.GetByXreffId(vm.GateTrId.Value);


                            // get IGPheader data using xrefrance of Gate =GateXrefDetailId 
                            // object Id 

                        if (filter.IsYarn==true)
                        {
                            await _uow.IGPDetailService.Create(new InwardGatePassDetail
                            {FabricTypesId=null,
                              InwardGatePassId= igp.Id,
                                YarnTypeId= Convert.ToInt64(gate.YarnTypeId),
                                 Bags=gate.Bags.Value,
                                TearWeightInKg = 0,
                                NetWeightInKg = Convert.ToDecimal(gate.QtyDr),


                            }
                           );

                        }

                        else
                        {
                            await _uow.IGPDetailService.Create(new InwardGatePassDetail
                            {
                                YarnTypeId= null,
                                InwardGatePassId = igp.Id,
                                FabricTypesId = Convert.ToInt64(gate.FabricTypeId),
                                FabricTypes = gate.FabricTypes,
                                NoOfRolls = gate.NoOfRolls.Value,
                                TearWeightInKg = 0,
                                NetWeightInKg = Convert.ToDecimal(gate.QtyDr),


                            }
                           );


                        }
                        // _tempData.MSG = "Successfully Created";
                    }
                }
                catch (Exception ex) {

                    throw ex;
                }

            }


            return RedirectToAction("Details", "GateIGPYarn", new { id = vm.GateTrId });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long? id) {
            try {
                if (id.HasValue) {
                    await _uow.GateTrDetailService.Delete(await _uow.GateTrDetailService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }
    }
}