using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC
{
    public class Party : DefaultEntity
    {
        public Party() 
        {
            PurchaseOrders = new List<PurchaseOrder>();
            Buyers = new List<Buyer>();
        }
        public long Id { get; set; }
        public long SubAccount { get; set; }
        public string Name { get; set; }
        public long ControlAccount { get; set; }
        public bool IsHeader { get; set; }
        public List<PurchaseOrder> PurchaseOrder { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<Buyer> Buyers { get; set; }
        public bool IsCommercial { get; set; }

    }
}
