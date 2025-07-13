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

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class DyesChemicalOpenningController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly TempDataViewModel _tempData;
        public DyesChemicalOpenningController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
            _tempData = new TempDataViewModel();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uow.DyeChemicalTrService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            //COMMENTED FOR TIME BEING
            // var endDate = startDate.AddMonths(1).AddDays(-1); 
            var startTempDate = new DateTime(2018, 1, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startTempDate;
                options.ed = endDate;
            }
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRange(options.sd.Value, options.ed.Value, ChemicalTransactions.OpeningBalance));
        }

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {

            //var supplierList = _uow.PartyService.GetAll().ToSelectList();

            TrViewModel vm = null;
            if (id.HasValue)
            {
                vm = _mapper.Map<TrViewModel>(await _uow.DyeChemicalTrService.GetById(id.Value));

                //supplierList.Find(x => Convert.ToInt64(x.Value) == vm.PartyId).Selected = true;
            }
            //  ViewBag.supplierList = supplierList;

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
                    m.TrType = ChemicalTransactions.OpeningBalance;
                    if (!id.HasValue)
                    {
                        // create
                        var d = await _uow.DyeChemicalTrService.GetAllByTrType(ChemicalTransactions.OpeningBalance);

                        if (d.Count() != 0)
                        {
                            DateTime lastdate = d.LastOrDefault().TransactionDate.AddYears(1);

                            if (DateTime.Compare(lastdate, vm.TransactionDate) >= 0)
                            {
                                await _uow.DyeChemicalTrService.Create(m);
                                 _tempData.MSG = "Successfully Created";
                                return RedirectToAction("Details", "DyesChemicalOpenning", new { Id = m.Id });
                            }
                            else
                            {
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            await _uow.DyeChemicalTrService.Create(m);
                            _tempData.MSG = "Successfully Created";
                        }
                        //   DateTime newdate = vm.TransactionDate;


                    }
                    else
                    {
                        //update
                        var oldo = await _uow.DyeChemicalTrService.GetById(m.Id);
                        var newo = await _uow.DyeChemicalTrService.Update(m);
                        // if (oldo.TransactionDate != newo.TransactionDate) _uow.TrLinkerMasterService.UpdateTrMaster(null, null, m.TransactionDate, m.Id, null, ChemicalTransactions.OpeningBalance);


                        _tempData.MSG = "Successfully Updated";
                    }
                    return RedirectToAction("Details", "DyesChemicalOpenning", new { Id = m.Id });
                }
                catch (Exception ex)
                {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return View();
        }

        public async Task<IActionResult> Details(long Id)
        {

            var m = await _uow.DyeChemicalTrService.GetById(Id);
            //var supplierList = _uow.PartyService.GetAll().ToSelectList();

            if (m != null)
            {
                // supplierList.Find(x => Convert.ToInt64(x.Value) == m.PartyId).Selected = true;
            }

            //  ViewBag.supplierList = supplierList;
            return View(m);

        }
    }
}