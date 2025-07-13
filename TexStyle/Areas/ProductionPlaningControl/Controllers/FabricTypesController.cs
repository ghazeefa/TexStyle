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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.PRODUCTION_PLANING_CONTROL.Name)]
    public class FabricTypesController : Controller
      
    {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(FabricTypes)}";

        private readonly TempDataViewModel _tempData;
        private IFabricTypesService _FabricTypesService;
        private readonly IMapper _mapper;

        public FabricTypesController(IFabricTypesService FabricTypesService, IMapper mapper)
        {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(FabricTypes)}";
            _tempData = new TempDataViewModel();
            _FabricTypesService = FabricTypesService;
            _mapper = mapper;
        }




        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View((await _FabricTypesService.GetAll()).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            FabricTypesViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<FabricTypesViewModel>(await _FabricTypesService.GetById(id.Value));
            }
            return PartialView("AddOrUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, FabricTypesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<FabricTypes>(vm);
                    if (!id.HasValue)
                    {
                        // create
                        await _FabricTypesService.Create(m);
                       
                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update
                        await _FabricTypesService.Update(m);
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
                    await _FabricTypesService.Delete(await _FabricTypesService.GetById(id.Value));
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
