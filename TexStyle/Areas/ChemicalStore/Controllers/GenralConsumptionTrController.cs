using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.CS.Forms;

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class GenralConsumptionTrController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public GenralConsumptionTrController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
        }

        //public IActionResult Index() {
        //    return View(_uow.DyeChemicalTrService.GetAll());
        //}

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value, ChemicalTransactions.GeneralConsumption));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            TrViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<TrViewModel>(await _uow.DyeChemicalTrService.GetById(id.Value));
            }
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
                    m.TrType = ChemicalTransactions.GeneralConsumption;
                    if (!id.HasValue)
                    {
                        // create
                        //m.TrType = ChemicalTransactions.Dilution;
                        await _uow.DyeChemicalTrService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    }
                    else
                    {
                        //update
                        //var oldo=  _uow.DyeChemicalTrService.GetById(m.Id);
                        var newo = await _uow.DyeChemicalTrService.Update(m);
                        //if (oldo.TransactionDate != newo.TransactionDate) _uow.
                        //Service.UpdateTrMaster(null, null, m.TransactionDate, m.Id, null, ChemicalTransactions.Dilution);


                        _tempData.MSG = "Successfully Updated";
                    }
                    return RedirectToAction("Details", "GenralConsumptionTr", new { Id = m.Id });
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                    throw ex;
                }

            }
            return View();

        }

        public async Task<IActionResult> Details(long Id) => View(await _uow.DyeChemicalTrService.GetById(Id));

        [HttpPost]
        public async Task<IActionResult> Delete(long? id)
        {
            try
            {
                if (id.HasValue)
                {
                    await _uow.DyeChemicalTrService.Delete(await _uow.DyeChemicalTrService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }


        [HttpGet]
        public async Task<IActionResult> ChemicalDilutionDetailReport(long id)
        {
            try
            {
                if (id == 0) return BadRequest();
                var planin = await _uow.DyeChemicalTrService.GetById(id);
                ViewBag.reportTitle = nameof(ChemicalDilutionTr);
                ViewBag.reportStatus = "IN/OUT General Consumption";
                return View(planin);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
