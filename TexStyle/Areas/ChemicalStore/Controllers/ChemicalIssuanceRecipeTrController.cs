using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.Common;
using TexStyle.Core.CS;
using TexStyle.Core.YD;
using TexStyle.Extensions;
using TexStyle.Infrastructure;
using TexStyle.ViewModels;
using TexStyle.ViewModels.CS;
using TexStyle.ViewModels.YD;
using RecipeNoViewModel = TexStyle.ViewModels.CS.RecipeNoViewModel;

namespace TexStyle.Areas.ChemicalStore.Controllers
{
    [Area(AreaConstants.CHEMICAL_STORE.Name)]
    public class ChemicalIssuanceRecipeTrController : Controller
    {
        public IMapper _mapper { get; set; }
        public IUnitOfWork _uow { get; set; }

        public ChemicalIssuanceRecipeTrController( IMapper mapper , IUnitOfWork uow )
        {
            _mapper = mapper;
            _uow = uow;
        }

        //public IActionResult Index()
        //{

        //   // return View(_uow.ChemicalIssuanceRecipeTrService.GetAll());
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
            ViewBag.FilterOpts = new FilterOptionsViewModel { sd = options.sd.Value.ToString("yyyy-MM-dd"), ed = options.ed.Value.ToString("yyyy-MM-dd") };
            return View(await _uow.DyeChemicalTrService.GetBetweenDateRangeRecipe(options.sd.Value, options.ed.Value , ChemicalTransactions.ChemicalIssuanceRecipe, ChemicalTransactions.CKL6RecipeIssuance));
        }
        public async Task<IActionResult> AddOrUpdate()
        {
            RecipeNoViewModel vm = null;
            List<Recipe> allRecipelist= (await _uow.RecipeService.GetAllForChemicalService()).ToList();
            List<long>  usedRecipelist = await _uow.DyeChemicalTrService.GetAllRecipeNoUsedService();
            List<Recipe> recipeslist = new List<Recipe>();

            foreach (var x in allRecipelist)
            {
                if (!usedRecipelist.Contains(Convert.ToInt64(x.Id)))
                {
                    Recipe recipe = new Recipe
                    {
                        Id = x.Id,
                        No = x.No
                    };
                    recipeslist.Add(recipe);
                }
            }

            if (recipeslist!= null)
            {

                ViewBag.recipeList = recipeslist.ToList().ToSelectList(nameof(Recipe.No));
            }

            return PartialView(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(RecipeNoViewModel vm )
        {
            if (ModelState.IsValid)
            {

            DyeChemicalTr v = new DyeChemicalTr();   
           List<DyeChemicalTrDetail> dv = new List<DyeChemicalTrDetail>();   
            Recipe rm= await _uow.RecipeService.GetById(Convert.ToInt64(vm.RecipeId));
            v.RecipeId = rm.Id;
            v.TransactionDate = DateTime.Now;
                v.TransactionDate = v.TransactionDate.Date;
                v.IsCompleteIssued = false;
                if (rm.IsYarn == true)
                {
                    v.TrType = 11;

                }

                if (rm.IsYarn == false)
                {
                    v.TrType = 17;

                }
                if (rm.IsYarn==null)
                {
                    v.TrType = 11;

                }
                //

                v.IsDeleted = false;
            v.RecipieIssuanceDate =  rm.Date;




                DyeChemicalTr nv = await _uow.DyeChemicalTrService.Create(v);

                  
                rm.RecipeDetails.Where(m=>m.RecipeStepId== null && m.Weight!= 0).ToList().ForEach(X =>
                  {
                      dv.Add(new DyeChemicalTrDetail
                      {
                          DyeId = X.DyeId,
                          ChemicalId = X.ChemicalId,
                          QtyCr = Convert.ToDecimal(X.Weight),
                          RecipeDetailId = X.Id,
                          DyeChemicalTrId = nv.Id,
                          IsDr = false,
                          IsDeleted=false,
                          IsIssued=false
                          //PendingDate = X.pendingdate

                      });
                       
                      
                      //linker = new 
                     // ();
                      //linker.TrStatus = LinkerMasterTrStatus.Credit;
                      //linker.DyeId = X.DyeId;
                      //linker.ChemicalId = X.ChemicalId;
                      //linker.ChemicalIssuanceRecipeTrDetailId = X.Id;
                      //linker.ChemicalIssuanceRecipeTrId = nv.Id;
                      //linker.Date = nv.IssuanceDate;
                      //_uow.TrLinkerMasterService.Create(linker);
                  });

                await Task.WhenAll(dv.Select(d => _uow.DyeChemicalTrDetailService.Create(d)));

                return RedirectToAction("Details", "ChemicalIssuanceRecipeTr", new { Id = nv.Id });
            }

            //ViewBag.recipeList = _uow.RecipeService.GetAll(true).ToSelectList(nameof(Recipe.No));
            return View(nameof(Index));  
        }

        [HttpGet]
        public async Task<IActionResult> Details( long id )
        {
            var m = await _uow.DyeChemicalTrService.GetById(id);

            // if (m == null) return RedirectToAction(nameof(Index));
            var recipeList = (await _uow.RecipeService.GetAll(true)).ToSelectList(nameof(Recipe.No));
            //var dyeList = _uow.DyeService.GetAll().ToSelectList();
            //var chemicalList = _uow.ChemicalService.GetAll().ToSelectList();

            if (m != null)
            {
                recipeList.Find(x => Convert.ToDecimal(x.Value) == m.RecipeId).Selected = true;
            }
            ViewBag.recipeList = recipeList;
            //ViewBag.supplierList = supplierList;
            //ViewBag.dyeList = dyeList;
            //ViewBag.chemicalList = chemicalList;


            return View(m);
            //return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddOrUpdateDetail(long id)
        {
           
            var m = await _uow.DyeChemicalTrDetailService.GetById(id);
            m.IsIssued = true;
            /// store procedure / pass status id and item id
            /// getqty value
            string message = "Balance less than a Credit";
            if (m.ChemicalId== null && m.DyeId != null)
            {
                var data = await _uow.DyeChemicalTrService.DyeTotal_Balance(m.DyeId.Value);
                if (m.QtyCr <= data.Balance)
                {
                    //var m = _uow.DyeChemicalTrDetailService.GetById(id);
                    //m.IsIssued = true;
                    await _uow.DyeChemicalTrDetailService.Update(m);
                }
                else
                {
                    ViewBag.message = "Balance less than a Credit";
                    return Json(message);
                }   
               
            }
            else if (m.ChemicalId != null && m.DyeId == null)
            {
                var data1 = await _uow.DyeChemicalTrService.DyeChemicalTotal_Balance(m.ChemicalId.Value);
                //var data1 = _uow.DyeChemicalTrService.ChemicalDyeIdData(m.DyeId.Value, m.Status.Value);
                if (m.QtyCr <= data1.Balance)
                {
                    //var m = _uow.DyeChemicalTrDetailService.GetById(id);
                    //m.IsIssued = true;
                    await _uow.DyeChemicalTrDetailService.Update(m);
                    //  return RedirectToAction("Details", "ChemicalIssuanceRecipeTr", new { Id = m.DyeChemicalTrId });
                }
                else
                {
                    ViewBag.message = "Balance less than a Credit";
                    return Json(message);
                }
                //ViewBag.message = "Everything is good";
            }
            return RedirectToAction("Details", "ChemicalIssuanceRecipeTr", new { Id = m.DyeChemicalTrId });
        }
            

    }
}