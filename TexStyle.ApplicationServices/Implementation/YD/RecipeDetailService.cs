using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Implementation.YD {
    internal class RecipeDetailService : IRecipeDetailService {
        private IRecipeDetailRepository _repo;
        public RecipeDetailService(IRecipeDetailRepository repo) {
            _repo = repo;
        }

        public async Task<RecipeDetail> Create(RecipeDetail o) {
            try {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<RecipeDetail> Delete(RecipeDetail o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<RecipeDetail>> DeletebyId(long recipeno) {
            try {

                var recdetail = await _repo.GetList(x => x.RecipeId == recipeno);
                foreach (RecipeDetail d in recdetail) {
                    d.IsDeleted = true;
                    await _repo.Update(d);

                }
                return recdetail;
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public async Task<List<RecipeDetail>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false,
                    n => n.Recipe,
                    n => n.RecipeStep,
                    n => n.Dye,
                    n => n.Chemical
                    );
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<RecipeDetail>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<RecipeDetail> GetById(long id) {
            try {
                //return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
                //    n => n.Recipe,
                //    n => n.RecipeStep,
                //    n => n.Dye,
                //    n => n.Chemical
                //    );
                return await _repo.GetSingleById(id);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IList<RecipeDetail>> GetByRecNo(long id) {
            try {
                return await _repo.GetList(x => x.RecipeId == id && x.IsDeleted == false,
                    n => n.Recipe,
                    n => n.RecipeStep,
                    n => n.Dye,
                    n => n.Chemical
                    );
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<RecipeDetail> Update(RecipeDetail o) {
            try {
                o.UpdatedOn = DateTime.Now;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
