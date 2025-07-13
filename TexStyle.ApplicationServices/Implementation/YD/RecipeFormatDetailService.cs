using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Implementation.YD {
    public class RecipeFormatDetailService : IRecipeFormatDetailService {

        private IRecipeFormatDetailRepository _repo;
        public RecipeFormatDetailService(IRecipeFormatDetailRepository recipeFormatDetailRepository) {
            _repo = recipeFormatDetailRepository;
        }
        public async Task<RecipeFormatDetail> Create(RecipeFormatDetail o) {
            try {
                await _repo.Add(o);
                return o;
            }
            catch (Exception) {

                throw;
            }
        }

        public async Task<RecipeFormatDetail> Delete(RecipeFormatDetail o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;

            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<RecipeFormatDetail>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<RecipeFormatDetail>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<RecipeFormatDetail> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, nav => nav.RecipeFormatHeader);

            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<RecipeFormatDetail> Update(RecipeFormatDetail o) {
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
