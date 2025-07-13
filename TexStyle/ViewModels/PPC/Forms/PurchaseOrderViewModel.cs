using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC
{
    public class PurchaseOrderViewModel
    {
        public long Id { get; set; }
        [DisplayName("Purchase Order")]
        public long Po { get; set; }
        [DisplayName("Is Closed?")]
        public bool IsClosed { get; set; }


        [DisplayName("Yarn Type")]
        public long ? YarnTypeId { get; set; }

        [DisplayName("Yarn Quality")]
        public long ? YarnQualityId { get; set; }

        [DisplayName("Fabric Type")]
        public long ? FabricTypeId { get; set; }

        [DisplayName("Fabric Quality")]
        public long ? FabricQualityId { get; set; }


        [Required]
        [DisplayName("Buyer Color")]
        public long BuyerColorId { get; set; }
        [DisplayName("Season")]
        public long SeasonId { get; set; }

        [DisplayName("Party")]
        public long PartyId { get; set; }
        [DisplayName("Buyer")]
        public long BuyerId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? BranchId { get; set; }
        public int? UserId { get; set; }
        public bool? IsYarn { get; set; }
    }
}
