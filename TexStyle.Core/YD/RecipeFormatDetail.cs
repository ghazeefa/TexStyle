using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.YD
{
   public class RecipeFormatDetail: DefaultEntity
    {
        public long Id { get; set; }
        public long? RecipeFormatHeaderId { get; set; }
        public long Sno { get; set; }
        public long? DyeId { get; set; }
        public long? ChemicalId { get; set; }
        public long? RecipeStepId { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal Gpl { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Percentage { get; set; }
        // public bool? IsDye { get; set; }

        [ForeignKey(nameof(RecipeFormatHeaderId))]
        public virtual RecipeFormatHeader RecipeFormatHeader { get; set; }
        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }
        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }
        [ForeignKey(nameof(RecipeStepId))]
        public virtual RecipeStep RecipeStep { get; set; }

    }
}
