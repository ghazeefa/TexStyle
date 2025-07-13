using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
  public  class GetFabricDetailByLot
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
