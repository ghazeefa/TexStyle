
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    internal class RollMarkingDetailService : IRollMarkingDetailService
    {

        private IRollMarkingDetailRepository _repo;
        public RollMarkingDetailService(IRollMarkingDetailRepository repo)
        {
            _repo = repo;
        }

        public async Task<RollMarkingDetail> Create(RollMarkingDetail o)
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

        public async Task<RollMarkingDetail> Delete(RollMarkingDetail o)
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

        public async Task<List<RollMarkingDetail>> GetAll()
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

        public async Task<List<RollMarkingDetail>> GetBetweenDateRange(DateTime start, DateTime end)
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

        public async Task<RollMarkingDetail> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RollMarkingDetail> Update(RollMarkingDetail o)
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
