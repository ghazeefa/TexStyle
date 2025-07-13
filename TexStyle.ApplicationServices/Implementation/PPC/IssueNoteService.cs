using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.ApplicationServices.Implementation.PPC
{
    internal class IssueNoteService : IIssueNoteService
    {
        private readonly IIssueNoteRepository _repo;
        private readonly IIssueNoteDetailRepository _detailRepo;
        public IssueNoteService(IIssueNoteRepository repo, IIssueNoteDetailRepository detailRepo)
        {
            _repo = repo;
            _detailRepo = detailRepo;
        }

        public async Task<IssueNote> Create(IssueNote o)
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

        public async Task<IssueNote> Delete(IssueNote o)
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

        public async Task<List<IssueNote>> GetAll()
        {
            try
            {
                //x => x.IsDeleted == false
                var list = await _repo.GetAll();
                    
                //_repo.GetList(x => x.IsDeleted == false, nav => nav.IssueNoteDetail).ToList();

                //issueses.ForEach(i =>
                //{
                //    i.IssueNoteDetail.ToList().ForEach(d =>
                //    {
                //        var detail = _detailRepo.GetSingle(x => x.Id == d.Id,
                //            x => x.YarnType,
                //            x => x.YarnQuality,
                //            x => x.YarnManufacturer,
                //            x => x.StoreLocation
                //            );

                //        d.YarnType = detail.YarnType;
                //        d.YarnQuality = detail.YarnQuality;
                //        d.YarnManufacturer = detail.YarnManufacturer;
                //        d.StoreLocation = detail.StoreLocation;

                //    });
                //});

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<IssueNote>> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.IssueDate.Date >= start.Date && x.IssueDate.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<IssueNote> GetById(long id)
        {
            try
            {
                return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, nav => nav.IssueNoteDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IssueNote> Update(IssueNote o)
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
