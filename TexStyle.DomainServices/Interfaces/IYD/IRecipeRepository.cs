using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.Core.ReportsViewModel.YD;
using TexStyle.Core.YD;

namespace TexStyle.DomainServices.Interfaces.IYD
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<List<DyeingConsupmtionSummaryRepository_D1ViewModel>> DyeingConsupmtionSummaryRepository_D1(long userid);
        Task<List<DyeingRecipieRepository_D2ViewModel>> DyeingRecipieRepository_D2(long recipeid);
        Task<List<RecipeIssuedRepository_D3ViewModel>> RecipeIssuedRepository_D3(long userid);
        Task<List<Dy>> DyeingDetailConsumption_D5(long userid);
        Task<List<long>> GetAllLPSNoUsedRepository();
        Task<IList<Vw_RecipeNo_YarnDyingViewModel>> Vw_RecipeNo_YarnDyingViewModel();
        Task<List<WithoutlpsRecipieRepository_D5ViewModel>> WithoutlpsRecipieRepository_D5ViewModel(long recipeid);
        Task<IList<Recipe>> GetData();
        Task<List<RecipeHeaderViewModelRepository>> RecipeHeaderViewModelRepository(long recipeid);
        Task<List<RecipeLPSViewModelRepository>> RecipeLPSViewModelRepository(long recipeid);
        Task<List<RecipeDetailViewModelRepository>> RecipeDetailViewModelRepository(long recipeid);
        Task<List<RecipeIndexViewModelRepository>> RecipeIndexViewModelRepository(DateTime dateto, DateTime datefrom);
        Task<List<DyeingRecipeReprocess_ViewModel>> GetDyeingRecipeReprocesses(long? userid, bool isyarn);
        Task<List<RecipeIndexViewModelRepository>> PendingDyeingTimeAddition(long? userid);
        Task<Recipe> GetSingleById(long id);
        Task<List<Vw_RecipeLpsDetail>> Vw_RecipeLpsDetail(long recipeid);
        Task<List<FabricDyeingWeighDetail_ViewModel>> FabricDyeingWeighDetail(long? userid);
        Task<List<TotalRecipes_ViewModel>> TotalRecipes_ViewModel(long? userid);
        Task<List<ChemicalIssuanceDetail_ViewModel>> ChemicalIssuanceDetail(long? userid);

    }
}
