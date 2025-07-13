using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
   public  class ChemicalIssuanceDetail_ViewModel
    {
        public string ChemicalORDyes { get; set; }
        public decimal RecipeNo { get; set; }
        public DateTime RecipeDate { get; set; }
        public string ItemName { get; set; }
        public decimal Weight { get; set; }
    }
}
