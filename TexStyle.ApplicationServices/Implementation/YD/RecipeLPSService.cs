using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Implementation.YD {
    internal class RecipeLPSService : IRecipeLPSService {
        private IRecipeLPSRepository _repo;
        //   private IRecipeLPSRepository _repo;
        public RecipeLPSService(IRecipeLPSRepository repo) {
            _repo = repo;
        }

        public async Task<RecipeLPS> Create(RecipeLPS o) {
            try {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<RecipeLPS> Delete(RecipeLPS o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<RecipeLPS>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<RecipeLPS> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<RecipeLPS> GetByLps(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.LPSId == id && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetWeightByRecNo(long id, bool isreprocess) {
            try {
                if (isreprocess) {
                    var list = await _repo.GetList(x => !x.IsDeleted && x.RecipeId == id, x => x.Reprocess);
                    return list.ToList().Sum(x => x.Reprocess.Kgs);
                } else {
                    var res = await _repo.GetList(x => !x.IsDeleted && x.RecipeId == id, x => x.LPS);
                    return res.ToList().Sum(x => x.LPS.Kgs);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<decimal> GetPcsByRecNo(long id, bool isreprocess)
        {
            try
            {
                var res = await _repo.GetList(x => !x.IsDeleted && x.RecipeId == id, x => x.LPS);
                return (decimal)res.ToList().Sum(x => x.LPS.Pcs);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> GetConsByRecNo(long id, bool isreprocess) {
            try {
                if (isreprocess) {
                    var list = await _repo.GetList(x => !x.IsDeleted && x.RecipeId == id, x => x.Reprocess);
                    return list.ToList().Sum(x => x.Reprocess.Cones);
                } else {
                    var list = await _repo.GetList(x => !x.IsDeleted && x.RecipeId == id, x => x.LPS);
                    return list.ToList().Sum(x => x.LPS.Cones);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }



        public async Task<RecipeLPS> Update(RecipeLPS o) {
            try {
                o.UpdatedOn = DateTime.Now;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<RecipeLPS>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<RecipeLPS>> GelListByLps(long? id)
        {
            try
            {
                var list = await _repo.GetList(x => x.LPSId == id && !x.IsDeleted);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
