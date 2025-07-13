using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.YD
{
   public class RecipeDetail : DefaultEntity
    {
        public long Id { get; set; }
        public long? RecipeId { get; set; }
        public long Sno { get; set; }
        public long? DyeId { get; set; }
        public long? ChemicalId { get; set; }
        public long? RecipeStepId { get; set; }
        public int? LotNo { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Gpl { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal Percentage { get; set; }
        [Column(TypeName = "decimal(18, 6)")]
        public decimal? Weight { get; set; }

        [NotMapped]
        public decimal Water
        {
            get
            {
                return Recipe.Weight * Recipe.LiquorRate;

            }
        }


        [ForeignKey(nameof(RecipeId))]
        public virtual Recipe Recipe { get; set; }
        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }
        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }
        [ForeignKey(nameof(RecipeStepId))]
        public virtual RecipeStep RecipeStep { get; set; }
    }
}
