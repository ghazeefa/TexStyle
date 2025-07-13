using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
   public  class FabricDyeingWeighDetail_ViewModel
    {
        public string FactoryPo { get; set; }
        public decimal RecipeNo { get; set; }
        public DateTime RecipeDate { get; set; }
        public string Buyer { get; set; }
        public string FabricType { get; set; }
        public string BuyerColor { get; set; }
        public string ProcessName { get; set; }
        public decimal? PPCPlanedKgs { get; set; }
    }
}
