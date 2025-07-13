
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    class LotMarkingService : ILotMarkingService
    {
        private ILotMarkingRepository _repo;
        public LotMarkingService(ILotMarkingRepository partyRepo)
        {
            _repo = partyRepo;
        }

        public async Task<LotMarking> Create(LotMarking o)
        {
            try
            {
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<LotMarking> Delete(LotMarking o)
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

        public async Task<List<LotMarking>> GetAll()
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
        public async Task<List<LotMarking>> GetAllWithHeader()
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
        public async Task<List<LotMarking>> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LotMarking> GetById(long id)
        {
            try
            {
                var res = await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<LotMarking> GetByLPSNo(long id)
        {
            try
            {
                var res = await _repo.GetSingle(x => x.PPCPlanningId == id && x.IsDeleted == false);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<LotMarking> Update(LotMarking o)
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
    }
}
