using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.YD.Reports
{
   public class DyeingRecipeReprocessReport_ViewModel : DefaultViewModel
    {
        public decimal RecipeNo { get; set; }
        public DateTime RecipeDate { get; set; }
        public bool IsReprocessed { get; set; }
        public int LotNo { get; set; }
        public decimal Weight { get; set; }
        public string BuyerColor { get; set; }
        public string BuyerName { get; set; }
        public string MachineName { get; set; }
        public string ShiftName { get; set; }
        public string ReportName { get; set; }

    }
}
