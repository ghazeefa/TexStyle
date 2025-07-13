using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC
{
    public class OGPDetailViewModel
    {
        public bool IsComplete { get; set; }
        public long? Id { get; set; }
        public long Xref { get; set; }
        // public long LotNo { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        public string Description { get; set; }
        public decimal Kgs { get; set; }
        public decimal Rate { get; set; }
        public decimal Bags { get; set; }
        public decimal NoOfRolls { get; set; }
        [DisplayName("Reprocess No")]
        public long? ReprocessId { get; set; }
        [Required]
        [DisplayName("Yarn Type")]
        public long? YarnTypeId { get; set; }

        [Required]
        [DisplayName("Fabric Type")]
        public long? FabricTypesId { get; set; }
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }
        [DisplayName("Buyer Color")]
        public long? BuyerColorId { get; set; }
        [DisplayName("Fabric Quality")]
        public long? FabricQualityId { get; set; }
        [DisplayName("PO")]
        public long? PO { get; set; }
        [DisplayName("OGP")]
        public long? OutwardGatePassId { get; set; }
        [DisplayName("LotNo")]

        public long? LotNo { get; set; }


        [DisplayName("FactoryPo")]
        public long? FactoryPoId { get; set; }

        //public long? ReturnNoteId { get; set; }
        //[ForeignKey(nameof(ReturnNoteId))]
        //public virtual ReturnNote ReturnNote { get; set; }

        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
        [DisplayName("IGPDetailNo")]
        public long? InwardGatePassDetailId { get; set; }

        public decimal AvailableLpsKgs { get; set; }



    }
}
