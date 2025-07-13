using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.YD;
using TexStyle.Extensions;
using TexStyle.ViewModels;
using TexStyle.ViewModels.YD;
using TexStyle.ViewModels.YD.Reports;

namespace TexStyle.Areas.ProductionPlaningControl.Controllers
{
    [Area(AreaConstants.YARN_DYEING.Name)]
    public class RecipeTimeController : Controller {
        private readonly TempDataViewModel _tempData;
        private IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public static int counts = 0;
        public RecipeTimeController(IUnitOfWork uow, IMapper mapper) {
            _tempData = new TempDataViewModel();
            _uow = uow;
            _mapper = mapper;
        
    }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View(_uow.RecipeService.GetAll().ToList());
        //}

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options) {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue) {
                options.sd = startDate;
                options.ed = endDate;
            }
            var dt = options.sd.Value;

            var df = options.ed.Value;

            var de = dt.Date;
            var dc = dt.Date;

            //var ress = _uow.RecipeService.GetById(151037);
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            var res =(await _uow.RecipeService.GetBetweenDateRange(options.sd.Value, options.ed.Value)).OrderBy(m=>m.No);
            var fromDate = new DateTime(2023, 11, 1);
         var x =(await _uow.RecipeService.RecipeIndexViewModelRepository(options.sd.Value, options.ed.Value)).Where(a => a.IsTimeAdded != true && a.Date >= fromDate);
          //  var vm = _mapper.Map<List<RecipeViewModel>>(x);


            return View(x);
        }

        [HttpGet]
        public async Task<IActionResult> TimeAdded([FromQuery] FilterOptions options)
        {
            var today = DateTime.Now;
            var startDate = new DateTime(today.Year, today.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            if (!options.sd.HasValue || !options.ed.HasValue)
            {
                options.sd = startDate;
                options.ed = endDate;
            }
            var dt = options.sd.Value;

            var df = options.ed.Value;

            var de = dt.Date;
            var dc = dt.Date;

            //var ress = _uow.RecipeService.GetById(151037);
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            var res =(await _uow.RecipeService.GetBetweenDateRange(options.sd.Value, options.ed.Value)).OrderBy(m => m.No);
            var fromDate = new DateTime(2023, 11, 1);
            var x =(await _uow.RecipeService.RecipeIndexViewModelRepository(options.sd.Value, options.ed.Value)).Where(a => a.IsTimeAdded == true && a.Date >= fromDate);
            //  var vm = _mapper.Map<List<RecipeViewModel>>(x);


            return View(x);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrUpdate(long? id)
        {
            //var machineTypeList = _uow.MachineTypeService.GetAll().ToSelectList();
            //var recipeFormatList = _uow.RecipeFormatHeaderService.GetAll().ToSelectList();
            //var shiftViewModelList = new List<ShiftViewModel>
            //    {
            //        new ShiftViewModel { Id = 1, ShiftName = "A" },
            //        new ShiftViewModel { Id = 2, ShiftName = "B" }
            //    };

            //var selectList = new SelectList(shiftViewModelList, "Id", "ShiftName");
            //var shiftViewModelSelectListItems = selectList.Select(x => new SelectListItem { Value = x.Text, Text = x.Text }).ToList();


            var vm = new RecipeViewModel();


            if (id.HasValue)
            {
                var cs = await _uow.DyeChemicalTrService.CountDyeChemicalTrByRecipeId(id.Value);
                if (cs > 1)
                {
                    throw new Exception();

                }
                vm = _mapper.Map<RecipeViewModel>(await _uow.RecipeService.GetById(id.Value));
                //machineTypeList.Find(x => Convert.ToInt64(x.Value) == vm.MachineTypeId).Selected = true;
                //recipeFormatList.Find(x => Convert.ToInt64(x.Value) == vm.RecipeFormatId).Selected = true;
                //shiftViewModelSelectListItems.Find(x => x.Value == vm.ShiftName).Selected = true;
            }
            else
            {
                var item =(await _uow.RecipeService.GetAll(true)).Count();
                int max = (int)(await _uow.RecipeService.GetAll(true)).Max(x => x.No);
                vm.No = item == 0 ? 1.0m : (max + 1.0m);
            }

            //ViewBag.machineTypeList = machineTypeList;
            //ViewBag.recipeFormatList = recipeFormatList;
            //ViewBag.shiftNameList = shiftViewModelSelectListItems;

            return PartialView(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate([FromRoute] long? id, [FromForm] RecipeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var m = new Recipe();
                m =(await _uow.RecipeService.GetById(vm.Id.Value));

                m.MachineStartTime = vm.MachineStartTime;
                m.MachineUnloadTime = vm.MachineUnloadTime;
                m.SoapingDrainTime = vm.SoapingDrainTime;

                if (!(m.MachineStartTime == null) && !(m.MachineUnloadTime == null) && !(m.SoapingDrainTime == null))
                {
                    m.IsTimeAdded = true;
                }
                else
                {
                    m.IsTimeAdded = null;
                }

                try
                {
                    //update
                    await _uow.RecipeService.Update(m);
                    _tempData.MSG = "Successfully Updated";
                    
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    throw ex;
                    _tempData.Error = ex.Message;
                }

            }

            return View();

        }



        public async Task RecipeDetailAddUpdate(long recformatId, long recipeId, bool isadd) {

            if (!isadd) await _uow.RecipeDetailService.DeletebyId(recipeId);
            var format = await _uow.RecipeFormatHeaderService.GetById(Convert.ToInt64(recformatId));
            List<RecipeDetail> recipeDetail = (from c in format.RecipeFormatDetails
                                               let a = new RecipeDetail() {
                                                   DyeId = c.DyeId,
                                                   ChemicalId = c.ChemicalId,
                                                   RecipeStepId = c.RecipeStepId,
                                                   Gpl = c.Gpl,
                                                   Percentage = c.Percentage,
                                                   Sno = c.Sno,
                                                   RecipeId = recipeId
                                               }
                                               select a).ToList();
            if (recipeDetail != null) {


                await Task.WhenAll(recipeDetail.Select(async detail =>
                {
                    await _uow.RecipeDetailService.Create(detail);
                }));
            }

        }
        public async Task<IActionResult> AddLPS(long id) {
            var m = new RecipeLPSViewModel { RecipeId = id };
            ViewBag.Res =(await _uow.RecipeService.GetByIdd(id));

            ViewBag.Reprocessed = (await _uow.RecipeService.GetByIdd(id)).IsReprocessed;

            return View(m);
        }
        [HttpPost]
        public async Task<IActionResult> AddLPS(long id, RecipeLPSViewModel vm)  {
            var m = _mapper.Map<RecipeLPS>(vm);

            var i =(await _uow.RecipeLPSService.GetByLps(vm.LPSId.Value));
            var x =(await _uow.PPCPlanningService.GetById(vm.LPSId.Value));
            if (i!=null) {

                throw new Exception("Exception message");
            }

            m.RecipeId = id;
            var o = await _uow.RecipeLPSService.Create(m);
            Recipe r = await _uow.RecipeService.GetByIdd(id);
            r.Weight = await _uow.RecipeLPSService.GetWeightByRecNo(id, r.IsReprocessed);
            r.Cones = await _uow.RecipeLPSService.GetConsByRecNo(id, r.IsReprocessed);
            r.PartyId = x.PartyId;
            r.BuyerColorId = x.BuyerColorId;

            r.LotNo = x.LotNo;
            await _uow.RecipeService.Update(r);
            return RedirectToAction(nameof(Details), new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteLPS(long id) {
            try {
                var o = await _uow.RecipeLPSService.GetById(id);

                if (o == null) return new NotFoundResult();

                await _uow.RecipeLPSService.Delete(o);
                Recipe r = await _uow.RecipeService.GetById(o.RecipeId);
                r.Weight = await _uow.RecipeLPSService.GetWeightByRecNo(r.Id, r.IsReprocessed);
                r.Cones = await _uow.RecipeLPSService.GetConsByRecNo(r.Id, r.IsReprocessed);
                await _uow.RecipeService.Update(r);

                if (o.IsDeleted == true)
                    return new OkResult();
                // return RedirectToAction(nameof(Details), new { id = o.RecipeId });
            }
            catch (Exception ex) {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                throw ex;
            }

            return new BadRequestResult();

        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id) {
            try {
                var x = await _uow.RecipeService.RecipeHeaderViewModelRepository(id);
                var o = await _uow.RecipeService.GetById(id);

                if (o == null)                    
                    return new NotFoundResult();

                await _uow.RecipeService.Delete(o);

                if (o.IsDeleted == true) return new OkResult();
            }
            catch (Exception ex) {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                throw ex;
            }

            return new BadRequestResult();
        }
        [HttpGet]
        public async Task<IActionResult> CopyPPCData(long id) {
            var filter = (await _uow.ReportFilterService.GetReportFiltersForUser(Convert.ToInt32(User.Identity.GetUserId()))).FirstOrDefault();

            var o = await _uow.RecipeLPSService.GetByLps(id);
            if (o != null)
            {
                ViewBag.Message = "This Lps is  used. Please chose some other Lps ";
                return View();
                
            }
            else
            {
                ViewBag.filter = filter;
                return PartialView(await _uow.PPCPlanningService.GetById(id));
                //return PartialView(_uow.PPCPlanningService.GetAll());

            }
        }
        [HttpGet]
        public async Task<IActionResult> CopyReprocessData(long id) {
            return PartialView((await _uow.PPCPlanningService.GetById(id)).Reprocesses.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> CalculateWeight(long id) {

            var detail = await _uow.RecipeService.GetById(id);

            await Task.WhenAll(detail.RecipeDetails.Select(async x =>
            {
                if (x.Gpl != 0)
                {
                    x.Weight = 0;
                    x.Weight = (detail.LiquorRate * detail.Weight * x.Gpl) / 1000;
                }
                else if (x.Percentage != 0)
                {
                    x.Weight = 0;
                    x.Weight = (detail.Weight * x.Percentage) / 100;
                }

                await _uow.RecipeDetailService.Update(x);
            }));

            return RedirectToAction(nameof(Details), "Recipe", new { id = id });
        }
        public async Task<IActionResult> Details(long id) {

            var x= await _uow.RecipeService.RecipeHeaderViewModelRepository(id);
            var y= await _uow.RecipeService.RecipeLPSViewModelRepository(id);
            var z= await _uow.RecipeService.RecipeDetailViewModelRepository(id);
        
            var m = await _uow.RecipeService.GetById(id);


            //var machineTypeList = _uow.MachineTypeService.GetAll().ToSelectList();
            //machineTypeList.Find(x => Convert.ToInt64(x.Value) == m.MachineTypeId).Selected = true;
            ViewBag.Header = x;
            ViewBag.LPS = y;
            ViewBag.Detail = z;

            
           return View(m);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateDetail([FromRoute]long? id, [FromQuery] long rId) {
            var recipeStepList = (await _uow.RecipeStepService.GetAll()).ToSelectList();
            var dyeList =(await  _uow.DyeService.GetAll()).ToSelectList();
            var chemicalList =(await _uow.ChemicalService.GetAll()).ToSelectList();
            var recp = await _uow.RecipeService.GetById(rId);
            ViewBag.recp = recp;


            var vm = new RecipeDetailViewModel();
        

            if (id.HasValue) {
                vm = _mapper.Map<RecipeDetailViewModel>(await _uow.RecipeDetailService.GetById(id.Value));
                ViewBag.filter = vm;

                if (vm != null) {
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

                }
            } else {
                vm.RecipeId = rId;
            }

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDetail([FromRoute]long? id, [FromForm] RecipeDetailViewModel vm) {
            // if (ModelState.IsValid)
            //{
            try {
                
                var m = _mapper.Map<RecipeDetail>(vm);
                var recipe = await _uow.RecipeService.GetById(vm.RecipeId.Value);
       
                if (m.ChemicalId == -1) m.ChemicalId = null;
                if (m.DyeId == -1) m.DyeId = null;
                if (m.RecipeStepId == -1) m.RecipeStepId = null;
                //if (recipe.RecipeDetails.Count() > 0) {
                    //if (recipe.RecipeDetails.Where(x => x.Id == m.Id).Count() == 0) {
                    //    if (m.ChemicalId == -1) m.ChemicalId = null;
                    //    if (m.DyeId == -1) m.DyeId = null;
                    //    if (m.RecipeStepId == -1) m.RecipeStepId = null;

                    //    _uow.RecipeDetailService.Create(m);
                    //} else {
                       await _uow.RecipeDetailService.Update(m);
              //      }


                //} else {

                //    //m.ReprocessId = null;
                //    _uow.RecipeDetailService.Create(m);
                //}
            }
            catch (Exception ex) {
                _tempData.Error = ex.Message;
            }
            //}
            return RedirectToAction(nameof(Details), "Recipe", new { id = vm.RecipeId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDetail(long id) {
            try {
                var o = await _uow.RecipeDetailService.GetById(id);

                if (o == null) return new NotFoundResult();

                await _uow.RecipeDetailService.Delete(o);

                if (o.IsDeleted == true) return new OkResult();
            }
            catch (Exception ex) {
                throw ex;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return new BadRequestResult();
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateReprocess() {

            ViewBag.recipeList =(await _uow.RecipeService.GetAll(true)).ToSelectList(nameof(Recipe.No));
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateReprocess(ReprocessRecipeViewModel vm)
        {
            Recipe r = null;
            if (vm != null)
            {
                var m = await _uow.RecipeService.RecipeHeaderViewModelRepository(vm.RecipeId);
                var y = await _uow.RecipeService.RecipeLPSViewModelRepository(vm.RecipeId);
                var z = await _uow.RecipeService.RecipeDetailViewModelRepository(vm.RecipeId);
                r = await _uow.RecipeService.GetById(vm.RecipeId);
                r.IsReprocessed = true;
                r.IsConfirmed = false;
                r.XRefRecipe = m.FirstOrDefault().No;
                r.No = await _uow.RecipeService.GetMaxRecNoByRec(r.No ,r.IsReprocessed) + 0.1m;
                //r.No = r.No + 0.1m;
                r.Id = 0;
                r.UserId = Convert.ToInt32(User.Identity.GetUserId());
                
                
                r.RecipeDetails.ToList().ForEach(a => a.Id = 0);
                
                try
                {
                   var res = await _uow.RecipeService.Create(r);

                    if (r.IsReprocessed == true)
                    {
                        if (y.Count > 0)
                        {
                            foreach (var lp in y)
                            {
                                RecipeLPS lps = new RecipeLPS();
                                lps.RecipeId = res.Id;
                                lps.LPSId = lp.LPSId;
                                await _uow.RecipeLPSService.Create(lps);
                            }
                        }
                    }

                    foreach (RecipeDetail d in r.RecipeDetails)
                    {
                        d.RecipeId = r.Id;
                        await _uow.RecipeDetailService.Create(d);
                    }
                   
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            ViewBag.recipeList =(await _uow.RecipeService.GetAll()).ToSelectList();
            return RedirectToAction(nameof(Details),
                new RouteValueDictionary(new { id = r.Id }));
        }
        [HttpPost]
        public async Task<IActionResult> CopyRecipe(ReprocessRecipeViewModel vm)
        {
            var x = await _uow.RecipeService.RecipeHeaderViewModelRepository(vm.RecipeId);
            var oldR = await _uow.RecipeService.GetById(vm.RecipeId);
            var oldDetails = oldR.RecipeDetails.ToList();

            var m = await _uow.RecipeService.RecipeHeaderViewModelRepository(vm.RecipeId);
            var y = await _uow.RecipeService.RecipeLPSViewModelRepository(vm.RecipeId);
            var z = await _uow.RecipeService.RecipeDetailViewModelRepository(vm.RecipeId);

            oldR.Id = 0;
            oldR.CreatedOn = null;
            oldR.No = await _uow.RecipeService.GetMaxRecNoByRec(oldR.No, false) + 1m;
            oldR.RecipeLPs.Clear();
            oldR.RecipeDetails.Clear();
            var dateAndTime = DateTime.Now;
           oldR.Date= dateAndTime.Date;
            oldR.IsConfirmed = false;

            var nR = await _uow.RecipeService.Create(oldR);

            await Task.WhenAll(oldDetails.Select(async d =>
            {
                d.Id = 0;
                d.RecipeId = nR.Id;

                await _uow.RecipeDetailService.Create(d);
            }));
            return RedirectToAction(nameof(Details), "Recipe", new { id = nR.Id });
        }
        [HttpGet]
        public async Task<IActionResult> CopyRecipe() {
            //var rec= _uow.RecipeService.Vw_RecipeNo_YarnDyingViewModel();
            //            ViewBag.recipeList =rec.
            //ViewBag.recipeList = _uow.RecipeService.GetAll(true).ToSelectList(nameof(Recipe.No));
            ViewBag.recipeList =(await _uow.RecipeService.GetRecipeNo()).ToSelectList(nameof(Recipe.No));
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> CopyRcipe(RecipeNoViewModel vm)
        {
            var oldR = await _uow.RecipeService.GetById(vm.Id);
            var oldDetails = oldR.RecipeDetails.ToList();

            oldR.Id = 0;
            oldR.CreatedOn = null;
            oldR.No = await _uow.RecipeService.GetMaxRecNoByRec(oldR.No, false) + 1m;
            oldR.RecipeLPs.Clear();
            oldR.RecipeDetails.Clear();

            var nR = await _uow.RecipeService.Create(oldR);

            await Task.WhenAll(oldDetails.Select(async d =>
            {
                d.Id = 0;
                d.RecipeId = nR.Id;

                await _uow.RecipeDetailService.Create(d);
            }));

            return RedirectToAction(nameof(Details), "Recipe", new { id = nR.Id });
        }
        public async Task<IActionResult> DyeingRecipe_D2Report(long id)
        {

            List<DyeingRecipe_D2ViewModel> res = new List<DyeingRecipe_D2ViewModel>();
            var d =(await _uow.RecipeService.DyeingRecipieService_D2(id)).ToList();

            //d.ToList().GroupBy(y => y.RecipeName).ToList().ForEach(
            //  x =>
            //  {
            DyeingRecipe_D2ViewModel r = new DyeingRecipe_D2ViewModel();
            if (d.Count()>0)
            {
                r.No = d.FirstOrDefault().No; ;
                r.LPS = d.FirstOrDefault().LPS;
                r.MachineType = d.FirstOrDefault().MachineType;
                r.Format = d.FirstOrDefault().Format;
                r.Weight = d.FirstOrDefault().Weight;
                r.Color = d.FirstOrDefault().Color;
                r.LiquorRate = d.FirstOrDefault().LiquorRate;
                r.Date = d.FirstOrDefault().Date;
                r.RecipeName = d.FirstOrDefault().RecipeName;
                r.IsReprocessed = d.FirstOrDefault().IsReprocessed;
                r.Party = d.FirstOrDefault().Party;
                r.IsYarn = d.FirstOrDefault().IsYarn;
                r.IsWithoutLPS = d.FirstOrDefault().IsWithoutLPS;
                r.LotNo= d.FirstOrDefault().LotNo;
                r.Buyer= d.FirstOrDefault().Buyer;
                r.FabricType= d.FirstOrDefault().FabricType;
                r.YarnType = d.FirstOrDefault().YarnType;
                r.ShiftName = d.FirstOrDefault().ShiftName;
                r.ShiftInchargeName = d.FirstOrDefault().ShiftInchargeName;
                r.ShiftInchargeCode = d.FirstOrDefault().ShiftInchargeCode;
                r.MachineOperatorName = d.FirstOrDefault().MachineOperatorName;
                r.MachineOperatorCode = d.FirstOrDefault().MachineOperatorCode;
                r.HelperName = d.FirstOrDefault().HelperName;
                r.HelperCode = d.FirstOrDefault().HelperCode;
                r.CStoreOperatorName = d.FirstOrDefault().CStoreOperatorName;
                r.CStoreOperatorCode = d.FirstOrDefault().CStoreOperatorCode;

                d.ForEach(b =>
                {
                    DyeingRecipeDetail_D2ViewModel e = new DyeingRecipeDetail_D2ViewModel();
                    e.RecipeName = b.RecipeName;
                    e.ItemName = b.ItemName;
                    e.Kgs = b.Kgs;
                    e.Gpl = b.Gpl;
                    e.Percentage = b.Percentage;
                    e.RecipeStepId = b.RecipeStepId;
                    e.Water = b.Water;
                    e.Status = b.Status;
                    

                    r.DyeingRecipeDetail_D2ViewModels.Add(e);
                });
                res.Add(r);
            }
            
            counts += 1;
            ViewBag.Counts = counts;
            //});
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MMM/dd/yyyy") + "  Printed by: Tayyab" + "  Page: [page]/[toPage]\"" +
             " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 2 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;
        }
        public async Task<IActionResult> WithoutLPS_D5Report(long id)
        {

            List<DyeingRecipe_D2ViewModel> res = new List<DyeingRecipe_D2ViewModel>();
            var d =(await _uow.RecipeService.WithoutlpsRecipie_D5(id)).ToList();

           
            DyeingRecipe_D2ViewModel r = new DyeingRecipe_D2ViewModel();
            if (d.Count() > 0)
            {
                r.No = d.FirstOrDefault().No; ;
             
                r.MachineType = d.FirstOrDefault().MachineType;
                r.Format = d.FirstOrDefault().Format;
                r.Weight = d.FirstOrDefault().Weight;
               
                r.LiquorRate = d.FirstOrDefault().LiquorRate;
                r.Date = d.FirstOrDefault().Date;
                r.IsYarn = d.FirstOrDefault().IsYarn;
                r.IsWithoutLPS = d.FirstOrDefault().IsWithoutLPS;
                //r.LotNo = d.FirstOrDefault().;





                d.ToList().ForEach(b =>
                {
                    DyeingRecipeDetail_D2ViewModel e = new DyeingRecipeDetail_D2ViewModel();
                    e.RecipeName = b.RecipeName;
                    e.ItemName = b.ItemName;
                    e.Kgs = b.Kgs;
                    e.Gpl = b.Gpl;
                    e.Percentage = b.Percentage;
                    e.RecipeStepId = b.RecipeStepId;
                    e.Water = b.Water;





                    r.DyeingRecipeDetail_D2ViewModels.Add(e);
                });
                res.Add(r);
            }

            //});
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
             " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 2 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;
        }
//public IActionResult CostingRecipe_D5(long id)
        //{

        //    List<DyeingRecipe_D2ViewModel> res = new List<DyeingRecipe_D2ViewModel>();
        //    var d = _uow.RecipeService.DyeingRecipieService_D2(id).ToList();

        //    //d.ToList().GroupBy(y => y.RecipeName).ToList().ForEach(
        //    //  x =>
        //    //  {
        //          DyeingRecipe_D2ViewModel r = new DyeingRecipe_D2ViewModel();
        //          r.No = d.FirstOrDefault().No; ;
        //          r.LPS = d.FirstOrDefault().LPS;
        //          r.MachineType = d.FirstOrDefault().MachineType;
        //          r.Format = d.FirstOrDefault().Format;
        //          r.Weight = d.FirstOrDefault().Weight;
        //          r.Color = 000000000000000000000000000000000000000000000000d.FirstOrDefault().Color;
        //          r.LiquorRate= d.FirstOrDefault().LiquorRate;
        //          r.Date= d.FirstOrDefault().Date;
        //          r.RecipeName = d.FirstOrDefault().RecipeName;


        //    d.ToList().ForEach(b =>
        //          {
        //              DyeingRecipeDetail_D2ViewModel e = new DyeingRecipeDetail_D2ViewModel();
        //              e.RecipeName = b.RecipeName;
        //              e.ItemName = b.ItemName;
        //              e.Kgs = b.Kgs;
        //              e.Gpl = b.Gpl;
        //              e.Percentage = b.Percentage;
        //              e.RecipeStepId = b.RecipeStepId;
        //              e.Water = b.Water;

        //          if (e.CostPerKgChemical != null){

        //                  e.CostPerKgChemical = (b.Kgs / b.Weight) * b.Rate;
        //              }


        //              if (e.CostPerKgDye != null)
        //              {

        //                  e.CostPerKgDye = (b.Kgs / b.Weight) * b.Rate;
        //              }


        //              r.DyeingRecipeDetail_D2ViewModels.Add(e);
        //          });
        //          res.Add(r);
        //      //});
        //    string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
        //     " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
        //    ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
        //    return new ViewAsPdf(res)
        //    {
        //        PageSize = Rotativa.AspNetCore.Options.Size.A4,
        //        PageMargins = { Left = 4 },
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
        //        CustomSwitches = footer
        //    }
        //        ;

        //}
        public async Task<IActionResult> DyeingCostingReport_D5(long id)
        {

            List<DyeingRecipe_D2ViewModel> res = new List<DyeingRecipe_D2ViewModel>();
            var d =(await _uow.RecipeService.DyeingRecipieService_D2(id)).ToList();

            //d.ToList().GroupBy(y => y.RecipeName).ToList().ForEach(
            //  x =>
            //  {
            DyeingRecipe_D2ViewModel r = new DyeingRecipe_D2ViewModel();
            r.No = d.FirstOrDefault().No; ;
            r.LPS = d.FirstOrDefault().LPS;
            r.MachineType = d.FirstOrDefault().MachineType;
            r.Format = d.FirstOrDefault().Format;
            r.Weight = d.FirstOrDefault().Weight;
            r.Color = d.FirstOrDefault().Color;
            r.LiquorRate = d.FirstOrDefault().LiquorRate;
            r.Date = d.FirstOrDefault().Date;
            r.RecipeName = d.FirstOrDefault().RecipeName;
            r.Party = d.FirstOrDefault().Party;


            r.Total = (decimal)d.Sum(y => y.Cost);
            
          
          



            d.ToList().ForEach(b =>
            {
                DyeingRecipeDetail_D2ViewModel e = new DyeingRecipeDetail_D2ViewModel();
                e.RecipeName = b.RecipeName;
                e.ItemName = b.ItemName;
                e.Kgs = b.Kgs;
                e.Gpl = b.Gpl;
                e.Percentage = b.Percentage;
                e.RecipeStepId = b.RecipeStepId;
                e.Water = b.Water;

                if (e.Kgs != null)
                {

                    e.CostPerKgDye = (b.Kgs / b.Weight) * b.Rate;
               

                    

                }
               
                    //e.Cost = b.Cost.Value;

                



                r.DyeingRecipeDetail_D2ViewModels.Add(e);
            });
            res.Add(r);
            //});
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
             " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;
        }
        public async Task<IActionResult> DyeingReprocessRecipeReport_D6 (long id)
        {

            List<DyeingRecipe_D2ViewModel> res = new List<DyeingRecipe_D2ViewModel>();
            var d =(await _uow.RecipeService.DyeingRecipieService_D2(id)).ToList();

            //d.ToList().GroupBy(y => y.RecipeName).ToList().ForEach(
            //  x =>
            //  {
            DyeingRecipe_D2ViewModel r = new DyeingRecipe_D2ViewModel();
            r.No = d.FirstOrDefault().No; ;
            r.LPS = d.FirstOrDefault().LPS;
            r.MachineType = d.FirstOrDefault().MachineType;
            r.Format = d.FirstOrDefault().Format;
            r.Weight = d.FirstOrDefault().Weight;
            r.Color = d.FirstOrDefault().Color;
            r.LiquorRate = d.FirstOrDefault().LiquorRate;
            r.Date = d.FirstOrDefault().Date;
            r.RecipeName = d.FirstOrDefault().RecipeName;


            r.Total = (decimal)d.Sum(y => y.Cost);






            d.ToList().ForEach(b =>
            {
                DyeingRecipeDetail_D2ViewModel e = new DyeingRecipeDetail_D2ViewModel();
                e.RecipeName = b.RecipeName;
                e.ItemName = b.ItemName;
                e.Kgs = b.Kgs;
                e.Gpl = b.Gpl;
                e.Percentage = b.Percentage;
                e.RecipeStepId = b.RecipeStepId;
                e.Water = b.Water;

                if (e.Kgs != null)
                {

                    e.CostPerKgDye = (b.Kgs / b.Weight) * b.Rate;




                }

                //e.Cost = b.Cost.Value;











                r.DyeingRecipeDetail_D2ViewModels.Add(e);
            });
            res.Add(r);
            //});
            string footer = "--footer-center \"Printed on: " + DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
             " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\"";
            ViewBag.Message = string.Format("Hello {0} to ASP.NET MVC!", "Giorgio III.");
            return new ViewAsPdf(res)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 4 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = footer
            }
                ;
        }
    }
}