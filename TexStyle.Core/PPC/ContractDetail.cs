using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC
{
   public class ContractDetail: DefaultEntity
    {
        public long Id { get; set; }
        public long? BuyerId { get; set; }
        public long? YarnCountId { get; set; }
        public long? ColorId { get; set; }
        public decimal Quanitity { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
        public long? ContractId { get; set; }


        [ForeignKey(nameof(BuyerId))]
        public virtual Buyer Buyer { get; set; }

        [ForeignKey(nameof(YarnCountId))]
        public virtual YarnType YarnType { get; set; }

        [ForeignKey(nameof(ColorId))]
        public virtual BuyerColor Color { get; set; }

        [ForeignKey(nameof(ContractId))]
        public virtual Contract Contract { get; set; }

    }
}
