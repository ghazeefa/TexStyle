using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS {
    internal class LCImportInTrRepository : Repository<LCImportInTr>, ILCImportInTrRepository {
        private AppDbContext _db;
        public LCImportInTrRepository(AppDbContext db) : base(db) {
            _db = db;
        }

        public override LCImportInTr GetSingle(Func<LCImportInTr, bool> where, params Expression<Func<LCImportInTr, object>>[] navigationProperties)
        {
            return _db.LCImportInTrs
                .Include(x => x.Party)
                .Include(x => x.GateTr)
                .Include(x => x.LCImportInTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LCImportInTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LCImportInTrDetails).ThenInclude(y => y.LCImportInTr)
                .SingleOrDefault(where);
        }

        public override IList<LCImportInTr> GetList(Func<LCImportInTr, bool> where, params Expression<Func<LCImportInTr, object>>[] navigationProperties)
        {
            return _db.LCImportInTrs
                .Include(x => x.Party)
                .Include(x => x.GateTr)
                .Include(x => x.LCImportInTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LCImportInTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LCImportInTrDetails).ThenInclude(y => y.LCImportInTr)
                .Where(where).ToList();
        }
    }
}
