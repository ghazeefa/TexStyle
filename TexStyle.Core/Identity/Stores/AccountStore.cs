using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TexStyle.Identity.Extensions.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TexStyle.Identity.Extensions.Stores {
    public class AccountStore : UserStore<Account, AccountRole, DbContext, int> {
        public AccountStore(DbContext context, IdentityErrorDescriber describer = null) : base(context, describer) {
        }
    }
}
