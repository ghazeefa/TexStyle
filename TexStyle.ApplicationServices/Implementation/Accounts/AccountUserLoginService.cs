using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountUserLoginService : IAccountUserLoginService {

        private readonly IAccountUserLoginRepository _repo;
        public AccountUserLoginService(IAccountUserLoginRepository repo) {
            _repo = repo;
        }
        public async Task<AccountUserLogin> Create(AccountUserLogin o) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserLogin> Delete(AccountUserLogin o) {
            throw new NotImplementedException();
        }

        public async Task<List<AccountUserLogin>> GetAll() {
            throw new NotImplementedException();
        }

        public async Task<List<AccountUserLogin>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserLogin> GetById(long id) {
            throw new NotImplementedException();
        }

        public async Task<AccountUserLogin> Update(AccountUserLogin o) {
            throw new NotImplementedException();
        }
    }
}
