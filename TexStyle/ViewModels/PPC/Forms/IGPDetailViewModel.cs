using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC {
    public class IGPDetailViewModel {
        public long? Id { get; set; }
        //public int Sno { get; set; }

        //[DisplayName("Production Date")]
        //public Nullable<DateTime> ProductionDate { get; set; }
        public string Description { get; set; }
        [DisplayName("Party Ref No")]
        public string PartyRefNo { get; set; }
        [DisplayName("Lot No")]
        public int LotNo { get; set; }
        [DisplayName("Buyer PO")]
        [Required(ErrorMessage = "Buyer PO is required.")]
        public string BuyerPO { get; set; }
        [DisplayName("Style No")]
        public string StyleNo { get; set; }

        public decimal Bags { get; set; }
        public decimal Difference { get; set; }

 

        [DisplayName("Gross Kg")]
        public decimal GrossWeightInKg { get; set; }

        [DisplayName("Tear Kg")]
        public decimal TearWeightInKg { get; set; }

        [DisplayName("Net Kg")]
        public decimal NetWeightInKg { get; set; }

        [DisplayName("Zero Balance")]
        public bool IsZeroBalance { get; set; }

        [DisplayName("Store Location")]
        public long? StoreLocationId { get; set; }

        //[DisplayName("Purchase Order")]
        //public long? PurchaseOrderId { get; set; }

        [DisplayName("Yarn Manufacturer")]
        public long? YarnManufacturerId { get; set; }
        [DisplayName("Bag Marking")]
        public long? BagMarkingId { get; set; }
        [DisplayName("Cone Marking")]
        public long? ConeMarkingId { get; set; }
        [DisplayName("Yarn Type")]
        public long? YarnTypeId { get; set; }
        [DisplayName("Yarn Quality")]
        public long? YarnQualityId { get; set; }
        [DisplayName("IGP")]
        public long? InwardGatePassId { get; set; }


        [DisplayName("Fabric Type")]
        public long? FabricTypesId { get; set; }

        [DisplayName("Fabric Quality")]
        public long? FabricQualityId { get; set; }

        [DisplayName("Roll Marking")]
        public long? RollMarkingId { get; set; }

        [DisplayName("Knitter")]
        public long? KnitingPartyId { get; set; }
        [DisplayName("No of Rolls")]
        public decimal? NoOfRolls { get; set; }

        [DisplayName("Weight")]
        public long? Weight { get; set; }

        [DisplayName("GSM")]
        public long? GSM { get; set; }

        [DisplayName("Dia")]
        public long? Dia { get; set; }

        [DisplayName("Process Activity Type")]
        public long? ActivityTypeId { get; set; }

        public string YarnCountOfFabric { get; set; }
        

    }
}
