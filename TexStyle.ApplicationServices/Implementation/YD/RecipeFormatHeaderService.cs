using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Implementation.YD {
    class RecipeFormatHeaderService : IRecipeFormatHeaderService {
        private IRecipeFormatHeaderRepository _repo;
        private IRecipeFormatDetailRepository _repodeail;
        public RecipeFormatHeaderService(IRecipeFormatHeaderRepository recipeFormatHeaderRepository,
            IRecipeFormatDetailRepository recipeFormatDetailRepository) {
            _repo = recipeFormatHeaderRepository;
            _repodeail = recipeFormatDetailRepository;
        }

        public async Task<RecipeFormatHeader> Create(RecipeFormatHeader o) {
            try {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<RecipeFormatHeader> Delete(RecipeFormatHeader o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<RecipeFormatHeader>> GetAll() {
            try {

                var list = await _repo.GetList(x => x.IsDeleted == false, x => x.ProcessType, x => x.RecipeFormatDetails);
                return list.ToList();


            }
            catch (Exception ex) {

                throw ex;
            }
        }
       
        public async Task<List<RecipeFormatHeader>> GetAllYarn()
        {
            try
            {

                var list = await _repo.GetList(x => x.IsDeleted == false && x.IsYarn == true, x => x.ProcessType, x => x.RecipeFormatDetails);
                return list.ToList();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  async Task<List<RecipeFormatHeader>> GetAllFabric()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.IsYarn == false, x => x.ProcessType, x => x.RecipeFormatDetails);
                return list.ToList();
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }
        }


        public async Task<List<RecipeFormatHeader>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<RecipeFormatHeader> GetById(long id) {
            try {
                //x => x.IsDeleted == false,
                var recipeformathearder = await _repo.GetSingle(x => x.Id == id,  x => x.RecipeFormatDetails);
                //recipeformathearder?.RecipeFormatDetails.ToList().ForEach(b => {
                //    var dtl = _repodeail.GetSingle(x => x.Id == b.RecipeFormatHeaderId,
                //     x => x.Dye,
                //     x => x.Chemical,
                //   x => x.RecipeStep);
                //    b.Dye = dtl.Dye;
                //    b.Chemical = dtl.Chemical;
                //    b.RecipeStep = dtl.RecipeStep;
                //});





                return recipeformathearder;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<RecipeFormatHeader> Update(RecipeFormatHeader o) {
            try {
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }
    }
}
