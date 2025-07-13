using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    internal class IssueNoteDetailService : IIssueNoteDetailService
    {
        private readonly IIssueNoteDetailRepository _repo;
        public IssueNoteDetailService(IIssueNoteDetailRepository repo)
        {
            _repo = repo;
        }

        public async Task<IssueNoteDetail> Create(IssueNoteDetail o)
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

        public async Task<IssueNoteDetail> Delete(IssueNoteDetail o)
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

        public async Task<List<IssueNoteDetail>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false, n => n.IssueNote,
                    //n => n.YarnManufacturer,
                    //n => n.YarnQuality,
                    //n => n.YarnType,
                    n => n.StoreLocation
                    );
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<IssueNoteDetail>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IssueNoteDetail> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, 
                    n => n.IssueNote,
                    //n => n.YarnManufacturer,
                    //n => n.YarnQuality,
                    //n => n.YarnType,
                    n => n.StoreLocation
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IssueNoteDetail> GetByIGPNO(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.PPCPlanningId == id && x.IsDeleted == false,
                    n => n.IssueNote,
                    //n => n.YarnManufacturer,
                    //n => n.YarnQuality,
                    //n => n.YarnType,
                    n => n.StoreLocation
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IssueNoteDetail> Update(IssueNoteDetail o)
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
