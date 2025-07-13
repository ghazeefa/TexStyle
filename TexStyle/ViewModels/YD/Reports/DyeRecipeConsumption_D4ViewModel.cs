using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.YD.Reports
{
    public class DyeRecipeConsumption_D4ViewModel : DefaultViewModel
    {
   
        public DyeRecipeConsumption_D4ViewModel()
        {
            this.DyeRecipeConsumptionDetail_D4ViewModel = new List<DyeRecipeConsumptionDetail_D4ViewModel>();
        }


        public string Status { get; set; }

        public List<DyeRecipeConsumptionDetail_D4ViewModel> DyeRecipeConsumptionDetail_D4ViewModel { get; set; }
    }
}