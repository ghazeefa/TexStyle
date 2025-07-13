using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountRoleService : IAccountRoleService {
        private readonly IAccountRoleRepository _repo;
        private readonly IAccountUserRoleRepository _accountUserRoleRepo;
        private readonly IAccountRoleClaimRepository _accountRoleClaimRepo;
        public AccountRoleService(IAccountRoleRepository repo, IAccountUserRoleRepository accountUserRoleRepo, IAccountRoleClaimRepository accountRoleClaimRepo) {
            _repo = repo;
            _accountUserRoleRepo = accountUserRoleRepo;
            _accountRoleClaimRepo = accountRoleClaimRepo;
        }
        public async Task<AccountRole> Create(AccountRole o) {
            try {
                await _repo.Add(o);

                if (o.RoleClaims.Count > 0) {
                    o.RoleClaims.ToList().ForEach(x => x.RoleId = o.Id);
                    await _accountRoleClaimRepo.Add(o.RoleClaims.ToArray());
                }

                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<AccountRole> Delete(AccountRole o) {
            try {
                var role = await GetById(o.Id);
                await _repo.Remove(role);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<AccountRole>> GetAll() {
            try {
                var list = await _repo.GetAll(x => x.UserRoles, x => x.RoleClaims);
                var accountRoles = list.ToList();

                await Task.WhenAll(accountRoles.Select(async x =>
                { 
                    x.UserRoles = (await _accountUserRoleRepo.GetList(u => u.RoleId == x.Id)).ToList();
                }));
                return accountRoles;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<AccountRole>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<AccountRole> GetById(long id) {
            try {
                var m = await _repo.GetSingle(x => x.Id == id, x => x.RoleClaims);
                var roleClaimsData = await _accountRoleClaimRepo.GetList(x => x.RoleId == m.Id);
                var roleClaims = roleClaimsData.ToList();
                m.RoleClaims = roleClaims;
                return m;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<AccountRole>> GetUserRoles() {
            try {
                var accountRolesData = await _repo.GetList(x => !x.Name.Equals("developer", StringComparison.OrdinalIgnoreCase),
                    x => x.UserRoles,
                    x => x.RoleClaims);
                var accountRoles = accountRolesData.ToList();

                await Task.WhenAll(accountRoles.Select(async x =>
                {
                    x.UserRoles = (await _accountUserRoleRepo.GetList(u => u.RoleId == x.Id)).ToList();
                }));
                return accountRoles;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<AccountRole> Update(AccountRole o) {
            try {
                var f = await GetById(o.Id);
                f.Name = o.Name;
                f.NormalizedName = o.Name.ToUpper();

                if (f.RoleClaims.Count > 0) {
                    await _accountRoleClaimRepo.Remove(f.RoleClaims.ToArray());

                    if (o.RoleClaims.Count > 0) {
                       await _accountRoleClaimRepo.Add(o.RoleClaims.ToArray());
                    }
                } else {
                    await _accountRoleClaimRepo.Add(o.RoleClaims.ToArray());
                }
                await _repo.Update(f);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
