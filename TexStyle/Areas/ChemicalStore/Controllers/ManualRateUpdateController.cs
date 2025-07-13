using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;
using TexStyle.ViewModels.PPC;

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]

    public class ManualRateUpdateController : Controller
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public ManualRateUpdateController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
        }



        [HttpGet]
        public async Task<IActionResult> Index(PagingOptions options)
        {
            var rec = await _uow.DyeChemicalTrService.ManualRateUpdateService();
            return View(rec);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            var det = await _uow.DyeChemicalTrDetailService.GetById(id.Value);

            ////ManualRateUpdateViewModel vm = null;

            ////if (id.HasValue)
            ////{
            ////    vm = _mapper.Map<ManualRateUpdateViewModel>(_uow.DyeChemicalTrDetailService.GetById(id.Value));

            ////}
            ViewBag.det = det;

            return PartialView(det);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, DyeChemicalTrDetail vm )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mod = await _uow.DyeChemicalTrDetailService.GetById(vm.Id);
                    mod.Rate = vm.Rate;
                    var m = _mapper.Map<DyeChemicalTrDetail>(mod);
                  
                        await _uow.DyeChemicalTrDetailService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                    _tempData.Error = ex.Message;
                }
            }

            return RedirectToAction(nameof(Index));
        }



    }
}
