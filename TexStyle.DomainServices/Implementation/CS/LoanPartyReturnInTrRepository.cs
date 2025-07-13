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
    internal class LoanPartyReturnInTrRepository : Repository<LoanPartyReturnInTr>, ILoanPartyReturnInTrRepository {
        private AppDbContext _db;
        public LoanPartyReturnInTrRepository(AppDbContext db) : base(db) {
            _db = db;
        }

        public override LoanPartyReturnInTr GetSingle(Func<LoanPartyReturnInTr, bool> where, params Expression<Func<LoanPartyReturnInTr, object>>[] navigationProperties)
        {
            return _db.LoanPartyReturnInTrs
                .Include(x => x.Party)
                .Include(x => x.LoanPartyReturnInTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LoanPartyReturnInTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LoanPartyReturnInTrDetails).ThenInclude(y => y.LoanPartyReturnInTr)
                .SingleOrDefault(where);
        }

        public override IList<LoanPartyReturnInTr> GetList(Func<LoanPartyReturnInTr, bool> where, params Expression<Func<LoanPartyReturnInTr, object>>[] navigationProperties) => 
            _db.LoanPartyReturnInTrs
                .Include(x => x.Party)
                .Include(x => x.LoanPartyReturnInTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.LoanPartyReturnInTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.LoanPartyReturnInTrDetails).ThenInclude(y => y.LoanPartyReturnInTr)
                .Where(where).ToList();

    }
}
