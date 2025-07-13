using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC {
    public class IGPReportViewModel {
        public DateTime? IssueDate { get; set; }
        public long Sno { get; set; } // serial
        public string Description { get; set; }
        public decimal Bags { get; set; }
        public decimal NetKgs { get; set; }
    }
}
