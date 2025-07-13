using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports {
    public class YarnReceivedReportPPCPlaningModel {
        public decimal LPSNo { get; set; }
        public decimal Kgs { get; set; } 
        public decimal RemainingKgs { get; set; }
        public int Cones{ get; set; }
        public long IssueId { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
