using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Implementation;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Interfaces.Accounts {
    internal class ReprocessRepository : Repository<Reprocess>, IReprocessRepository {
        private AppDbContext _db;
        public ReprocessRepository(AppDbContext db) : base(db) {
            _db = db;
        }


        public override async Task<IList<Reprocess>> GetAll(params Expression<Func<Reprocess, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.Reprocesses
             .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnType)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnQuality)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnManufacturer)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).ProductionType)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)

                .Where(x => x.IsDeleted == false).AsNoTracking().ToList());
        }

        public override async Task<Reprocess> GetSingle(Func<Reprocess, bool> where, params Expression<Func<Reprocess, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.Reprocesses
               .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnType)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnQuality)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).ProductionType)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).YarnManufacturer)
                            .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).BuyerColor).ThenInclude(y => y.Buyer).ThenInclude(z => z.Party)
              .Include(x => x.PPCPlanning).ThenInclude(id => (id as PPCPlanning).InwardGatePassDetail).ThenInclude(p => p.InwardGatePass)
                .AsNoTracking()
                .SingleOrDefault(where));
        }

    }
}
