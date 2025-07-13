using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.Premisis
{
    public class BranchVouchers : DefaultEntity
    {

        public int Id { get; set; }
       

       

        public int? ModuleId { get; set; }
        [ForeignKey(nameof(ModuleId))]
        public virtual Module Module { get; set; }

        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }

        public int? VoucherId { get; set; }
        [ForeignKey(nameof(VoucherId))]
        public virtual Voucher Voucher { get; set; }

    }
}
