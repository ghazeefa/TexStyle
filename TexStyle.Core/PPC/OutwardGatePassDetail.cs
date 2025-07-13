using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC {
    public class OutwardGatePassDetail : DefaultEntity {
        public long Id { get; set; }
      //  public long Xref { get; set; }
       // public long LotNo { get; set; }
        public string Description { get; set; }
        public decimal Kgs { get; set; }
        public decimal Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal Bags { get; set; }
        public decimal? NoOfRolls { get; set; }
        public int? LotNo { get; set; }

        //public long? ReturnNoteId { get; set; }
        //[ForeignKey(nameof(ReturnNoteId))]
        //public virtual ReturnNote ReturnNote { get; set; }

        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
        [ForeignKey(nameof(PPCPlanningId))]
        public virtual PPCPlanning PPCPlanning { get; set; }

        public long? ReprocessId { get; set; }
        [ForeignKey(nameof(ReprocessId))]
        public virtual Reprocess Reprocess { get; set; }


        public long? InwardGatePassDetailId { get; set; }
        [ForeignKey(nameof(InwardGatePassDetailId))]
        public virtual InwardGatePassDetail InwardGatePassDetail { get; set; }



        public long? YarnTypeId { get; set; }
        [ForeignKey(nameof(YarnTypeId))]
        public virtual YarnType YarnType { get; set; }

        public long? FabricTypesId { get; set; }
        [ForeignKey(nameof(FabricTypesId))]
        public virtual FabricTypes FabricTypes { get; set; }

        public long? OutwardGatePassId { get; set; }
        [ForeignKey(nameof(OutwardGatePassId))]
        public virtual OutwardGatePass OutwardGatePass { get; set; }

        [NotMapped] // Used in issue
        public decimal AvailableLpsKgs { get; set; }
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        public bool? IsComplete { get; set; }

        [DisplayName("FactoryPoDetail")]
        public long? FactoryPoDetailId { get; set; }
        [ForeignKey(nameof(FactoryPoDetailId))]
        public virtual FactoryPoDetail FactoryPoDetail { get; set; }

    }
}
