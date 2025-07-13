using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.Core.ReportsViewModel.YD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD
{
    internal class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private AppDbContext _db;
        public RecipeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Recipe> GetSingle()
        {
            return await Task.FromResult( _db.Recipes
                //.Include(x => x.RecipeFormat)
                //.Include(x => x.MachineType)
                //.Include(x => x.RecipeLPs)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.Reprocess).ThenInclude(x => x.PPCPlanning).ThenInclude(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.Recipe)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.YarnType)
                .Include(x => x.RecipeDetails)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.FabricType)
                //.Include(x => x.RecipeDetails)
                //.Include(x => x.RecipeDetails)
                .FirstOrDefault());

        }

        public override async Task<Recipe> GetSingle(Func<Recipe, bool> where, params Expression<Func<Recipe, object>>[] navigationProperties)
        {
            return await Task.FromResult(_db.Recipes

                //.Include(x => x.RecipeFormat)
                //.Include(x => x.MachineType)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.Recipe)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.Reprocess).ThenInclude(x => x.PPCPlanning).ThenInclude(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                ////.Include(x => x.RecipeDetails).ThenInclude(x => x.Recipe)
                .Include(x => x.RecipeDetails)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.FabricType)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.YarnType)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.Chemical)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.RecipeStep)
                .FirstOrDefault(where));

        }

        public async Task<IList<Recipe>> GetList()
        {
            return await Task.FromResult( _db.Recipes
              
                //.Include(x => x.RecipeFormat)
                //.Include(x => x.MachineType)
                .Include(x => x.RecipeLPs)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.YarnType)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.Reprocess).ThenInclude(x => x.PPCPlanning).ThenInclude(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.Recipe)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.FabricType)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.Dye)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.Chemical)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.RecipeStep)
                .ToList());
        }


        public async Task<IList<Recipe>> GetData()
        {
            return await Task.FromResult( _db.Recipes.Where(x => x.IsDeleted == false).ToList());

        }

        public override async Task<IList<Recipe>> GetList(Func<Recipe, bool> where, params Expression<Func<Recipe, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.Recipes
                //.Include(x => x.RecipeFormat)
                //.Include(x => x.MachineType)
                .Include(x => x.RecipeLPs)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.Reprocess).ThenInclude(x => x.PPCPlanning).ThenInclude(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.Recipe)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.Dye)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.YarnType)
                //.Include(x => x.RecipeLPs).ThenInclude(x => x.LPS).ThenInclude(x => x.FabricType)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.Chemical)
                //.Include(x => x.RecipeDetails).ThenInclude(x => x.RecipeStep)
                .Where(where).ToList());
        }

        public async Task<List<DyeingConsupmtionSummaryRepository_D1ViewModel>> DyeingConsupmtionSummaryRepository_D1(long userid)
        {
            return await Task.FromResult( _db.DyeingConsupmtionSummaryRepository_D1ViewModels.FromSql($"dbo.usp_DyeingConsupmtionSummary_D1 {userid}").ToList());
        }
        
        public async Task<List<DyeingRecipieRepository_D2ViewModel>> DyeingRecipieRepository_D2(long recipeid)
        {
            return await Task.FromResult( _db.DyeingRecipieRepository_D2ViewModels.FromSql($"dbo.usp_DyeingRecipie_D2 {recipeid}").ToList());

        }
        public async Task<List<Vw_RecipeLpsDetail>> Vw_RecipeLpsDetail(long recipeid)
        {
            return await Task.FromResult(_db.Vw_RecipeLpsDetail.Where(x => x.RecipeId == recipeid).ToList());
        }

        public async Task<List<WithoutlpsRecipieRepository_D5ViewModel>> WithoutlpsRecipieRepository_D5ViewModel(long recipeid)
        {
            return await Task.FromResult( _db.WithoutlpsRecipieRepository_D5ViewModel.FromSql($"dbo.usp_WithoutlpsRecipie_D5 {recipeid}").ToList());
        }


        public async Task<List<RecipeIssuedRepository_D3ViewModel>> RecipeIssuedRepository_D3(long userid)
        {
            return await Task.FromResult( _db.RecipeIssuedRepository_D3ViewModel.FromSql($"usp_RecipeIssued_D3 { userid}").ToList()); 
        } 
        
        public async Task<List<Dy>> DyeingDetailConsumption_D5(long userid)
        {
            return await Task.FromResult( _db.DyeingDetailConsumptionRepository_ViewModel.FromSql($"usp_DyeingConsupmtionDetail_D4 { userid}").ToList());
        }

        public async Task<List<long>> GetAllLPSNoUsedRepository()
        {
            return await Task.FromResult( _db.RecipeLPs.Where(y => y.LPSId != null).Select(x => x.LPSId.Value).ToList());               
        }

        public async Task<IList<Vw_RecipeNo_YarnDyingViewModel>> Vw_RecipeNo_YarnDyingViewModel()
        {

            return await Task.FromResult( _db.Vw_RecipeNo_YarnDying.ToList());

        }

        public async Task<List<RecipeHeaderViewModelRepository>> RecipeHeaderViewModelRepository(long recipeid)
        {
            return await Task.FromResult( _db.RecipeHeaderViewModelRepository.FromSql($"usp_RecipeHeader {recipeid}").ToList());
        }

        
        public async Task<List<RecipeLPSViewModelRepository>> RecipeLPSViewModelRepository(long recipeid)
        {
            return await Task.FromResult( _db.RecipeLPSViewModelRepository.FromSql($"usp_RecipeLPS {recipeid}").ToList());
        }  
        public async Task<List<RecipeDetailViewModelRepository>> RecipeDetailViewModelRepository(long recipeid)
        {
           return await Task.FromResult( _db.RecipeDetailViewModelRepository.FromSql($"usp_RecipeDetail {recipeid}").ToList());
        }    
        public async Task<List<RecipeIndexViewModelRepository>> RecipeIndexViewModelRepository(DateTime dateto, DateTime datefrom)
        {
            return await Task.FromResult( _db.RecipeIndexViewModelRepository.FromSql($"usp_RecipeIndex {dateto}, {datefrom}").ToList());
        }

        public async Task<List<DyeingRecipeReprocess_ViewModel>> GetDyeingRecipeReprocesses(long? userid, bool isyarn)
        {
            return await Task.FromResult( _db.DyeingRecipeReprocess_ViewModel.FromSql($"usp_DyeingRecipeReprocess {userid}, {isyarn}").ToList());
        }

        public async Task<List<RecipeIndexViewModelRepository>> PendingDyeingTimeAddition(long? userid)
        {
            return await Task.FromResult( _db.RecipeIndexViewModelRepository.FromSql($"usp_PendingDyeingTimeAddition {userid}").ToList());
        }

        public async Task<Recipe> GetSingleById(long id)
        {
            var res = await Task.FromResult(_db.Recipes
                .Include(x => x.RecipeDetails)
                .Include(x => x.RecipeLPs)
                .Where(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefault());
            return res;
        }

        public async Task<List<FabricDyeingWeighDetail_ViewModel>> FabricDyeingWeighDetail(long? userid)
        {
            return await Task.FromResult(_db.FabricDyeingWeighDetail_ViewModel.FromSql($"usp_FabricDyeingWeighDetail {userid}").ToList());
        }
        public async Task<List<TotalRecipes_ViewModel>> TotalRecipes_ViewModel(long? userid)
        {
            return await Task.FromResult(_db.TotalRecipes_ViewModel.FromSql($"usp_TotalRecipes {userid}").ToList());
        }

        public async Task<List<ChemicalIssuanceDetail_ViewModel>> ChemicalIssuanceDetail(long? userid)
        {
            return await Task.FromResult(_db.ChemicalIssuanceDetail_ViewModel.FromSql($"usp_ChemicalIssuanceDetail {userid}").ToList());
        }
    }
}
