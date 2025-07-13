using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using TexStyle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.DomainServices.Implementation.Accounts {
    internal class AccountRepository : Repository<Account>, IAccountRepository {
        private readonly AppDbContext _db;
        public AccountRepository(AppDbContext db) : base(db) {
            _db = db;
        }
        public override async Task<IList<Account>> GetAll(params Expression<Func<Account, object>>[] navigationProperties) {
            return await _db.Users
                .Include(x => x.Claims)
                .Include(x => x.Logins)
                .Include(x => x.Tokens)
                .Include(x => x.UserRoles).ThenInclude(r => (r as AccountUserRole).Role)
              .AsNoTracking()
                .ToListAsync();
        }
        public override async Task<Account> GetSingle(Func<Account, bool> where, params Expression<Func<Account, object>>[] navigationProperties) {
            return await Task.FromResult( _db.Users
              .Include(x => x.Claims)
              .Include(x => x.Logins)
              .Include(x => x.Tokens)
              .Include(x => x.UserRoles).ThenInclude(r => (r as AccountUserRole).Role)
              .AsNoTracking()
              .FirstOrDefault(where));
        }
    }
}
