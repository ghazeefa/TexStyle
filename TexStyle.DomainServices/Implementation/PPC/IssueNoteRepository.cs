using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
    internal class IssueNoteRepository : Repository<IssueNote>, IIssueNoteRepository
    {
        private readonly AppDbContext _db;
        public IssueNoteRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IList<IssueNote>> GetAll()
        {
            //return base.GetAll(navigationProperties);
            return await _db.IssueNotes
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnManufacturer)
               .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

               .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.BuyerColor)


                .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.BuyerColor)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

                //  .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnType)
                // .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnQuality)
                //.Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnManufacturer)
                .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).StoreLocation)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .ToListAsync();

        }


        //public override IList<IssueNote> GetAll(params Expression<Func<IssueNote, object>>[] navigationProperties) {
        //    //return base.GetAll(navigationProperties);
        //    var list = _db.IssueNotes
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnType)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnQuality)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnManufacturer)
        //       .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

        //       .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.BuyerColor)


        //        .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnType)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnQuality)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnManufacturer)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.BuyerColor)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

        //        //  .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnType)
        //        // .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnQuality)
        //        //.Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnManufacturer)
        //        .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).StoreLocation)
        //        .Where(x => x.IsDeleted == false);

        //    return list.AsNoTracking().ToList();
        //}

        public async Task<IssueNote> GetSingle()
        {
            return await Task.FromResult( _db.IssueNotes
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.BuyerColor)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnType)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnQuality)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.BuyerColor)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnManufacturer)
              .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

               //.Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnQuality)
               //.Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnManufacturer)
               .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).StoreLocation)
               .AsNoTracking().Where(x=>x.IsDeleted == false)
               .SingleOrDefault());
        }

        //public override IssueNote GetSingle(Func<IssueNote, bool> where, params Expression<Func<IssueNote, object>>[] navigationProperties)
        //{
        //    return _db.IssueNotes
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y=>y.YarnType)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y=>y.YarnQuality)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y=>y.YarnManufacturer)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y=>y.BuyerColor)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).PPCPlanning).ThenInclude(y=>y.InwardGatePassDetail).ThenInclude(p=>p.InwardGatePass)

        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z=>z.PPCPlanning).ThenInclude(y => y.YarnType)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnQuality)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.BuyerColor)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.YarnManufacturer)
        //      .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).Reprocess).ThenInclude(z => z.PPCPlanning).ThenInclude(y => y.InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

        //       //.Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnQuality)
        //       //.Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).YarnManufacturer)
        //       .Include(x => x.IssueNoteDetail).ThenInclude(id => (id as IssueNoteDetail).StoreLocation)
        //       .AsNoTracking()
        //       .SingleOrDefault(where);
        //}
    }
}
