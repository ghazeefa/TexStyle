using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TexStyle.Common;
using TexStyle.Core.PPC;

using TexStyle.ViewModels;
using TexStyle.ViewModels.PPC;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using TexStyle.ViewModels.PPC.Forms;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class FabricQualityController : Controller
    {
       
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(FabricQuality)}";

        private readonly TempDataViewModel _tempData;
        private IFabricQualityService _FabricQualityService;
        private readonly IMapper _mapper;

        public FabricQualityController(IFabricQualityService FabricQualityService, IMapper mapper)
        {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(FabricQuality)}";
            _tempData = new TempDataViewModel();
            _FabricQualityService = FabricQualityService;
            _mapper = mapper;
        }




        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View((await _FabricQualityService.GetAll()).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            FabricQualityViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<FabricQualityViewModel>(await _FabricQualityService.GetById(id.Value));
            }
            return PartialView("AddOrUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, FabricQualityViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<FabricQuality>(vm);
                    if (!id.HasValue)
                    {
                        // create
                        await _FabricQualityService.Create(m);

                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update
                        await _FabricQualityService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col)
        {
            try
            {
                if (id.HasValue)
                {
                    await _FabricQualityService.Delete(await _FabricQualityService.GetById(id.Value));
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