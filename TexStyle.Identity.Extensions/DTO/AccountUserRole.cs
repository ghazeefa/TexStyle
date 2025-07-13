using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Identity.Extensions.DTO {

    public class AccountUserRole : IdentityUserRole<int> {
        public virtual Account User { get; set; }
        public virtual AccountRole Role { get; set; }
    }
}
