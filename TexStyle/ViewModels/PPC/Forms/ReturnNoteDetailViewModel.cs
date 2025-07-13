using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC
{
    public class ReturnNoteDetailViewModel
    {
        public long? Id { get; set; }
        [DisplayName("Issue Id")]
        public long? IssueNoteId { get; set; }
        //public long ReturnTypeId { get; set; }
        public decimal Kgs { get; set; }
        public decimal EcurKgs { get; set; }
        public decimal NoOfRolls { get; set; }

        public decimal Bags { get; set; }

        public string Status { get; set; } 

        //[DisplayName("Yarn Type")]
        //public long? YarnTypeId { get; set; }
        //[DisplayName("Yarn Quality")]
        //public long? YarnQualityId { get; set; }
        [DisplayName("Return Id")]
        public long? ReturnNoteId { get; set; }

        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }

        [Required]
        [DisplayName("Store Location")]
        public long? StoreLocationId { get; set; }
        [DisplayName("Reprocess No")]
        public long? ReprocessId { get; set; }

        public decimal AvailableLpsKgs { get; set; }
    }
}
