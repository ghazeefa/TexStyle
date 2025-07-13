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
    class LoanPartyGivenOutTrRepository : Repository<LoanPartyGivenOutTr>, ILoanPartyGivenOutTrRepository
    {
        private AppDbContext _db;
        public LoanPartyGivenOutTrRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public override LoanPartyGivenOutTr GetSingle(Func<LoanPartyGivenOutTr, bool> where, params Expression<Func<LoanPartyGivenOutTr, object>>[] navigationProperties)
        {
            return _db.LoanPartyGivenOutTrs
                .Include(x => x.Party)
                .Include(x => x.LoanPartyGivenOutTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LoanPartyGivenOutTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LoanPartyGivenOutTrDetails).ThenInclude(y => y.LoanPartyGivenOutTr)
                .SingleOrDefault(where);
        }

        public override IList<LoanPartyGivenOutTr> GetList(Func<LoanPartyGivenOutTr, bool> where, params Expression<Func<LoanPartyGivenOutTr, object>>[] navigationProperties)
        {
            return _db.LoanPartyGivenOutTrs
                .Include(x => x.Party)
                .Include(x => x.LoanPartyGivenOutTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LoanPartyGivenOutTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LoanPartyGivenOutTrDetails).ThenInclude(y => y.LoanPartyGivenOutTr)
                .Where(where).ToList();
        }
    }
}
