using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class YarnQualityService : IYarnQualityService {
        private IYarnQualityRepository _repo;
        public YarnQualityService(IYarnQualityRepository repo) {
            _repo = repo;
        }
        public async Task<YarnQuality> Create(YarnQuality o) {
            try {
                await _repo.Add(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<YarnQuality> Delete(YarnQuality o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<YarnQuality>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            } catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<YarnQuality>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<YarnQuality> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<YarnQuality> Update(YarnQuality o) {
            try {
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }
    }
}
