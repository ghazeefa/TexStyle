using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    internal class ReturnNoteDetailService : IReturnNoteDetailService
    {
        private readonly IReturnNoteDetailRepository _repo;
        public ReturnNoteDetailService(IReturnNoteDetailRepository repo)
        {
            _repo = repo;
        }

        public async Task<ReturnNoteDetail> Create(ReturnNoteDetail o)
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
        

        public async Task<ReturnNoteDetail> Delete(ReturnNoteDetail o)
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
        public async Task<List<ReturnNoteDetail>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false, navigationProperties: nav => nav.ReturnNote);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ReturnNoteDetail>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<IList<ReturnNoteDetail>> GetByXreffId(long id)
        {
            try
            {
                return await _repo.GetList(x => x.PPCPlanningId == id && x.IsDeleted == false);
            
                          
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReturnNoteDetail> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, navigationProperties: nav => nav.ReturnNote);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReturnNoteDetail> Update(ReturnNoteDetail o)
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
    }
}
