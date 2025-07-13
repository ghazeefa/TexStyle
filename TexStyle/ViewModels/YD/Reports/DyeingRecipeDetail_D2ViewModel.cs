using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD.Reports
{
    public class DyeingRecipeDetail_D2ViewModel
    {

        public decimal? Kgs { get; set; }
        public decimal Gpl { get; set; }
        public int LotNo { get; set; }
        public decimal Percentage { get; set; }
        public decimal? CostPerKgDye { get; set; }
        public decimal? CostPerKgChemical { get; set; }
        public decimal? Water { get; set; }
        public long? RecipeStepId { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public decimal Cost { get; set; }
        public int Status  { get; set; }
        public string ItemName { get; set; }
        public string RecipeName { get; set; }


    }
}
