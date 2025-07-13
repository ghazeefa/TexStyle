using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Common;
using TexStyle.Core.YD;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.YD;

namespace TexStyle.Areas.YarnDyeing.Controllers {
    [Area(AreaConstants.YARN_DYEING.Name)]
    public class RecipeFormatHeaderController : Controller {


        private IRecipeFormatHeaderService _recipeFormatHeaderService;
        private IProcessTypeService _processTypeService;
        private IMapper _mapper;
        private IDyeService _dyeService;
        private IChemicalService _chemicalService;
        private IRecipeStepService _recipestepService;
        private IRecipeFormatDetailService _recipeFormatDetailService;

        public RecipeFormatHeaderController(IRecipeFormatHeaderService recipeFormatHeaderService,
            IProcessTypeService processTypeService, IMapper mapper, IDyeService dyeService, IChemicalService chemicalService,
            IRecipeStepService recipeStepService, IRecipeFormatDetailService recipeFormatDetailService) {
            _recipeFormatHeaderService = recipeFormatHeaderService;
            _processTypeService = processTypeService;
            _mapper = mapper;
            _dyeService = dyeService;
            _chemicalService = chemicalService;
            _recipestepService = recipeStepService;
            _recipeFormatDetailService = recipeFormatDetailService;
        }

        public async Task<IActionResult> Index() {

            return View(await _recipeFormatHeaderService.GetAll());
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
        //    return View(_recipeFormatHeaderService.GetBetweenDateRange(options.sd.Value, options.ed.Value));
        //}
        public async Task<IActionResult> AddOrUpdate(long? id) {
            RecipeFormatHeaderViewModel vm = null;

            var processTypeList =(await _processTypeService.GetAll()).ToSelectList();

            if (id.HasValue) {
                //RecipeFormatHeader vmt = _recipeFormatHeaderService.GetById(id.Value);
                vm = _mapper.Map<RecipeFormatHeaderViewModel>(await _recipeFormatHeaderService.GetById(id.Value));
                processTypeList.Find(x => Convert.ToInt64(x.Value) == vm.ProcessTypeId).Selected = true;

            }
            ViewBag.ProcessTypeList = processTypeList;
            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(RecipeFormatHeaderViewModel vm, long? id) {

            var v = _mapper.Map<RecipeFormatHeader>(vm);
            try {
                if (id.HasValue) {

                    if (vm.IsYarn==true)
                    {
                        v.IsYarn = true;

                    }
                    if (vm.IsFabric==true)
                    {
                        v.IsYarn = false;
                    }
                    await _recipeFormatHeaderService.Update(v);
                } else {

                    if (vm.IsYarn == true)
                    {
                        v.IsYarn = true;

                    }
                    if (vm.IsFabric == true)
                    {
                        v.IsYarn = false;
                    }

                    await _recipeFormatHeaderService.Create(v);
                }


            } catch (Exception ex) {

                throw ex;
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(long? id) {
            RecipeFormatHeader vm = null;
            var processTypeList = (await _processTypeService.GetAll()).ToSelectList();
            if (id != null) {
                vm = _mapper.Map<RecipeFormatHeader>(await _recipeFormatHeaderService.GetById(id.Value));
                if (vm.ProcessTypeId != null) {
                    processTypeList.Find(x => Convert.ToInt64(x.Value) == vm.ProcessTypeId).Selected = true;
                }
            }
            ViewBag.processTypeList = processTypeList;
            return View(vm);
        }

        public async Task<IActionResult> AddOrUpdateDetail(long? id, long? formatHeaderId) {
            ViewBag.RecipeStepList = null;
            ViewBag.DyeList = null;
            ViewBag.RecipeStepList = null;
            RecipeFormatDetailViewModel vm = new RecipeFormatDetailViewModel();
            var dyeList =(await _dyeService.GetAll()).ToSelectList();
            var recipeStepList =(await _recipestepService.GetAll()).ToSelectList();
            var chemicalList =(await _chemicalService.GetAll()).ToSelectList();

            if (id.HasValue) {
                vm = _mapper.Map<RecipeFormatDetailViewModel>(await _recipeFormatDetailService.GetById(id.Value));
                if (vm.DyeId != null) {
                    dyeList.Find(x => Convert.ToInt64(x.Value) == vm.DyeId).Selected = true;
                    ViewBag.DyeList = dyeList;

                } else if (vm.ChemicalId != null) {
                    chemicalList.Find(x => Convert.ToInt64(x.Value) == vm.ChemicalId).Selected = true;

                    ViewBag.ChemicalList = chemicalList;

                } else if (vm.RecipeStepId != null) {
                    recipeStepList.Find(x => Convert.ToInt64(x.Value) == vm.RecipeStepId).Selected = true;

                    ViewBag.RecipeStepList = recipeStepList;
                }

            } else {
                vm.RecipeFormatHeaderId = formatHeaderId.Value;
            }

            return PartialView(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDetail(long? id, RecipeFormatDetailViewModel vm) {
            RecipeFormatDetail m = _mapper.Map<RecipeFormatDetail>(vm);
            if (id.HasValue && id.Value != 0) {
                try {
                    await _recipeFormatDetailService.Update(m);
                } catch (Exception ex) {

                    throw ex;
                }

            } else {
                try {
                    await _recipeFormatDetailService.Create(m);
                } catch (Exception ex) {

                    throw ex;
                }

            }
            return RedirectToAction(nameof(Details)
                , new RouteValueDictionary(new { Id = m.RecipeFormatHeaderId }));
        }
        public async Task<IActionResult> Getdyes() {
            var dyeSelectList =(await _dyeService.GetAll()).ToSelectList();
            return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel {
                Name = "DyeId",
                PlaceHolder = "Select Dye",
                UseDefault = true,
                SelectList = dyeSelectList
            });
        }
        public async Task<IActionResult> Getchemicals() {
            var chemicalSelectList =(await _chemicalService.GetAll()).ToSelectList();
            return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel {
                Name = "ChemicalId",
                PlaceHolder = "Select Chemical",
                UseDefault = true,
                SelectList = chemicalSelectList
            });
        }
        public async Task<IActionResult> GetHead() {
            var headSelectList =(await _recipestepService.GetAll()).ToSelectList();
            return PartialView("~/Views/Shared/_SelectList.cshtml", new SelectListItemViewModel {
                Name = "RecipeStepId",
                PlaceHolder = "Select Recipe Step",
                UseDefault = true,
                SelectList = headSelectList
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDetail(long? id) {
            try {
                if (id.HasValue) {
                    await _recipeFormatDetailService.Delete(await _recipeFormatDetailService.GetById(id.Value));
                    return new StatusCodeResult(200);
                }
            } catch (Exception) {
                return new StatusCodeResult(500);
            }
            return new StatusCodeResult(400);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var o =await _recipeFormatHeaderService.GetById(id);

                if (o == null) return new NotFoundResult();

                await _recipeFormatHeaderService.Delete(o);

                if (o.IsDeleted == true) return new OkResult();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
                throw ex;
            }

            return new BadRequestResult();
        }

    }
}