using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    class YarnManufacturerService : IYarnManufacturerService {
        private readonly IYarnManufacturerRepository _repo;
        public YarnManufacturerService(IYarnManufacturerRepository repo) {
            _repo = repo;
        }

        public async Task<YarnManufacturer> Create(YarnManufacturer o) {
            try {
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<YarnManufacturer> Delete(YarnManufacturer o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<YarnManufacturer>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<YarnManufacturer>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<YarnManufacturer> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<YarnManufacturer> Update(YarnManufacturer o) {
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
