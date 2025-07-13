using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TexStyle.ApplicationServices.Interfaces.IAnalysis;
using TexStyle.Common;
using TexStyle.Core.Analysis;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.Analysis;

namespace TexStyle.Areas.Analysis.Controllers {
    [Area(AreaConstants.Analysis.Name)]
    public class AnalysisTypeController : Controller {
        [ViewData]
        public string AreaName { get; set; }
        private string _ViewPath = $"/Areas/{AreaConstants.Analysis.Name}/Views/{nameof(AnalysisType)}";

        private readonly TempDataViewModel _tempData;
        private readonly IAnalysisTypeService _analysisTypeService;
        private readonly IMapper _mapper;
        public AnalysisTypeController(IAnalysisTypeService analysisTypeService, IMapper mapper) 
        {
            AreaName = $"{AreaConstants.Analysis.NormalizedName} / {nameof(AnalysisType)}";
            _tempData = new TempDataViewModel();
            _analysisTypeService = analysisTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            return View(await _analysisTypeService.GetAll());
        }

        //[HttpGet]
        //public IActionResult Index([FromQuery] FilterOptions options) {
        //    var today = DateTime.Now;
        //    var startDate = new DateTime(today.Year, today.Month, 1);
        //    var endDate = startDate.AddMonths(1).AddDays(-1);
        //    if (!options.sd.HasValue || !options.ed.HasValue) {
        //        options.sd = startDate;
        //        options.ed = endDate;
        //    }
        //    ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
        //    return View(_buyerColorService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}

        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id) {
            AnalysisTypeViewModel vm = null;
            if (id.HasValue) {
                vm = _mapper.Map<AnalysisTypeViewModel>(await _analysisTypeService.GetById(id.Value));
            }
            return PartialView($"{_ViewPath}/{nameof(AddOrUpdate)}.cshtml", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(long? id, AnalysisTypeViewModel vm) {
            if (ModelState.IsValid) {
                try {
                    var m = _mapper.Map<AnalysisType>(vm);
                    if (!id.HasValue) {
                        // create
                        await _analysisTypeService.Create(m);
                        _tempData.MSG = "Successfully Created";
                    } else {
                        //update
                        await _analysisTypeService.Update(m);
                        _tempData.MSG = "Successfully Updated";
                    }
                }
                catch (Exception ex) {
                    _tempData.Error = ex.Message;
                    throw ex;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long? id, IFormCollection col) {
            try {
                if (id.HasValue) {
                    await _analysisTypeService.Delete(await _analysisTypeService.GetById(id.Value));
                    return new StatusCodeResult(StatusCodes.Status200OK);
                }
            }
            catch (Exception) {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }
    }
}