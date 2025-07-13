using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC
{
    public class IssueNoteDetailViewModel
    {
        [DisplayName("Receive Id")]
        public long? Id { get; set; }
        public long LPSNO { get; set; }
        public decimal Kgs { get; set; }
        public decimal Bags { get; set; }
        //public long? YarnTypeId { get; set; }
        //public long? YarnManufacturerId { get; set; }
        //public long? YarnQualityId { get; set; }
        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
   

        [Required]
        public long? StoreLocationId { get; set; }
        public long IssueNoteId { get; set; }
        public long? ReprocessId { get; set; }
        public decimal AvailableKgs { get; set; }
    }
}
