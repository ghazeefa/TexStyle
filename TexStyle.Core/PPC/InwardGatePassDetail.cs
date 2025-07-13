using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC {
    public class InwardGatePassDetail : DefaultEntity {

        public long Id { get; set; }
        public int Sno { get; set; }

        public string Description { get; set; }
        public string PartyRefNo { get; set; }
        [DisplayName("Lot No")]
        public int LotNo { get; set; }
      


    
        //public decimal GrossWeightInKg { get; set; }
        [DisplayName("Tear Weight (Kg)")]
        public decimal TearWeightInKg { get; set; }
        [DisplayName("Net Weight (Kg)")]
        public decimal NetWeightInKg { get; set; }
        // TODO: Make Net Weight Getter prop with gross wt - tear weight
        //[DisplayName("Net Weight (Kg)")]
        [DisplayName("Gross Weight (Kg)")]
        [NotMapped]
        public decimal GrossWeightInKg
        {
            get
            {
                return NetWeightInKg + TearWeightInKg;
            }
        }

       // public decimal Difference { get; set; }
        [DisplayName("Zero Balance")]
        public bool IsZeroBalance { get; set; }
        [DisplayName("Store Location")]
        public long? StoreLocationId { get; set; }

        [ForeignKey(nameof(StoreLocationId))]
        public virtual StoreLocation StoreLocation { get; set; }

        public decimal Bags { get; set; }
        public string BuyerPO { get; set; }

        [DisplayName("Weight")]
        public long? Weight { get; set; }

        [DisplayName("GSM")]
        public long? GSM { get; set; }

        [DisplayName("Dia")]
        public long? Dia { get; set; }
      // public string StyleNo { get; set; }

        [DisplayName("Yarn Type")]
        public long? YarnTypeId { get; set; }
        [ForeignKey(nameof(YarnTypeId))]
        public virtual YarnType YarnType { get; set; }

        [DisplayName("Yarn Quality")]
        public long? YarnQualityId { get; set; }
        [ForeignKey(nameof(YarnQualityId))]
        public virtual YarnQuality YarnQuality { get; set; }

        [DisplayName("Bag Marking")]
        public long? BagMarkingId { get; set; }
        [ForeignKey(nameof(BagMarkingId))]
        public virtual BagMarking BagMarking { get; set; }

        [DisplayName("Cone Marking")]
        public long? ConeMarkingId { get; set; }
        [ForeignKey(nameof(ConeMarkingId))]
        public virtual ConeMarking ConeMarking { get; set; }

        [DisplayName("Yarn Manufacturer")]
        public long? YarnManufacturerId { get; set; }
        [ForeignKey(nameof(YarnManufacturerId))]
        public virtual YarnManufacturer YarnManufacturer { get; set; }

        public long? InwardGatePassId { get; set; }
        [ForeignKey(nameof(InwardGatePassId))]
        public virtual InwardGatePass InwardGatePass { get; set; }

        [NotMapped] // Used in ppcplaing add
        public decimal AvailableLpsKgs { get; set; }

        public long? FabricTypesId { get; set; }
        [ForeignKey(nameof(FabricTypesId))]
        public virtual FabricTypes FabricTypes { get; set; }

        public long? FabricQualityId { get; set; }
        [ForeignKey(nameof(FabricQualityId))]
        public virtual FabricQuality FabricQuality { get; set; }

        public  long? RollMarkingId { get; set; }
        [ForeignKey(nameof(RollMarkingId))]
        public virtual RollMarking RollMarking { get; set; }
        public decimal? NoOfRolls { get; set; }
        public long? KnitingPartyId { get; set; }
        [ForeignKey(nameof(KnitingPartyId))]
        public virtual knittingParty knittingParty { get; set; }

        [DisplayName("Process Activity Type")]
        public long? ActivityTypeId { get; set; }
        [ForeignKey(nameof(ActivityTypeId))]
        public virtual ActivityType ActivityType { get; set; }
        public string YarnCountOfFabric { get; set; }

    }
}
