using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.Core.PPC;
using TexStyle.Core.YD;



namespace TexStyle.Core.Gate
{
  public  class GateTrDetail:DefaultEntity
    {
        public long Id { get; set; }

        public decimal? QtyDr { get; set; }
        public decimal? Bags { get; set; }     
        public decimal? NoOfRolls { get; set; }
        public decimal? QtyCr { get; set; }
        public string Description { get; set; }
        public long? GateXrefDetailId { get; set; }

        [Range(1, 200000)]
        
        public decimal? Rate { get; set; }
        public decimal? Packet { get; set; }
        public long? DyeId { get; set; }
        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }

        public long? ChemicalId { get; set; }
        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }

        public long? YarnTypeId { get; set; }
        [ForeignKey(nameof(YarnTypeId))]
        public virtual YarnType YarnType { get; set; }
        public long? FabricTypeId { get; set; }
        [ForeignKey(nameof(FabricTypeId))]
        public virtual FabricTypes FabricTypes { get; set; }





        public long? GateTrId { get; set; }
        [ForeignKey(nameof(GateTrId))]
        public virtual GateTr GateTr { get; set; }

        public long? OGPGateTrDetailId { get; set; }
        [ForeignKey(nameof(OGPGateTrDetailId))]
        public virtual GateTrDetail OGPGateTrDetail { get; set; }


        public long? OutwardGatePassDetailId { get; set; }
        [ForeignKey(nameof(OutwardGatePassDetailId))]
        public virtual OutwardGatePassDetail OutwardGatePassDetail { get; set; }

        //public long? LoanTakenReturnOutTrDetailId { get; set; }
        //[ForeignKey(nameof(LoanTakenReturnOutTrDetailId))]
        //public virtual LoanTakenReturnOutTrDetail LoanTakenReturnOutTrDetail { get; set; }

        //public long? LoanPartyGivenOutTrDetailId { get; set; }
        //[ForeignKey(nameof(LoanPartyGivenOutTrDetailId))]
        //public virtual LoanPartyGivenOutTrDetail LoanPartyGivenOutTrDetail { get; set; }

        public long? DyeChemicalTrDetailId { get; set; }
        [ForeignKey(nameof(DyeChemicalTrDetailId))]
        public virtual DyeChemicalTrDetail DyeChemicalTrDetail { get; set; }
        public long? BillityNo { get; set; }





    }
}
