using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountUserRoleService : IAccountUserRoleService {
        private readonly IAccountUserRoleRepository _repo;
        private readonly IAccountRoleRepository _accountRoleRepo;
        public AccountUserRoleService(IAccountUserRoleRepository repo, IAccountRoleRepository accountRoleRepo) {
            _repo = repo;
            _accountRoleRepo = accountRoleRepo;
        }
        public async Task<AccountUserRole> Create(AccountUserRole o) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserRole> Delete(AccountUserRole o) {
            throw new NotImplementedException();
        }

        public async Task<List<AccountUserRole>> GetAll() {
            var userRolesData = await _repo.GetAll();
            var userRoles = userRolesData.ToList();
            return userRoles;
        }

        public async Task<List<AccountUserRole>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserRole> GetById(long id) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserRole> Update(AccountUserRole o) {
            throw new NotImplementedException();
        }
    }
}
