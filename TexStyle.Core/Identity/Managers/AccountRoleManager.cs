using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Identity.Extensions.Managers {
    public class AccountRoleManager : RoleManager<AccountRole> {
        public AccountRoleManager(IRoleStore<AccountRole> store, IEnumerable<IRoleValidator<AccountRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<AccountRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger) {
        }
    }
}
