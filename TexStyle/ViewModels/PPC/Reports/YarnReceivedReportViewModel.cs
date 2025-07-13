using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.PPC {
    public class YarnReceivedReportViewModel:DefaultViewModel {
        public YarnReceivedReportViewModel() {
            Items = new List<YarnReceivedReportItemViewModel>();
        }
        public string Descriminator { get; set; }
        public decimal KgSum { get; set; }       
        public decimal SumKg { get; set; }
      
        public bool IsYarn { get; set; }

        public List<YarnReceivedReportItemViewModel> Items { get; set; }
    }
}
