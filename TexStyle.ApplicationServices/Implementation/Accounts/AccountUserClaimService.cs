using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountUserClaimService : IAccountUserClaimService {

        private readonly IAccountUserClaimRepository _repo;
        public AccountUserClaimService(IAccountUserClaimRepository repo) {
            _repo = repo;
        }
        public async Task<AccountUserClaim> Create(AccountUserClaim o) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserClaim> Delete(AccountUserClaim o) {
            throw new NotImplementedException();
        }

        public async Task<List<AccountUserClaim>> GetAll() {
            throw new NotImplementedException();
        }

        public async Task<List<AccountUserClaim>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserClaim> GetById(long id) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserClaim> Update(AccountUserClaim o) {
            throw new NotImplementedException();
        }
    }
}
