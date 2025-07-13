using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountRoleClaimService : IAccountRoleClaimService {
        private readonly IAccountRoleClaimRepository _repo;
        public AccountRoleClaimService(IAccountRoleClaimRepository repo) {
            _repo = repo;
        }
        public async Task<AccountRoleClaim> Create(AccountRoleClaim o) {
            try {
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<AccountRoleClaim> Delete(AccountRoleClaim o) {
            try {
                var role = await GetById(o.Id);
                await _repo.Remove(role);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<AccountRoleClaim>> GetAll() {
            try {
                var list = await _repo.GetAll(x => x.Role);
                return list.ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<List<AccountRoleClaim>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<AccountRoleClaim> GetById(long id) {
            try {
                return await _repo.GetSingle(x => x.Id == id);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<AccountRoleClaim> Update(AccountRoleClaim o) {
            try {
                var m = await GetById(o.Id);
                await _repo.Update(m);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}
