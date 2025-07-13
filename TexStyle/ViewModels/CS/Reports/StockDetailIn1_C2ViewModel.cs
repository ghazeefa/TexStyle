using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.ViewModels.PPC.Reports;

namespace TexStyle.ViewModels.CS.Reports
{
    public class StockDetailIn1_C2ViewModel:DefaultViewModel
    {
        public DateTime? OpeningDate { get; set; }
        public long? OpeningNo { get; set; }
        public decimal? OpeningQty { get; set; }
        public DateTime? LCImportDate { get; set; }
        public long? LCImportNo { get; set; }
        public decimal? LCImportQty { get; set; }

        public DateTime? LocalPurchaseDate { get; set; }
        public long? LocalPurchaseNo { get; set; }
        public decimal? LocalPurchaseQty { get; set; }
        
        public DateTime? LoanTakenInDate { get; set; }
        public long? LoanTakenInNo { get; set; }
        public decimal? LoanTakenInQty { get; set; } 
       
        public DateTime? LoanPartyInDate { get; set; }
        public long? LoanPartyInNo { get; set; }
        public decimal? LoanPartyInQty { get; set; }
        
        public DateTime? DilutionDate { get; set; }
        public decimal? DilutionQty { get; set; }
        public long? DilutionNo { get; set; }


        public decimal? LoanReturnRate { get; set; }
        public decimal? DilutionRate { get; set; }
        public decimal? LoanTakenRate { get; set; }
        public decimal? LocalPurchRate { get; set; }
        public decimal? LCRate { get; set; }
    }
}
