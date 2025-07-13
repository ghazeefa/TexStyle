using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.PPC
{
    public class PPCPlanningViewModel
    {
        [DisplayName("LPS No")]
        public long? Id { get; set; }
        public long? IGPNo { get; set; }


        [ForeignKey("BuyerId")]
        public virtual Buyer Buyer
        {
            get;
            set;
        }

        [ForeignKey("BuyerColorId")]
        public virtual BuyerColor BuyerColor
        {
            get;
            set;
        }

        [DisplayName("Lot No")]
        public int LotNo { get; set; }
        [DisplayName("Cones")]
        public int Cones { get; set; }
        [Required]
        [DisplayName("Kgs")]
        public decimal Kgs { get; set; }
        [Required]
        [DisplayName("Issued Date")]
        public DateTime IssuedDate { get; set; }
        [DisplayName("IGP Xreference")]
        public long IGPXref { get; set; }
        [Required]
        [DisplayName("Production Type")]
        public long ProductionTypeId { get; set; }
        [Required]
        [DisplayName("Purchase Order")]
        public long PurchaseOrderId { get; set; }
        [DisplayName("Purchase Order Code")]
        public string FactoryPo { get; set; }

        [Required]
        [DisplayName("Buyer Color")]
        public long BuyerColorId { get; set; }
        //[Required]
        [DisplayName("Yarn Type")]
        public long? YarnTypeId { get; set; }
        [DisplayName("Yarn Qualtity")]
        public long? YarnQualityId { get; set; }
        [DisplayName("Yarn Manufacturer")]
        public long? YarnManufacturerId { get; set; }

        [DisplayName("Fabric Type")]
        public long? FabricTypeId { get; set; }

        [DisplayName("Fabric Quality")]
        public long? FabricQualityId { get; set; }

        public bool IsConfirmed { get; set; }

        [Required]
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }

        [Required]
        [DisplayName("Party")]
        public long? PartyId { get; set; } 
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        public long InwardGatePassDetailId { get; set; }

        public decimal AvailableLpsKgs { get; set; }

        public int? UserId { get; set; }

        [DisplayName("Weight")]
        public long? Weight { get; set; }

        [DisplayName("GSM")]
        public long? GSM { get; set; }

        [DisplayName("Dia")]
        public long? Dia { get; set; }


        [DisplayName("Knitter")]
        public long? KnitingPartyId { get; set; }
        public decimal? NoOfRolls { get; set; }
        [DisplayName("Fabric Type")]
        public long? FabricTypesId { get; set; }
        
        [DisplayName("IsCancel?")]
        public bool IsCancel { get; set; }
        [DisplayName("ID")]
        public int DyeingWOID { get; set; }
        [DisplayName("Detail ID")]
        public int DyeingWODetailID { get; set; }
        public int Pcs { get; set; }
        [DisplayName("IsGarmentPrinting")]
        public bool IsGarmentPrinting { get; set; }
        [DisplayName("IsFabricPrinting")]
        public bool IsFabricPrinting { get; set; }

    }
}
