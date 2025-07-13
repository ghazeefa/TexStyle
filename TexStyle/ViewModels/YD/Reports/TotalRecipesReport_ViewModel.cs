using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.YD.Reports
{
   public class TotalRecipesReport_ViewModel : DefaultViewModel
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
