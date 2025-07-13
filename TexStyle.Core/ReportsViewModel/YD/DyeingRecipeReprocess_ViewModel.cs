using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.YD
{
   public class DyeingRecipeReprocess_ViewModel
    {
        public decimal RecipeNo { get; set; }
        public DateTime RecipeDate { get; set; }
        public bool IsReprocessed { get; set; }
        public int LotNo { get; set; }
        public decimal Weight { get; set; }
        public string BuyerColor { get; set; }
        public string BuyerName { get; set; }
        public string MachineName { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string ShiftName { get; set; }

    }
}
