using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Premisis;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.PPC {
    public class PPCPlanning : DefaultEntity {
        public PPCPlanning() {
            Reprocesses = new List<Reprocess>();
        }

        [Key]
        public long Id { get; set; } // lps id
        public int LotNo { get; set; }
        public DateTime IssuedDate { get; set; }
        public int Cones { get; set; }
        public decimal Kgs { get; set; }

        [DisplayName("Party PO No")]
        public string PartyPONo { get; set; }
        [DisplayName("Factory Po")]
        public string FactoryPo { get; set; }

        [DisplayName("Style No")]
        public string StyleNo { get; set; }
        public long? ProductionTypeId { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? PartyId { get; set; }
        public long? BuyerId { get; set; }
        public long? BuyerColorId { get; set; }
        public long? YarnTypeId { get; set; }
        public long? YarnQualityId { get; set; }
        public long? YarnManufacturerId { get; set; }
        public bool IsConfirmed { get; set; }
        public long? InwardGatePassDetailId { get; set; }

        //public long? InwardGatePassId { get; set; }
        //[ForeignKey(nameof(InwardGatePassId))]
        //public virtual InwardGatePass InwardGatePass { get; set; }

        [ForeignKey(nameof(ProductionTypeId))]
        public virtual ProductionType ProductionType { get; set; }

        [ForeignKey(nameof(PurchaseOrderId))]
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        [ForeignKey(nameof(InwardGatePassDetailId))]
        public virtual InwardGatePassDetail InwardGatePassDetail { get; set; }

        [ForeignKey(nameof(BuyerColorId))]
        public virtual BuyerColor BuyerColor { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party{ get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }

        [ForeignKey(nameof(YarnTypeId))]
        public virtual YarnType YarnType { get; set; }

        [ForeignKey(nameof(YarnQualityId))]
        public virtual YarnQuality YarnQuality { get; set; }

        [ForeignKey(nameof(YarnManufacturerId))]
        public virtual YarnManufacturer YarnManufacturer { get; set; }

        public ICollection<Reprocess> Reprocesses { get; set; }

        [NotMapped] // Used in Issue/Return note add
        public decimal AvailableLpsKgs { get; set; }

        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branches Branches { get; set; }

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Account Account { get; set; }
        public string Remarks { get; set; }

        public long? FabricTypeId { get; set; }
        [ForeignKey(nameof(FabricTypeId))]
        public virtual FabricTypes FabricTypes { get; set; }

        public long? FabricQualityId { get; set; }
        [ForeignKey(nameof(FabricQualityId))]
        public virtual FabricQuality FabricQuality { get; set; }

        public long? KnitingPartyId { get; set; }
        [ForeignKey(nameof(KnitingPartyId))]
        public virtual knittingParty knittingParty { get; set; }

        [DisplayName("Weight")]
        public long? Weight { get; set; }

        [DisplayName("GSM")]
        public long? GSM { get; set; }


        [DisplayName("Dia")]
        public long? Dia { get; set; }
        public bool? IsYarn { get; set; }
        public decimal? NoOfRolls { get; set; }

        public long? FabricTypesId { get; set; }
        [ForeignKey(nameof(FabricTypesId))]
        public virtual FabricTypes FabricType { get; set; }

        
        [DisplayName("Cancel?")]
        public bool? IsCancel { get; set; }
        public int? DyeingWOID { get; set; }
        public int? DyeingWODetailID { get; set; }

        [DisplayName("Pcs")]
        public int? Pcs { get; set; }

        public bool? IsGarmentPrinting { get; set; }
        public bool? IsFabricPrinting { get; set; }

    }
}
