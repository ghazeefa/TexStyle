using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.YD.Reports
{
    public class DyeingConsupmtionSummary_D1ViewModel : DefaultViewModel
    {
        public DyeingConsupmtionSummary_D1ViewModel()
        {
            this.DyeingConsumptionSummaryDetail_D1ViewModels = new List<DyeingConsumptionSummaryDetail_D1ViewModel>();
        }


        public string Status { get; set; }

        public List<DyeingConsumptionSummaryDetail_D1ViewModel> DyeingConsumptionSummaryDetail_D1ViewModels { get; set; }
    }
}
