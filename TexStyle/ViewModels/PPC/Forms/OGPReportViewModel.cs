using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC {
    public class OGPReportViewModel {
        public long Id { get; set; }
        public long Lps { get; set; }
        public long LotNo { get; set; }
        public string Description { get; set; }
        public decimal Kgs { get; set; }
        public decimal RateKg { get; set; }
        public decimal Amount { get; set; }
    }
}
