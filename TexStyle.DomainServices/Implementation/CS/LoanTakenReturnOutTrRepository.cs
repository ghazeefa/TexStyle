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
    class LoanTakenReturnOutTrRepository : Repository<LoanTakenReturnOutTr>, ILoanTakenReturnOutTrRepository
    {
        private AppDbContext _db;
        public LoanTakenReturnOutTrRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public override LoanTakenReturnOutTr GetSingle(Func<LoanTakenReturnOutTr, bool> where, params Expression<Func<LoanTakenReturnOutTr, object>>[] navigationProperties)
        {
            return _db.LoanTakenReturnOutTrs
                .Include(x => x.Party)
                 .Include(x => x.LoanTakenInTr).ThenInclude(y => y.GateTr)
                .Include(x => x.LoanTakenReturnOutTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LoanTakenReturnOutTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LoanTakenReturnOutTrDetails).ThenInclude(y => y.LoanTakenReturnOutTr)
                .SingleOrDefault(where);
        }

        public override IList<LoanTakenReturnOutTr> GetList(Func<LoanTakenReturnOutTr, bool> where, params Expression<Func<LoanTakenReturnOutTr, object>>[] navigationProperties)
        {
            return _db.LoanTakenReturnOutTrs
                .Include(x => x.Party)
                .Include(x => x.LoanTakenInTr).ThenInclude(y=>y.GateTr)
                .Include(x => x.LoanTakenReturnOutTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LoanTakenReturnOutTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LoanTakenReturnOutTrDetails).ThenInclude(y => y.LoanTakenReturnOutTr)
               // .Include(x => x.LoanTakenReturnOutTrDetails).ThenInclude(y => y.Lo)
                  .Where(where).ToList();

        }
    }
}
