using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports {
    public class YarnReceivedReportItemViewModel {
        

        public YarnReceivedReportItemViewModel() {
            PPCPlaningList = new List<YarnReceivedReportPPCPlaningModel>();
        }
        public long No { get; set; }
        public string Knitter { get; set; }
        public string FabricType { get; set; }
       
        public string YarnType { get; set; }
        public string YarnQuality { get; set; }
        public string Party { get; set; }
        public string YarnManufacturer { get; set; }
        public string Po { get; set; }
        public string Shade { get; set; }
        public string Buyer { get; set; }
        public decimal Bags { get; set; }
        public long LotNo { get; set; }
        public long LPSId { get; set; }
        public decimal Kgs { get; set; }
        public decimal NetKgs { get; set; }
        public DateTime? Date { get; set; }
        public bool IsDateWise { get; set; }
        public bool IsYarn { get; set; }
        public string Description { get; set; }
        public decimal BalanceKgs { get; set; }
        public decimal TearWeightInKg { get; set; }
        public List<YarnReceivedReportPPCPlaningModel> PPCPlaningList{ get; set; }
        
    }
}
