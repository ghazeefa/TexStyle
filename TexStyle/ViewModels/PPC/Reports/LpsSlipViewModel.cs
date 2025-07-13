using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports {
    public class LpsSlipViewModel {
        public long LpsNo { get; set; }
        public long LotNo { get; set; }
        public DateTime IssueDate { get; set; }
        public string Activity { get; set; }
        public string FactoryPo { get; set; }
        public string Party { get; set; }
        public string Buyer { get; set; }

    }
}
