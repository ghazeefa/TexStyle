using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.DomainServices.Interfaces.Accounts;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.ApplicationServices.Implementation.Accounts {
    internal class AccountLoginService : IAccountLoginService {
        private readonly IAccountLoginRepository _repo;
        public AccountLoginService(IAccountLoginRepository repo) {
            _repo = repo;
        }
        public async Task<AccountLogin> Create(AccountLogin o) {
            throw new NotImplementedException();
        }

        public async Task<AccountLogin> Delete(AccountLogin o) {
            throw new NotImplementedException();
        }

        public async Task<List<AccountLogin>> GetAll() {
            throw new NotImplementedException();
        }

        public async Task<List<AccountLogin>> GetBetweenDateRange(DateTime start, DateTime end) {
            throw new NotImplementedException();
        }

        public async Task<AccountLogin> GetById(long id) {
            throw new NotImplementedException();
        }

        public async Task<AccountLogin> Update(AccountLogin o) {
            throw new NotImplementedException();
        }
    }
}
