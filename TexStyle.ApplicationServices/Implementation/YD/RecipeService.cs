using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.Core.ReportsViewModel.YD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Implementation.YD {
    internal class RecipeService : IRecipeService {
        private IRecipeRepository _repo;
        public RecipeService(IRecipeRepository repo) {
            _repo = repo;
        }

        public async Task<Recipe> Create(Recipe o) {
            try {
                o.CreatedOn = DateTime.Now;
                if (o.IsYarn == true)
                {
                
                    o.BranchId = 1;
                }
          if (o.IsYarn==false)
                {
      
                    o.BranchId = 2;

                }

               
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Recipe> Delete(Recipe o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Recipe>> GetAll() {
            try {
                var res= await _repo.GetList(x => x.IsDeleted == false,
                                 n => n.RecipeLPs.Where(t=> t.IsDeleted==false),
                                 n => n.MachineType,
                                 n => n.RecipeFormat,
                                 n => n.RecipeDetails
                                 );
                return res.ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }


        public async Task<List<Recipe>> GetRecipeNo()
        {
            try
            {
                var res = await _repo.GetData();
                return res.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public async Task<List<Recipe>> GetAll(bool? isreprocess) {
            try {

               var list = await _repo.GetList(x => x.IsDeleted == false ,
               n => n.RecipeLPs,
               n => n.MachineType,
               n => n.RecipeFormat,
               n => n.RecipeDetails
               );

                return list.ToList();
            }
            catch (Exception ex) { 
                throw ex;
            }
        }

        public async Task<List<Recipe>> GetAllForChemicalService()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false  && x.IsConfirmed == true);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<long>> GetAllLPSNoUsedService()
        {
            try
            {
                return await _repo.GetAllLPSNoUsedRepository();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetMaxRecNoByRec(decimal? recno, bool isreprocessed)
        {
            try
            {
                var a1 = await _repo.GetList(x => x.IsDeleted == false, x => x.XRefRecipe == recno);
                var a = a1.Where(x => x.IsReprocessed == false).Max(x => x.No);
                
                if (isreprocessed == true)
                {
                    a1 = await _repo.GetList(x => x.IsDeleted == false, x => x.XRefRecipe == recno);
                    a = a1.Where(x => x.No < recno + 1).Max(x => x.No);
                }
                
                return a;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<Recipe> GetByRecNo(Decimal recno) {
            try {

                var a = await _repo.GetSingle(x => x.IsDeleted == false, x => x.No == recno);
                return a;

            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<Recipe> GetById(long id) {
            try {
                //var recipe = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
                //   //n => n.RecipeLPs,
                //   //n => n.MachineType,
                //   //n => n.RecipeFormat,
                //   n => n.RecipeDetails);

                var recipe = await _repo.GetSingleById(id);

                return recipe;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Recipe> GetByIdd(long id)
        {
            try
            {
                var res = await _repo.GetSingleitem(x => x.Id == id && x.IsDeleted == false,
                                        n => n.RecipeDetails, n => n.RecipeLPs



                   );
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Recipe> GetByIdLazy(long id)
        {
            try
            {
                var res = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false
             
                   );
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Recipe> Update(Recipe o) {
            try {
                o.UpdatedOn = DateTime.Now;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Recipe>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.Date.Date >= start.Date && x.Date.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                
                throw ex;
            }
        }

        public async Task<List<DyeingConsupmtionSummaryRepository_D1ViewModel>> DyeingConsupmtionSummaryRepository_D1(long userid)
        {
            try
            {
                var list = await this._repo.DyeingConsupmtionSummaryRepository_D1(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public async Task<List<RecipeIssuedRepository_D3ViewModel>> RecipeIssuedRepository_D3(long userid)
        {
            try
            {
                var list = await this._repo.RecipeIssuedRepository_D3(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }    
        public async Task<List<Dy>> DyeingDetailConsumption_D5 (long userid)
        {
            try
            {var list = await this._repo.DyeingDetailConsumption_D5(userid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
        public  async Task<List<WithoutlpsRecipieRepository_D5ViewModel>> WithoutlpsRecipie_D5(long userid)
        {
            try
            {
                var list = await this._repo.WithoutlpsRecipieRepository_D5ViewModel(userid);
                return list.ToList();
            }
            catch (Exception ex) 
            {
                throw ex; 
            }
        }  
        public async Task<List<DyeingRecipieRepository_D2ViewModel>> DyeingRecipieService_D2(long recipeid)
        {
            try
            {
                var list = await this._repo.DyeingRecipieRepository_D2(recipeid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }   
        
        
        public  async Task<List<RecipeHeaderViewModelRepository>> RecipeHeaderViewModelRepository(long recipeid)
        {
            try
            {
                var list = await this._repo.RecipeHeaderViewModelRepository(recipeid);
                return list.ToList();
            }
            catch (Exception ex) 
            {
                throw ex; 
            }
        }


        public async Task<List<RecipeIndexViewModelRepository>> RecipeIndexViewModelRepository(DateTime start, DateTime end)
        {
            try
            {
                var list = await this._repo.RecipeIndexViewModelRepository(start, end);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<RecipeLPSViewModelRepository>> RecipeLPSViewModelRepository(long recipeid)
        {
            try
            {
                var list = await this._repo.RecipeLPSViewModelRepository(recipeid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<RecipeDetailViewModelRepository>> RecipeDetailViewModelRepository(long recipeid)
        {
            try
            {
                var list = await this._repo.RecipeDetailViewModelRepository(recipeid);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<Vw_RecipeNo_YarnDyingViewModel>> Vw_RecipeNo_YarnDyingViewModel()
        {
            try
            {
                var res = await _repo.Vw_RecipeNo_YarnDyingViewModel();
                return res.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<DyeingRecipeReprocess_ViewModel>> GetDyeingRecipeReprocesses(long? userid, bool isyarn)
        {
            try
            {
                var res = await _repo.GetDyeingRecipeReprocesses(userid, isyarn);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<List<RecipeIndexViewModelRepository>> PendingDyeingTimeAddition(long? userid)
        {
            try
            {
                var res = await _repo.PendingDyeingTimeAddition(userid);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Vw_RecipeLpsDetail>> Vw_RecipeLpsDetail(long recipeid)
        {
            var res = await _repo.Vw_RecipeLpsDetail(recipeid);
            return res.ToList();
        }

        public async Task<List<FabricDyeingWeighDetail_ViewModel>> FabricDyeingWeighDetail(long? userid)
        {
            var res = await _repo.FabricDyeingWeighDetail(userid);
            return res.ToList();
        }
        public async Task<List<TotalRecipes_ViewModel>> TotalRecipes_ViewModel(long? userid)
        {
            var res = await _repo.TotalRecipes_ViewModel(userid);
            return res.ToList();
        }

        public async Task<List<ChemicalIssuanceDetail_ViewModel>> ChemicalIssuanceDetail(long? userid)
        {
            var res = await _repo.ChemicalIssuanceDetail(userid);
            return res.ToList();
        }
    }
}
