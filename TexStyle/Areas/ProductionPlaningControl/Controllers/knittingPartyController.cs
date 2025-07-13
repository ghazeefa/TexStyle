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
    public class knittingPartyController : Controller
    {


        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.PRODUCTION_PLANING_CONTROL.Name}/Views/{nameof(knittingParty)}";

        private readonly TempDataViewModel _tempData;
        private IknittingPartyService _knittingPartyService;
        private readonly IMapper _mapper;

        public knittingPartyController(IknittingPartyService knittingPartyService, IMapper mapper)
        {
            AreaName = $"{AreaConstants.PRODUCTION_PLANING_CONTROL.NormalizedName} / {nameof(knittingParty)}";
            _tempData = new TempDataViewModel();
            _knittingPartyService = knittingPartyService;
            _mapper = mapper;
        }




        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View((await _knittingPartyService.GetAll()).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            knittingPartyViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<knittingPartyViewModel>(await _knittingPartyService.GetById(id.Value));
            }
            return PartialView("AddOrUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, knittingPartyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var m = _mapper.Map<knittingParty>(vm);
                    if (!id.HasValue)
                    {
                        // create
                        await _knittingPartyService.Create(m);

                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update
                        await _knittingPartyService.Update(m);
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
                    await _knittingPartyService.Delete(await _knittingPartyService.GetById(id.Value));
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