using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Identity.Extensions.Stores {
    public class AccountRoleStore : RoleStore<AccountRole, DbContext, int> {
        public AccountRoleStore(DbContext context, IdentityErrorDescriber describer = null) : base(context, describer) {
        }
    }
}
