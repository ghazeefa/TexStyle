using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TexStyle.Identity.Extensions.DTO {
    public class AccountRole : IdentityRole<int> {

        public virtual ICollection<AccountUserRole> UserRoles { get; set; }
        public virtual ICollection<AccountRoleClaim> RoleClaims { get; set; }
    }
}
