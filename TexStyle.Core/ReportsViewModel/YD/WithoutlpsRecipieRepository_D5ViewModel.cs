using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.YD
{
   public  class WithoutlpsRecipieRepository_D5ViewModel
    {

        public string MachineType { get; set; }
        public string Format { get; set; }
        public decimal Weight { get; set; }

        public decimal LiquorRate { get; set; }
        public DateTime Date { get; set; }
        public decimal No { get; set; }
        public decimal? Kgs { get; set; }
        public decimal Gpl { get; set; }
        public decimal Percentage { get; set; }
        public decimal? Water { get; set; }
        public long? RecipeStepId { get; set; }
        public decimal Rate { get; set; }
        public decimal? Cost { get; set; }
        public string ItemName { get; set; }
        public string RecipeName { get; set; }
        public bool IsYarn { get; set; }
        public bool IsWithoutLPS { get; set; }

    }
}
