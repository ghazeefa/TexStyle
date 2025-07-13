using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS
{
    public class TrDetailViewModel
    {

        public long Id { get; set; }
        [DisplayName("Qty Dr")]
        public decimal? QtyDr { get; set; }
        [DisplayName("Qty Cr")]
        public decimal? QtyCr { get; set; }
        [Required]
        public decimal? Rate { get; set; }
        public bool IsDr { get; set; }
        public decimal? Packet { get; set; }
        public decimal? AvailableKgs { get; set; }
        public decimal Amount{   get; set;}
        
        public string Remarks {   get; set;}
        [DisplayName("Chemical")]
        public long? ChemicalId { get; set; }
        [DisplayName("Dye")]
        public long? DyeId { get; set; }
        public long? DyeChemicalTrId { get; set; }
        public long? GateTrDetailId { get; set; }
        public long? DyeChemicalXrefDetailTrId { get; set; }
        public long? DrId { get; set; }
        public decimal? DrBalance { get; set; }
        public bool? Status { get; set; }
        //[ForeignKey(nameof(GateTrDetailId))]
        //public virtual GateTrDetail GateTrDetail { get; set; }

        //[ForeignKey(nameof(ChemicalId))]
        //public virtual Chemical Chemical { get; set; }

        //[ForeignKey(nameof(DyeId))]
        //public virtual Dye Dye { get; set; }

        //[ForeignKey(nameof(DyeChemicalTrId))]
        //public virtual DyeChemicalTr DyeChemicalTrs { get; set; }


        //public long? Id { get; set; }
        //[DisplayName("Qty Dr")]
        //public decimal? QtyDr { get; set; }
        //[DisplayName("Qty Cr")]
        //public decimal? QtyCr { get; set; }
        //public decimal Rate { get; set; }
        //public decimal? AvailableKgs { get; set; }
        //public decimal? Packet { get; set; }
        //public decimal Amount { get; set; }
        //[DisplayName("Chemical")]
        //public long? ChemicalId { get; set; }
        //[DisplayName("Dye")]
        //public long? DyeId { get; set; }
        //public long? DyeChemicalTrId { get; set; }
        //public long? DetailId { get; set; }
        ////public long? LoanTakenInTrDetailId { get; set; }
    }
}
