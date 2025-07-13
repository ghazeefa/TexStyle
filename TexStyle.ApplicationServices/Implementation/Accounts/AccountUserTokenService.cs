using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountUserTokenService : IAccountUserTokenService {
        private readonly IAccountUserTokenRepository _repo;
        public AccountUserTokenService(IAccountUserTokenRepository repo) {
            _repo = repo;
        }
        public async Task<AccountUserToken> Create(AccountUserToken o) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserToken> Delete(AccountUserToken o) {
            throw new NotImplementedException();
        }

        public async Task<List<AccountUserToken>> GetAll() {
            throw new NotImplementedException();
        }

        public async Task<List<AccountUserToken>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserToken> GetById(long id) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserToken> Update(AccountUserToken o) {
            throw new NotImplementedException();
        }
    }
}
