using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD
{
    public class RecipeDetailViewModel
    {
        public int? LotNo { get; set; }
        public long? Id { get; set; }
        public long Sno { get; set; }
        public decimal Gpl { get; set; }
        public decimal Water { get; set; }
        public decimal Percentage { get; set; }
        public decimal Weight { get; set; }
        [DisplayName("Recipe No")]
        public long? RecipeId { get; set; }
        [DisplayName("Dye")]
        public long? DyeId { get; set; }
        [DisplayName("Chemical")]
        public long? ChemicalId { get; set; }
        [DisplayName("RecipeStep")]
        public long? RecipeStepId { get; set; }
    }
}
