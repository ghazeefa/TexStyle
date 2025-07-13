using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Premisis;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.PPC {
    public class PurchaseOrder : DefaultEntity {
        public long Id { get; set; }

        public long Po { get; set; }
        public bool IsClosed { get; set; }

        public long? YarnTypeId { get; set; }
        public long? YarnQualityId { get; set; }
        public long? BuyerColorId { get; set; }
        public long? SeasonId { get; set; }

        [ForeignKey(nameof(YarnTypeId))]
        public virtual YarnType YarnType { get; set; }

      

     

        [ForeignKey(nameof(YarnQualityId))]
        public virtual YarnQuality YarnQuality { get; set; }
        [ForeignKey(nameof(BuyerColorId))]
        public virtual BuyerColor BuyerColor { get; set; }
        [ForeignKey(nameof(SeasonId))]
        public virtual Season Season { get; set; }

        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Account Account { get; set; }

        public long? FabricTypeId { get; set; }
        [ForeignKey(nameof(FabricTypeId))]
        public virtual FabricTypes FabricTypes { get; set; }

        public long? FabricQualityId { get; set; }
        [ForeignKey(nameof(FabricQualityId))]
        public virtual FabricQuality FabricQuality { get; set; }
        public bool? IsYarn { get; set; }

    }
}
