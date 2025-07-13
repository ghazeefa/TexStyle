using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.YD.Reports
{
  


    public class DyeingDetailConsumption_ViewModel : DefaultViewModel
    {
        public DyeingDetailConsumption_ViewModel()
        {
            this.DyeingDetailConsumptionDetail_ViewModel = new List<DyeingDetailConsumptionDetail_ViewModel>();
        }

        public int Status { get; set; }

        public List<DyeingDetailConsumptionDetail_ViewModel> DyeingDetailConsumptionDetail_ViewModel { get; set; }

    }
}


