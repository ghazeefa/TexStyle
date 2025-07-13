using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC
{
    public class BuyerColor : DefaultEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        // public string CostPerKg { get; set; }

        public long BuyerId { get; set; }
        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }
    }
}
