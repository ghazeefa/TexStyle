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
    internal class AccountUserRoleRepository : Repository<AccountUserRole>, IAccountUserRoleRepository {
        private readonly AppDbContext _db;
        public AccountUserRoleRepository(AppDbContext db) : base(db) {
            _db = db;
        }
        public override async Task<IList<AccountUserRole>> GetAll(params Expression<Func<AccountUserRole, object>>[] navigationProperties) {
            return await Task.FromResult(_db.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .AsNoTracking()
                .ToList());
        }
        public override async Task<AccountUserRole> GetSingle(Func<AccountUserRole, bool> where, params Expression<Func<AccountUserRole, object>>[] navigationProperties) {
            return await Task.FromResult( _db.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .AsNoTracking()
                .FirstOrDefault(where));
        }
        public override async Task<IList<AccountUserRole>> GetList(Func<AccountUserRole, bool> where, params Expression<Func<AccountUserRole, object>>[] navigationProperties) {
            return await Task.FromResult( _db.UserRoles
                .Include(x => x.Role)
                .Include(x => x.User)
                .AsNoTracking()
                .Where(where)
                .ToList());

        }
    }
}
