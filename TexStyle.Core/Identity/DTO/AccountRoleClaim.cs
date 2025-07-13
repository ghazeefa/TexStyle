using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Identity.Extensions.DTO {
    public class AccountRoleClaim : IdentityRoleClaim<int> {
        public virtual AccountRole Role { get; set; }
    }
}
