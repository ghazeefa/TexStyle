using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Premisis;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.PPC {
    public class ReportFilter : DefaultEntity {
        public int Id { get; set; }

        public Nullable<DateTime> DateFrom { get; set; }
        public Nullable<DateTime> DateTo { get; set; }

        public long? YarnTypeId { get; set; }
        [ForeignKey(nameof(YarnTypeId))]
        public virtual YarnType YarnType { get; set; }

        public long? YarnQualityId { get; set; }
        [ForeignKey(nameof(YarnQualityId))]
        public virtual YarnQuality YarnQuality { get; set; }

        public long? FabricTypeId { get; set; }
        [ForeignKey(nameof(FabricTypeId))]
        public virtual FabricTypes FabricTypes { get; set; }
        
        public long? BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }


        public long? BuyerColorId { get; set; }
        [ForeignKey(nameof(BuyerColorId))]
        public virtual BuyerColor BuyerColor { get; set; }


        public long? FabricQualityId { get; set; }
        [ForeignKey(nameof(FabricQualityId))]
        public virtual FabricQuality FabricQuality { get; set; }

        public long? YarnPartyId { get; set; }
        [ForeignKey(nameof(YarnPartyId))]
        public virtual Party YarnParty { get; set; }

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Account User { get; set; }
        public long? AnalysisTypeId { get; set; }

        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }

        public bool? IsYarn { get; set; }
        public long? FactoryPO { get; set; }
        public string BranchName { get; set; }
        public long? LotNo { get; set; }

    }
}
