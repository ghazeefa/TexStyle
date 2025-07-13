using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    internal class OGPDetailService : IOGPDetailService
    {
        private readonly IOGPDetailRepository _repo;
        public OGPDetailService(IOGPDetailRepository repo)
        {
            _repo = repo;
        }

        public async Task<OutwardGatePassDetail> Create(OutwardGatePassDetail o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OutwardGatePassDetail> Delete(OutwardGatePassDetail o)
        {
            try
            {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OutwardGatePassDetail>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false,
                    x => x.YarnType,
                    x => x.OutwardGatePass);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OutwardGatePassDetail>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<OutwardGatePassDetail> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
                    x => x.YarnType,
                    x => x.OutwardGatePass);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public OutwardGatePassDetail GetByIGPDetailId(long id)
        //{
        //    try
        //    {
        //        return _repo.GetSingle(x => x.InwardGatePassDetailId == id && x.IsDeleted == false,
        //            x => x.YarnType,
        //            x => x.OutwardGatePass);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<List<OutwardGatePassDetail>> GetByIGPDetailId(long id)
        {
            try
            {
                var list = await _repo.GetList(x => x.InwardGatePassDetailId == id && x.IsDeleted == false,
                    x => x.YarnType,
                    x => x.OutwardGatePass);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetByKgSumbyLPSNo(long id)
        {
            try
            {
                var kg = await _repo.GetList(x => x.PPCPlanningId == id && x.IsDeleted == false);
                return kg.Sum(x => x.Kgs); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetBybagsSumbyLPSNo(long id)
        {
            try
            {
                var kg = await _repo.GetList(x => x.PPCPlanningId == id && x.IsDeleted == false);
                return kg.Sum(x => x.Bags); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public OutwardGatePassDetail GetByIGPNo(long id)
        //{
        //    try
        //    {
        //        return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
        //            x => x.YarnType,
        //            x => x.OutwardGatePass);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        public async Task<OutwardGatePassDetail> Update(OutwardGatePassDetail o)
        {
            try
            {
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<OutwardGatePassDetail>> GetIgpRepYarnRecievedReport(ReportFilter filter)
        {
            try
            {

                Func<OutwardGatePassDetail, bool> igpPred = x => x.IsDeleted == false;

                if (filter.DateFrom.HasValue && filter.DateTo.HasValue)
                {
                    igpPred += (x => x.IsDeleted == false && x.OutwardGatePass.OgpDate.Date >= filter.DateFrom.Value.Date && x.OutwardGatePass.OgpDate.Date <= filter.DateTo.Value.Date);
                }

                var res = await _repo.GetList(igpPred,
                    nav => nav.PPCPlanning,
                    nav => nav.OutwardGatePass);
              
                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
