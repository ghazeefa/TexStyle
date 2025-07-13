using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.PPC
{
    public class ReturnNoteViewModel
    {
        [DisplayName("Production No")]
        public long? Id { get; set; }

        [DisplayName("Production Date")]
        public DateTime ReturnDate { get; set; }
        public bool Confirmed { get; set; }
        public long ReturnTypeId { get; set; }
        [DisplayName("Reprocess?")]
        public bool IsReprocessed { get; set; }
        public List<ReturnNoteDetail> ReturnNoteDetails { get; set; }

        /// <summary>
        /// Party Specific Properties
        /// </summary>
        public long? IRNO { get; set; }
        public bool IsReturnToParty { get; set; }

        public int? BranchId { get; set; }
        public int? UserId { get; set; }
        public bool? IsYarn { get; set; }
        public decimal? TotalWeight { get; set; }
        [DisplayName("Lot No")]
        public int? LotNo { get; set; }

        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }
    }
}
