using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC
{
    public class ReturnNoteDetail : DefaultEntity
    {
        public long Id { get; set; }
        public decimal Kgs { get; set; }
        public decimal Bags { get; set; }
        public string Status { get; set; }

        //public long? YarnTypeId { get; set; }
        //[ForeignKey(nameof(YarnTypeId))]
        //public virtual YarnType YarnType { get; set; }

        //public long? YarnQualityId { get; set; }
        //[ForeignKey(nameof(YarnQualityId))]
        //public virtual YarnQuality YarnQuality { get; set; }

        public long ReturnNoteId { get; set; }
        [ForeignKey(nameof(ReturnNoteId))]
        public virtual ReturnNote ReturnNote { get; set; }

        // Winding Specific
        //public long? IssueNoteId { get; set; }

        //[ForeignKey(nameof(IssueNoteId))]
        //public virtual IssueNote IssueNote { get; set; }

        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
        [ForeignKey(nameof(PPCPlanningId))]
        public virtual PPCPlanning PPCPlanning { get; set; }


        public long? StoreLocationId { get; set; }
        [ForeignKey(nameof(StoreLocationId))]
        public virtual StoreLocation StoreLocation { get; set; }

        // Party Specific
        //public long? XRefIGPNO { get; set; }
        ////public int XRefSNo { get; set; }
        //public long? YarnManufacturerId { get; set; }
        //[ForeignKey(nameof(YarnManufacturerId))]
        //public virtual YarnManufacturer YarnManufacturer { get; set; }

        public long? ReceivedQualityStatus { get; set; }
        public long? ReprocessId { get; set; }
        [ForeignKey(nameof(ReprocessId))]
        public virtual Reprocess Reprocess { get; set; }


        public decimal? EcruKgs { get; set; }
        public decimal? NoOfRolls { get; set; }

    }
}
