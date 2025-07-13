using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class GetFabricDetailByLotReportViewModel : DefaultViewModel
    {
        public long LPSID { get; set; }
        public string FactoryPo { get; set; }
        public string FabricType { get; set; }
        public string KnittingParty { get; set; }
        public decimal Kgs { get; set; }
        public string Buyer { get; set; }
        public string BuyerColor { get; set; }
        public string PartyName { get; set; }
        public int LotNo { get; set; }
    }
}
