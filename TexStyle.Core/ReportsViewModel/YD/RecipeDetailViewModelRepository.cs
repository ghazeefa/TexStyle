using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.YD
{
  public  class RecipeDetailViewModelRepository
    {
     
        public long? Sno { get; set; }
        public long? RecipeId { get; set; }
        public long? RecipeStepId { get; set; }
        public long? ChemicalId { get; set; }
        public long? Id { get; set; }
        public long? DyeId { get; set; }
        public decimal? Gpl { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? Weight { get; set; }


   

        public string Chemical { get; set; }
        public string Dye { get; set; }
        public string RecipeStep { get; set; }



    }
}
