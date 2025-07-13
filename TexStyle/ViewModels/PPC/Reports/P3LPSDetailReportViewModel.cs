using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class P3LPSDetailReportViewModel
    {
        public P3LPSDetailReportViewModel()
        {
        //    DailyProductionList = new List<P3DailyProductionReportViewModel>();
        }
        public long? LPSNo{ get; set; }
        public long? PurchaseOrderNo { get; set; }
        public long? LotNo { get; set; }
        public string Buyer { get; set; }
        public string Shade { get; set; }
        public string YarnType { get; set; }
        public string YarnQuality { get; set; }
        public string FabricType { get; set; }
        public string FabricQuality { get; set; }
        public long? GSM { get; set; }
        public long? Dia { get; set; }
        public long? NoOfRolls { get; set; }
        public bool IsYarn { get; set; }
        public bool IsIssued { get; set; }


        public string Status { get; set; }
        //public decimal? OgpKgs { get; set; }
        //public decimal? RNKgs { get; set; }
        //public DateTime? ProductionDate { get; set; }
        public DateTime? IssuedDate { get; set; }
        
        public long? Cones { get; set; }
        public decimal? EcruKgs { get; set; }
        public decimal? DyeKgs { get; set; }
        public decimal? DyedBags { get; set; }
        public decimal? Loss { get; set; }
        public string ReceivedQualityStatus { get; set; }
        public string FactoryPo { get; set; }

        //public decimal EcruTotal { get; set; }
        //public decimal ConesTotal { get; set; }
        //public decimal DyedTotal { get; set; }
        //public decimal BagsTotal { get; set; }
        // List<P3DailyProductionReportViewModel> DailyProductionList { get; set; }
    }
}
