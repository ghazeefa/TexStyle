using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Common;
using TexStyle.Core.PPC;
using TexStyle.Core.ReportsViewModel.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC {
    internal class OGPService : IOGPService {
        private readonly IOGPRepository _repo;
        private readonly IOGPDetailRepository _ogpDetailRepo;
        public OGPService(IOGPRepository repo, IOGPDetailRepository oGPDetail) {
            _repo = repo;
            _ogpDetailRepo = oGPDetail;
        }

        public async Task<OutwardGatePass> Create(OutwardGatePass o) {
            try {
            
                o.BranchId = 1;
                o.IsYarn = true;
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<OutwardGatePass> CreateFabric(OutwardGatePass o)
        {
            try
            {
                o.BranchId = 2;
                o.IsYarn = false;
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OutwardGatePass> Delete(OutwardGatePass o) {
            try {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<OutwardGatePass>> GetAll() {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false, n => n.YarnType, n => n.ActivityType, n => n.OutwardGatePassDetails);
                return list.ToList();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<OutwardGatePass>> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.OgpDate.Date >= start.Date && x.OgpDate.Date <= end.Date && x.BranchId == 1 && x.IsYarn == true);
                return list.ToList(); 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public List<OutwardGatePass> GetBetweenDateRange(DateTime start, DateTime end)
        //{
        //    try
        //    {
        //        var res =_repo.GetList(x => x.IsDeleted == false && x.OgpDate.Date >= start.Date && x.OgpDate.Date <= end.Date).ToList();
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public async Task<List<GetbteweenRange_OGPRepositoryViewModel_P8>> GetbteweenRange_OGP(DateTime start, DateTime end, long userid)
        {
            try
            {

                return await _repo.GetbteweenRange_OGPRepositoryViewModel_P8(start, end, userid);
            }
            catch (Exception ex)
            {

                throw ex;
            }




        }
        public async Task<List<OutwardGatePass>> GetBetweenDateRangeFabric(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.OgpDate.Date >= start.Date && x.OgpDate.Date <= end.Date && x.IsYarn == false && x.BranchId == 2);
                return list.ToList(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public async Task<OutwardGatePass> GetById(long id) {
            try {
                var m = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, n => n.YarnType, n=> n.FabricTypes, n => n.ActivityType, n => n.OutwardGatePassDetails, n => n.Buyer);
                if(m.OutwardGatePassDetails.Count > 0) {
                    m.OutwardGatePassDetails = m.OutwardGatePassDetails.Where(x => x.IsDeleted == false).ToList();
                }
                return m;
            } catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<List<OutwardGatePassDetail>> GetOgpReport(PagingOptions options)
        {
            var ogpRep = await _ogpDetailRepo.GetAllQueryable(x => x.OutwardGatePass);
            Expression<Func<OutwardGatePassDetail, bool>> predicate = null;
            predicate = x => x.IsDeleted == false;

            if (options.Offset.HasValue && options.Limit.HasValue)
            {
                ogpRep = ogpRep
                    .Skip(options.Offset.Value)
                    .Take(options.Limit.Value);
            }

            if (predicate != null)
            {
                ogpRep.Where(predicate);
            }
            var res = ogpRep.ToList(); // ok sql you can run this command
            return await Task.FromResult(res);
        }
        //public List<OutwardGatePassDetail> GetOgpReport(PagingOptions options)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<OutwardGatePass> Update(OutwardGatePass o) {
            try {
                o.IsYarn = true;
                o.BranchId = 1;
                await _repo.Update(o);
                return o;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<OutwardGatePass> UpdateFabric(OutwardGatePass o)
        {
            try
            {
                // update Fabric user
                o.IsYarn = false;
                o.BranchId = 2;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
