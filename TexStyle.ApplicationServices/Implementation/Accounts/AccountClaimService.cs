using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountClaimService : IAccountClaimService {

        private readonly IAccountClaimRepository _repo;
        public AccountClaimService(IAccountClaimRepository  repo) {
            _repo = repo;
        }

        public async Task<AccountClaim> Create(AccountClaim o) {
            throw new NotImplementedException();
        }

        public async Task<AccountClaim> Delete(AccountClaim o) {
            throw new NotImplementedException();
        }

        public async Task<List<AccountClaim>> GetAll() {
            throw new NotImplementedException();
        }

        public async Task<List<AccountClaim>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<AccountClaim> GetById(long id) {
            throw new NotImplementedException();
        }

        public async Task<AccountClaim> Update(AccountClaim o) {
            throw new NotImplementedException();
        }
    }
}
