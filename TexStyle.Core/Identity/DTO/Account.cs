using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using TexStyle.Core.Premisis;
using TexStyle.Core.PPC;

namespace TexStyle.Identity.Extensions.DTO {
    public partial class Account : IdentityUser<int> {

        public int? ReportFilterId { get; set; }
        [ForeignKey(nameof(ReportFilterId))]
        public virtual ReportFilter ReportFilter { get; set; }

        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branch { get; set; }

        public bool? IsYarn { get; set; }

        // defaults 
        public virtual ICollection<AccountUserClaim> Claims { get; set; }
        public virtual ICollection<AccountUserLogin> Logins { get; set; }
        public virtual ICollection<AccountUserToken> Tokens { get; set; }
        public virtual ICollection<AccountUserRole> UserRoles { get; set; }

        
    }
}
