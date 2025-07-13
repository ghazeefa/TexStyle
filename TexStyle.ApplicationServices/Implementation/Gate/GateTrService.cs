using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IGate;
using TexStyle.Core.Gate;
using TexStyle.DomainServices.Interfaces.IGate;

namespace TexStyle.ApplicationServices.Implementation.Gate {
    class GateTrService : IGateTrService {
        private IGateTrRepository _repo;
        public GateTrService(IGateTrRepository repo) {
            _repo = repo;
        }
        public async Task<GateTr> Create(GateTr o) {
            try {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<GateTr> Delete(GateTr o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<List<GateTr>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false, n => n.GateTrDetails, n => n.GateActivityType, n => n.Party);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<List<GateTr>> GetAllByActivityId(long activityid) {
            try
            {

                var list = await _repo.GetList(x => x.IsDeleted == false && x.GateActivityTypeId == activityid, n => n.GateTrDetails, n => n.GateActivityType, n => n.Party);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<List<GateTr>> GetAllByActivityStatus(bool ispurchase, bool isloanin, bool isloanout) {
            try {

                var list = await _repo.GetListForActivityStatus(false, ispurchase, isloanin, isloanout, n => n.GateTrDetails, n => n.Party);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<GateTr> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, n => n.GateTrDetails);
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public async Task<GateTr> GetOGPById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Xref == id && x.IsDeleted == false, n => n.GateTrDetails);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<GateTr> Update(GateTr o) {
            try {
                // update yarn 
                o.IsYarn = true;
                o.BranchId = 1;
                o.UpdatedOn = DateTime.Now;

                await _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<GateTr> UpdateFabric(GateTr o)
        {
            try
            {
                // update Fabric 
                o.IsYarn = false;
                o.BranchId = 2;
                o.UpdatedOn = DateTime.Now;

                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<GateTr> CreateBySno(GateTr o) {
            try {
                o.UpdatedOn = DateTime.Now;
                // create yarn
                if(o.GateActivityTypeId ==1)
                {
                    o.BranchId = 1;
                    o.IsYarn = true;
                }
                //fabric create
                if (o.GateActivityTypeId==10)
                    {
                    o.IsYarn = false;
                    o.BranchId = 2;
                }
               
                await _repo.AddByNewSno(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<GateTr> CreateByActivityType(GateTr o, bool ispurchase, bool isloanin, bool isloanout) {
            try {
       
                o.UpdatedOn = DateTime.Now;
                o.BranchId = 1;
                await _repo.AddByNewActivityType(o, ispurchase, isloanin, isloanout);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<GateTr>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.Date.Date >= start.Date && x.Date.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<GateTr>> GetBetweenDateRangeByActivityStatus(bool ispurchase, bool isloanin, bool isloanout, DateTime start, DateTime end) {
            try {
                var list = await GetAllByActivityStatus(ispurchase, isloanin, isloanout);
                return list.Where(x => x.Date.Date >= start.Date && x.Date.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<List<GateTr>> GetBetweenDateRangeByActivityTypeId(long activityTypeId, DateTime start, DateTime end) {
            try {
                var list = await GetAllByActivityId(activityTypeId);
                return list.Where(x => x.Date.Date >= start.Date && x.Date.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }

}
