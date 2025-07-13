using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class BuyerColorService : IBuyerColorService {
        private readonly IBuyerColorRepository _repo;
        public BuyerColorService(IBuyerColorRepository repo) {
            _repo = repo;
        }

        public async Task<BuyerColor> Create(BuyerColor o) {
            try {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<BuyerColor> Delete(BuyerColor o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<BuyerColor>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false, navigationProperties: nav => nav.Buyer);
                return list.ToList();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<BuyerColor>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<BuyerColor> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, navigationProperties: nav => nav.Buyer);
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<BuyerColor> Update(BuyerColor o) {
            try {
                o.UpdatedOn = DateTime.Now;
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
