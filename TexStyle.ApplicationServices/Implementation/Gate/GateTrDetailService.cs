using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IGate;
using TexStyle.Core.Gate;
using TexStyle.Core.ReportsViewModel.CS.RateDetail;
using TexStyle.DomainServices.Interfaces.IGate;


namespace TexStyle.ApplicationServices.Implementation.Gate
{
    class GateTrDetailService : IGateTrDetailService
    {
        private IGateTrDetailRepository _repo;
        public GateTrDetailService(IGateTrDetailRepository repo)
        {
            _repo = repo;
        }
        public async Task<GateTrDetail> Create(GateTrDetail o)
        {
            try
            {
                o.CreatedOn = DateTime.Now;
                if (o.YarnTypeId == 0)
                {
                    o.YarnTypeId = null;
                }
                if (o.FabricTypeId == 0)
                {
                    o.FabricTypeId = null;

                }
             
               
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GateTrDetail> Delete(GateTrDetail o)
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

        public async Task<List<GateTrDetail>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false, n => n.GateTr);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GateTrDetail>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }



        public async Task<GateTrDetail> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, n => n.GateTr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GateTrDetail> Update(GateTrDetail o)
        {
            try
            {
                o.UpdatedOn = DateTime.Now;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<List<GateTrDetail>> GetByheaderId(long id)
        {
            try
            {
             var list= await _repo.GetList(x => x.IsDeleted == false && x.GateTrId == id, x => x.Dye, x => x.Chemical);
                return list.ToList(); 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public async Task<bool> UpdateRates(List<RateDetail> rateDetail)
        {
            try
            {
              await _repo.UpdateRates(rateDetail);
              return true;
            }
            catch (Exception ex)
            {

            throw ex;
            }
        }
    }
}
