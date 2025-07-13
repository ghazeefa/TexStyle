using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class P3DailyProductionReportViewModel:DefaultViewModel
    {
        public P3DailyProductionReportViewModel()
        {
            LPSDetail = new List<P3LPSDetailReportViewModel>();
        }

        
        public string Date { get; set; }
        public string Buyer { get; set; }
        public decimal? OgpKgs { get; set; }
        public decimal RNKgs { get; set; }
        public int Status { get; set; }
        public int? LotNo { get; set; }
        public decimal KgSum { get; set; }
        public decimal DayedKgSum { get; set; }
        public decimal DayedBagsSum { get; set; }        
        public decimal Loss { get; set; }        
       public  decimal TotalWeight { get; set; }       
       public  decimal TotalKgs { get; set; }       
       public  decimal TotalSum { get; set; }       
       public  decimal Total { get; set; }       
        public bool Type { get; set; }        


        public List<P3LPSDetailReportViewModel> LPSDetail { get; set; }
   
    }
}
