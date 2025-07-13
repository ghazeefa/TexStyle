using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;

namespace TexStyle.ApplicationServices.Implementation.YD {
    internal class DyeService : IDyeService {
        private IDyeRepository _repo;
        public DyeService(IDyeRepository repo) {
            _repo = repo;
        }

        public async Task<Dye> Create(Dye o) {
            try {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Dye> Delete(Dye o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Dye>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Dye>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Dye> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public async Task<Dye> Update(Dye o)                            {
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
