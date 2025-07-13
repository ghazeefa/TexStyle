using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    class ShadeService : IShadeService {
        private IShadeRepository _repo;
        public ShadeService(IShadeRepository shadeRepository) {
            _repo = shadeRepository;
        }

        public async Task<Shade> Create(Shade o) {
            try {
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<Shade> Delete(Shade o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<Shade>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<Shade>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Shade> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);

            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<Shade> Update(Shade o) {
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
