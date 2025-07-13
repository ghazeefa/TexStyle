using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD
{
    public class RecipeFormatDetailViewModel
    {
        public long Id { get; set; }
        public long? RecipeFormatHeaderId { get; set; }
        public long Sno { get; set; }
        [DisplayName("Dye")]
        public long? DyeId { get; set; }
        [DisplayName("Chemical")]
        public long? ChemicalId { get; set; }
        [DisplayName("Recipe Step")]
        public long? RecipeStepId { get; set; }
        public decimal Gpl { get; set; }
        public decimal Percentage { get; set; }
    }
}
