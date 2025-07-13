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
    internal class AccountRoleRepository : Repository<AccountRole>, IAccountRoleRepository {
        private readonly AppDbContext _db;
        public AccountRoleRepository(AppDbContext db) : base(db) {
            _db = db;
        }
        public override async Task<IList<AccountRole>> GetList(Func<AccountRole, bool> where, params Expression<Func<AccountRole, object>>[] navigationProperties) {
            return await Task.FromResult( _db.Roles
                .Include(x => x.UserRoles).ThenInclude(u => (u  as AccountUserRole).Role)
                .Include(x => x.UserRoles).ThenInclude(u => (u  as AccountUserRole).User)
                .AsNoTracking()
                .Where(where)
                .ToList());
        }

        public override async Task<AccountRole> GetSingle(Func<AccountRole, bool> where, params Expression<Func<AccountRole, object>>[] navigationProperties) {
            return await Task.FromResult( _db.Roles
                .Include(x => x.UserRoles)
                .Include(x => x.UserRoles).ThenInclude(u => (u as AccountUserRole).Role)
                .Include(x => x.UserRoles).ThenInclude(u => (u as AccountUserRole).User)
                .Where(where)
                .FirstOrDefault());
        }
    }
}
