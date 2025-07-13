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
    public class RollMarkingController : Controller
    {


        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(RollMarking)}";

        private readonly TempDataViewModel _tempData;
        private IRollMarkingService _RollMarkingService;
        private readonly IMapper _mapper;

        public RollMarkingController(IRollMarkingService RollMarkingService, IMapper mapper)
        {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(RollMarking)}";
            _tempData = new TempDataViewModel();
            _RollMarkingService = RollMarkingService;
            _mapper = mapper;
        }




        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View((await _RollMarkingService.GetAll()).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            RollMarkingViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<RollMarkingViewModel>(await _RollMarkingService.GetById(id.Value));
            }
            return PartialView("AddOrUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, RollMarkingViewModel vm)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    var m = _mapper.Map<RollMarking>(vm);
                    if (!id.HasValue)
                    {
                        // create
                        await _RollMarkingService.Create(m);

                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update
                        await _RollMarkingService.Update(m);
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

        public async Task<IActionResult> Details(long id)
        {
           
            var m = await _RollMarkingService.GetById(id);
         
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col)
        {
            try
            {
                if (id.HasValue)
                {
                    await _RollMarkingService.Delete(await _RollMarkingService.GetById(id.Value));
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