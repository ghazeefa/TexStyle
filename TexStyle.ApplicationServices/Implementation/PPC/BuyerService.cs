using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class BuyerService : IBuyerService {
        private readonly IBuyerRepository _repo;
        public BuyerService(IBuyerRepository repo) {
            _repo = repo;
        }

        public async Task<Buyer> Create(Buyer o) {
            try {
                await _repo.Add(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Buyer> Delete(Buyer o) {
            try {
                var found = await GetById(o.Id);
                found.IsDeleted = true;
                await _repo.Update(found);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Buyer>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false, nav => nav.BuyerColors, nav => nav.Party);
                return list.ToList();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<Buyer>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Buyer> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, nav => nav.BuyerColors, nav => nav.Party);
            } catch (Exception ex) {
                throw ex;
            }
        }
     
        public async Task<Buyer> Update(Buyer o) {
            try {
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
