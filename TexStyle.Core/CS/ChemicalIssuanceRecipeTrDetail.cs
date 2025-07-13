using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.YD;

namespace TexStyle.Core.CS
{
    public class ChemicalIssuanceRecipeTrDetail:DefaultEntity
    {
        public ChemicalIssuanceRecipeTrDetail()
        {
            IsIssued = false;

        }
        public long Id { get; set; }
        public long ChemicalIssuanceRecipeTrId { get; set; }
        public long? RecipeDetailId { get; set; }
        public long? ChemicalId { get; set; }
        public long? DyeId { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Weight { get; set; }
        public decimal Rate { get; set; }

        [NotMapped]
        public decimal Amount
        {
            get
            {
               return Weight * Rate;

            }
        }
        public bool IsIssued { get; set; }

        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }
        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }
        [ForeignKey(nameof(ChemicalIssuanceRecipeTrId))]
        public virtual ChemicalIssuanceRecipeTr ChemicalIssuanceRecipeTr  { get; set; }

        [ForeignKey(nameof(RecipeDetailId))]
        public virtual RecipeDetail RecipeDetail { get; set; }
    }
}
