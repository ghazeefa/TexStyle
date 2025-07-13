using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.Core.YD;

namespace TexStyle.Core.CS
{
   public  class DyeChemicalTrDetail:DefaultEntity
    {
        public long Id { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? QtyDr { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? QtyCr { get; set; }
        public decimal Rate { get; set; }
        public decimal? Packet { get; set; }
        public string Remarks { get; set; }
        //public string Description { get; set; }

        [NotMapped]
        public decimal Amount
        {
            get
            {
                if (QtyDr.HasValue)
                {
                    return QtyDr.Value * Rate;
                }
                else if (QtyCr.HasValue)
                {
                    return QtyCr.Value * Rate;
                }

                return 0;
            }
        }
        public bool? Status { get; set; }
        public bool IsDr { get; set; }
        public long? ChemicalId { get; set; }
        public long? DyeChemicalTrId { get; set; }
        public long? DyeId { get; set; }
        public long? GateTrDetailId { get; set; }

        [ForeignKey(nameof(GateTrDetailId))]
        public virtual GateTrDetail GateTrDetail { get; set; }

        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }

        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }

        [ForeignKey(nameof(DyeChemicalTrId))]
        public virtual DyeChemicalTr DyeChemicalTrs { get; set; }

        public long? DyeChemicalXrefDetailTrId { get; set; }
        [ForeignKey(nameof(DyeChemicalXrefDetailTrId))]
        public virtual DyeChemicalTrDetail DyeChemicalXrefDetailTr { get; set; }
        public long? RecipeDetailId { get; set; }

        [ForeignKey(nameof(RecipeDetailId))]
        public virtual RecipeDetail RecipeDetail { get; set; }
        public bool? IsIssued { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? FinalQty { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? FinalAmount { get; set; }
        public long? DrId { get; set; }
        public decimal? DrBalance { get; set; }
    }
}
