using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.Core.ReportsViewModel.YD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Interfaces.IYD
{
    public interface IRecipeService : IDefaultService<Recipe>
    {

        Task<decimal> GetMaxRecNoByRec(decimal? recno, bool isreprocessed);
        Task<List<Recipe>> GetAll(bool? isreprocess);
        Task<Recipe> GetByIdLazy(long id);
        Task<Recipe> GetByRecNo(Decimal recno);
        Task<List<DyeingConsupmtionSummaryRepository_D1ViewModel>> DyeingConsupmtionSummaryRepository_D1(long userid);
        Task<List<Recipe>> GetAllForChemicalService();
        Task<List<DyeingRecipieRepository_D2ViewModel>> DyeingRecipieService_D2(long recipeid);
        Task<List<RecipeIssuedRepository_D3ViewModel>> RecipeIssuedRepository_D3(long userid);
        Task<List<Dy>> DyeingDetailConsumption_D5(long userid);
        Task<List<long>> GetAllLPSNoUsedService();
        Task<List<WithoutlpsRecipieRepository_D5ViewModel>> WithoutlpsRecipie_D5(long userid);
        Task<IList<Vw_RecipeNo_YarnDyingViewModel>> Vw_RecipeNo_YarnDyingViewModel();
        Task<List<Recipe>> GetRecipeNo();
        Task<Recipe> GetByIdd(long id);
        Task<List<RecipeHeaderViewModelRepository>> RecipeHeaderViewModelRepository(long recipeid);
        Task<List<RecipeLPSViewModelRepository>> RecipeLPSViewModelRepository(long recipeid);
        Task<List<RecipeDetailViewModelRepository>> RecipeDetailViewModelRepository(long recipeid);
        Task<List<RecipeIndexViewModelRepository>> RecipeIndexViewModelRepository(DateTime start, DateTime end);
        Task<List<DyeingRecipeReprocess_ViewModel>> GetDyeingRecipeReprocesses(long? userid, bool isyarn);
        Task<List<RecipeIndexViewModelRepository>> PendingDyeingTimeAddition(long? userid);
        Task<List<Vw_RecipeLpsDetail>> Vw_RecipeLpsDetail(long recipeid);
        Task<List<FabricDyeingWeighDetail_ViewModel>> FabricDyeingWeighDetail(long? userid);
        Task<List<TotalRecipes_ViewModel>> TotalRecipes_ViewModel(long? userid);
        Task<List<ChemicalIssuanceDetail_ViewModel>> ChemicalIssuanceDetail(long? userid);
    }
}
