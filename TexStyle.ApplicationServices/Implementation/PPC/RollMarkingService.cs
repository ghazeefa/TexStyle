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
    internal class RollMarkingService : IRollMarkingService
    {

        private IRollMarkingRepository _repo;
        public RollMarkingService(IRollMarkingRepository repo)
        {
            _repo = repo;
        }

        public async Task<RollMarking> Create(RollMarking o)
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

        public async Task<RollMarking> Delete(RollMarking o)
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

        public async Task<List<RollMarking>> GetAll()
        {
            try
            {
                var list = await _repo.GetAll(x => x.IsDeleted == false);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<RollMarking>> GetBetweenDateRange(DateTime start, DateTime end)
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

        public async Task<RollMarking> GetById(long id)
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

        public async Task<RollMarking> Update(RollMarking o)
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
