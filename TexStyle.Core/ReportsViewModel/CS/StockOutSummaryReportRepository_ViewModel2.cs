using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.CS
{
   public class StockOutSummaryReportRepository_ViewModel2
    {
        public decimal No { get; set; }
        public string UserName { get; set; }
        public decimal QtyCr { get; set; }
        public decimal Rate { get; set; }
        public string machinename { get; set; }
        public string color { get; set; }
        public long LPSId { get; set; }
        public decimal Weight { get; set; }
        public decimal RatePerKG { get; set; }
        public decimal? Capacity { get; set; }
        public DateTime? Date { get; set; }
    }
}
