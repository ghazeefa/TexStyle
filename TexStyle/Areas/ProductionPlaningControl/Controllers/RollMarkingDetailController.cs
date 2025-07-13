
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC.Forms;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class RollMarkingDetailController : Controller
    {


        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(RollMarkingDetail)}";

        private readonly TempDataViewModel _tempData;
        private IRollMarkingDetailService _RollMarkingDetailService;
        private readonly IMapper _mapper;

        public RollMarkingDetailController(IRollMarkingDetailService RollMarkingDetailService, IMapper mapper)
        {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(RollMarkingDetail)}";
            _tempData = new TempDataViewModel();
            _RollMarkingDetailService = RollMarkingDetailService;
            _mapper = mapper;
        }




        // GET: /<controller>/
        //        public IActionResult Index()
        //        {
        //            return View(_RollMarkingDetailService.GetAll().ToList());
        //        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id, long? ParentId)
        {



            RollMarkingDetailViewModel vm = new RollMarkingDetailViewModel();

            if (id != null)
            {
                //edit
                vm = _mapper.Map<RollMarkingDetailViewModel>(await _RollMarkingDetailService.GetById(id.Value));


            }
            else
            {

                if (ParentId != null) vm.RollMarkingId = ParentId.Value;
            }

            //if (_uow.GateTrService.GetById(ParentId.Value).GateTrId != null)
            //{
            //    var ogop = _uow.GateTrService.GetById(Convert.ToInt64(ParentId.Value)).GateTrs.Sno;
            //    ViewBag.ogpId = ogop;
            //}

            return PartialView(vm);




            //RollMarkingViewModel vm = null;
            //if (id.HasValue)
            //{
            //    vm = _mapper.Map<RollMarkingViewModel>(_RollMarkingService.GetById(id.Value));
            //}

            //return PartialView("AddOrUpdate", vm);

        }

        //        [HttpPost]
        //        public IActionResult AddOrUpdate(long? id, RollMarkingDetailViewModel vm)
        //        
        //            //if (ModelState.IsValid)
        //            //{
        //            try
        //            {
        //                vm.RollMarkingId = 1;
        //                var m = _mapper.Map<RollMarkingDetail>(vm);
        //                if (!id.HasValue)
        //                {
        //                    // create
        //                    _RollMarkingDetailService.Create(m);

        //                    _tempData.MSG = "Successfully Created";
        //                }
        //                else
        //                {
        //                    //update
        //                    _RollMarkingDetailService.Update(m);
        //                    _tempData.MSG = "Successfully Updated";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                _tempData.Error = ex.Message;
        //            }
        //            //}

        //            return RedirectToAction(nameof(RollMarkingController.Details), "RollMarking", new { id = vm.RollMarkingId });
        //        }




        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, RollMarkingDetailViewModel v)
        {
            RollMarkingDetail vm = new RollMarkingDetail();
            if (ModelState.IsValid)
            {

                try
                {

                    if (id.Value != 0)
                    {
                        //edit
                        vm = _mapper.Map<RollMarkingDetail>(v);
                        await _RollMarkingDetailService.Update(vm);

                        //_tempData.MSG = "Successfully Updated";
                    }
                    else
                    {
                        vm.RollMarkingId = v.RollMarkingId;
                        vm.EcruKgs = v.EcruKgs;
                        vm.DyedKgs = v.DyedKgs;
                        vm.Status = v.Status;
                        //vm.RollMarkingId = v.RollMarkingId;

                        // vm = _mapper.Map<RollMarkingDetail>(v);
                        await _RollMarkingDetailService.Create(vm);

                    }

                }

                catch (Exception ex)
                {

                    throw ex;
                }


            }

            return RedirectToAction(nameof(RollMarkingController.Details), "RollMarking", new { id = v.RollMarkingId });
        }



    }
}
