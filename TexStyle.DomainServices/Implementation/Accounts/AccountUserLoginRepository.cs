using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.DomainServices.Implementation.Accounts {
    internal class AccountUserLoginRepository : Repository<AccountUserLogin>, IAccountUserLoginRepository {
        public AccountUserLoginRepository(AppDbContext db) : base(db) {
        }
    }
}
