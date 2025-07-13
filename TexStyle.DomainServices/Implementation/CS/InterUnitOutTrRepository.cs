using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS
{
    class InterUnitOutTrRepository: Repository<InterUnitOutTr>, IInterUnitOutTrRepository
    {
        private AppDbContext _db;
        public InterUnitOutTrRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public override InterUnitOutTr GetSingle(Func<InterUnitOutTr, bool> where, params Expression<Func<InterUnitOutTr, object>>[] navigationProperties)
        {
            return _db.InterUnitOutTrs
                .Include(x => x.Party)
                .Include(x => x.InterUnitOutTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.InterUnitOutTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.InterUnitOutTrDetails).ThenInclude(y => y.InterUnitOutTr)
                .SingleOrDefault(where);
        }

        public override IList<InterUnitOutTr> GetList(Func<InterUnitOutTr, bool> where, params Expression<Func<InterUnitOutTr, object>>[] navigationProperties)
        {
            return _db.InterUnitOutTrs
                .Include(x => x.Party)
                .Include(x => x.InterUnitOutTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.InterUnitOutTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.InterUnitOutTrDetails).ThenInclude(y => y.InterUnitOutTr)
                .Where(where).ToList();
        }
    }
}
