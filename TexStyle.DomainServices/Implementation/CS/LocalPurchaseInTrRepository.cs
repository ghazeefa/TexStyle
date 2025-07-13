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
    internal class LocalPurchaseInTrRepository : Repository<LocalPurchaseInTr>, ILocalPurchaseInTrRepository {
        private AppDbContext _db;
        public LocalPurchaseInTrRepository(AppDbContext db) : base(db) {
            _db = db;
        }
        public override LocalPurchaseInTr GetSingle(Func<LocalPurchaseInTr, bool> where, params Expression<Func<LocalPurchaseInTr, object>>[] navigationProperties)
        {
            return _db.LocalPurchaseInTrs
                .Include(x => x.Party)
                .Include(x => x.GateTr)
                .Include(x => x.localPurchaseInTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.localPurchaseInTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.localPurchaseInTrDetails).ThenInclude(y => y.LocalPurchaseInTr)
                .SingleOrDefault(where);
        }

        public override IList<LocalPurchaseInTr> GetList(Func<LocalPurchaseInTr, bool> where, params Expression<Func<LocalPurchaseInTr, object>>[] navigationProperties)
        {
            return _db.LocalPurchaseInTrs
                .Include(x => x.Party)
                .Include(x => x.GateTr)
                .Include(x => x.localPurchaseInTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.localPurchaseInTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.localPurchaseInTrDetails).ThenInclude(y => y.LocalPurchaseInTr)
                .Where(where).ToList();
        }
    }
}
