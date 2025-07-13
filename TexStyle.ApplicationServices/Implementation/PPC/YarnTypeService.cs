using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class YarnTypeService : IYarnTypeService {
        private IYarnTypeRepository _repo;
        public YarnTypeService(IYarnTypeRepository yarnTypeRepo) {
            _repo = yarnTypeRepo;
        }

        public async Task<YarnType> Create(YarnType o) {
            try {
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                // TODO: need to add custom exception here
                throw ex;
            }
        }

        public async Task<YarnType> Delete(YarnType o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<YarnType>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<YarnType>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<YarnType> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<YarnType> Update(YarnType o) {
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
