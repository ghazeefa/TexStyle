using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.DomainServices.Implementation.Accounts {
    internal class AccountUserTokenRepository : Repository<AccountUserToken>, IAccountUserTokenRepository {
        public AccountUserTokenRepository(AppDbContext db) : base(db) {
        }
    }
}
