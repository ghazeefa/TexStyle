   using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC {
    public class Buyer : DefaultEntity {
        public Buyer() {
            BuyerColors = new List<BuyerColor>();
            PurchaseOrders = new List<PurchaseOrder>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public long PartyId { get; set; }
        [ForeignKey(nameof(PartyId))]
        public virtual Party Party { get; set; }

        public ICollection<BuyerColor> BuyerColors { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
