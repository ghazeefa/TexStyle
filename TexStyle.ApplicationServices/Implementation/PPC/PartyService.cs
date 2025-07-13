using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    class PartyService : IPartyService {
        private IPartyRepository _repo;
        public PartyService(IPartyRepository partyRepo) {
            _repo = partyRepo;
        }

        public async Task<Party> Create(Party o) {
            try {
                await _repo.Add(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<Party> Delete(Party o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<List<Party>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.IsHeader == false, nav => nav.Buyers);
                return list.ToList();
            } catch (Exception ex) {

                throw ex;
            }
        }
        public async Task<List<Party>> GetAllWithHeader()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false, nav => nav.Buyers);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<Party>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Party> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, nav => nav.Buyers);
            } catch (Exception ex) {

                throw ex;
            }
        }

        public async Task<Party> Update(Party o) {
            try {
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {

                throw ex;
            }
        }
    }
}
