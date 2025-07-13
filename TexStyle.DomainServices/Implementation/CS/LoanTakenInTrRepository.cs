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
    internal class LoanTakenInTrRepository : Repository<LoanTakenInTr>, ILoanTakenInTrRepository {
        private AppDbContext _db;
        public LoanTakenInTrRepository(AppDbContext db) : base(db) {
            _db = db;
        }

        public override LoanTakenInTr GetSingle(Func<LoanTakenInTr, bool> where, params Expression<Func<LoanTakenInTr, object>>[] navigationProperties)
        {
            return _db.LoanTakenInTrs
                .Include(x => x.Party)
                .Include(x => x.GateTr)
                .Include(x => x.LoanTakenInTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LoanTakenInTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LoanTakenInTrDetails).ThenInclude(y => y.LoanTakenInTr)
                .SingleOrDefault(where);
        }

        public override IList<LoanTakenInTr> GetList(Func<LoanTakenInTr, bool> where, params Expression<Func<LoanTakenInTr, object>>[] navigationProperties)
        {
            return _db.LoanTakenInTrs
                .Include(x => x.Party)
                .Include(x => x.GateTr)
                .Include(x => x.LoanTakenInTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LoanTakenInTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LoanTakenInTrDetails).ThenInclude(y => y.LoanTakenInTr)
                .Where(where).ToList();
        }
    }
}
