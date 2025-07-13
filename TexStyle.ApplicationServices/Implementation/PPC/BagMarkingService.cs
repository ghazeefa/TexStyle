using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.DomainServices.Interfaces;
using TexStyle.Core.PPC;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class BagMarkingService : IBagMarkingService {
        private IBagMarkingRepository _repo;
        public BagMarkingService(IBagMarkingRepository repo) {
            _repo = repo;
        }

        public async Task<BagMarking> Create(BagMarking o) {
            try {
                await _repo.Add(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<BagMarking> Delete(BagMarking o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<BagMarking>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<BagMarking>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<BagMarking> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted ==false);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<BagMarking> Update(BagMarking o)
        {
            try
            {
                await _repo.Update(o);
                return o;
            } catch (Exception ex) 
            {
                throw ex;
            }
        }
    }


}
