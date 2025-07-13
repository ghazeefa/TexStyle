using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.YD
{
   public  class TotalRecipes_ViewModel
    {
        public string FactoryPo { get; set; }
        public decimal RecipeNo { get; set; }
        public DateTime RecipeDate { get; set; }
        public string Buyer { get; set; }
        public long? BuyerId { get; set; }
        public string BuyerColor { get; set; }
        public long? BuyerColorId { get; set; }
        public decimal? PPCPlanedKgs { get; set; }
        public int? Pcs { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
