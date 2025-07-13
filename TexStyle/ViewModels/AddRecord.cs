using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels
{
    public class AddRecord
    {

        public long id { get; set; }
        public long ProductionTypeId { get; set; }
        public decimal QtyDr { get; set; }
        public long DetailId { get; set; }
        public long fabrictypeid { get; set; }
        public long partyid { get; set; }
        public long buyerid { get; set; }
        public long fabricqualityid { get; set; }
        public long knittingpartyid { get; set; }
        public int LotNo { get; set; }
        public long PurchaseOrderId { get; set; }
        public long BuyerColorId { get; set; }
        public bool IsConfirmed { get; set; }
        public long GSM { get; set; }
        public long Dia { get; set; }
        public string FactoryPo { get; set; }
        public DateTime IssuedDate { get; set; }
        public int DyeingWOID { get; set; }
        public int DyeingWODetailID { get; set; }




    }
}
