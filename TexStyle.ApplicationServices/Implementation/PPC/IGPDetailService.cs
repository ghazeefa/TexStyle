using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    internal class IGPDetailService : IIGPDetailService
    {
        private readonly IIGPDetailRepository _repo;
        public IGPDetailService(IIGPDetailRepository repo)
        {
            _repo = repo;
        }

        public async Task<InwardGatePassDetail> Create(InwardGatePassDetail o)
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


        public async Task<InwardGatePassDetail> Delete(InwardGatePassDetail o)
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

        public async Task<List<InwardGatePassDetail>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<InwardGatePassDetail>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<InwardGatePassDetail> GetById(long id,long sno) {
            try {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false && x.Sno==sno,
                    x=> x.YarnType,
                    x => x.YarnManufacturer,
                    x=> x.YarnQuality,
                    x=> x.StoreLocation,
                    x => x.BagMarking,
                    x => x.ConeMarking,
                     x => x.RollMarking,
                     x => x.FabricTypes,
                      x => x.FabricQuality,
                      x=>x.knittingParty,
                      x=>x.ActivityType
                      //x=>x.Weight,
                      //x=>x.Dia,
                      //x=>x.GSM
                      );
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<InwardGatePassDetail> GetById(long id)
        {
            try
            {
                var result = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false,
                    x => x.YarnType,
                    x => x.YarnManufacturer,
                    x => x.YarnQuality,
                    x => x.StoreLocation,
                    x => x.BagMarking,
                    x => x.ConeMarking,
                    x=>x.RollMarking,
                     x => x.FabricTypes,
                      x => x.FabricQuality,
                      x=>x.knittingParty,
                        x => x.ActivityType
                    //x => x.Weight,
                    //x => x.Dia,
                    //x => x.GSM
                    );
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<InwardGatePassDetail>> GetByHeaderId(long id)
        {
            try
            {
                var result = await _repo.GetList(x => x.InwardGatePassId == id && x.IsDeleted == false);
                return result.ToList(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<InwardGatePassDetail> Update(InwardGatePassDetail o)
        {
            try { 
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
