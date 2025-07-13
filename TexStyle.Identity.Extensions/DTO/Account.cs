using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TexStyle.Identity.Extensions.DTO {
    public partial class Account : IdentityUser<int> {
        public virtual ICollection<AccountUserClaim> Claims { get; set; }
        public virtual ICollection<AccountUserLogin> Logins { get; set; }
        public virtual ICollection<AccountUserToken> Tokens { get; set; }
        public virtual ICollection<AccountUserRole> UserRoles { get; set; }

    }
}
