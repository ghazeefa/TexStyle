using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IGate;
using TexStyle.Core.Gate;
using TexStyle.DomainServices.Interfaces.IGate;

namespace TexStyle.ApplicationServices.Implementation.Gate {
    class GateActivityTypeService : IGateActivityTypeService {
        private IGateActivityTypeRepository _repo;
        public GateActivityTypeService(IGateActivityTypeRepository repo) {
            _repo = repo;
        }
        public async Task<GateActivityType> Create(GateActivityType o) {
            try {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<GateActivityType> Delete(GateActivityType o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<GateActivityType>> GetAll() {
            try {
                return (await _repo.GetList(x => x.IsDeleted == false)).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<GateActivityType>> GetAllByStatus(bool ispurchaseactivity, bool isloaninactivity, bool isloanoutactivity) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.IsPurchaseActivity == ispurchaseactivity && x.IsLoanINActivity == isloaninactivity && x.IsLoanOutActivity == isloanoutactivity);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<GateActivityType>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<GateActivityType> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<GateActivityType> GetByName(string name) {
            try {
                return await _repo.GetSingle(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && x.IsDeleted == false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<GateActivityType> Update(GateActivityType o) {
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
