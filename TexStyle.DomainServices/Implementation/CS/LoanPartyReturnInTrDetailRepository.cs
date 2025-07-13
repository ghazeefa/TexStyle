using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS {
    internal class LoanPartyReturnInTrDetailRepository : Repository<LoanPartyReturnInTrDetail>, ILoanPartyReturnInTrDetailRepository {
        public LoanPartyReturnInTrDetailRepository(AppDbContext db) : base(db) {
        }
    }
}
