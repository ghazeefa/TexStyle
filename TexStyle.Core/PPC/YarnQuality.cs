using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.PPC {
    public class YarnQuality : DefaultEntity {
        public YarnQuality() {
            PurchaseOrders = new List<PurchaseOrder>();
        }
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
