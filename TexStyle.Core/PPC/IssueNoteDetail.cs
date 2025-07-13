using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC
{
   public class IssueNoteDetail : DefaultEntity
    {
        [Key]
        public long Id { get; set; }

        // LPS TABLE NAVIGATION
        public decimal Kgs { get; set; }
        public decimal Bags { get; set; }
      


        //public long? IssueNoteId { get; set; }


        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
        [ForeignKey(nameof(PPCPlanningId))]
        public virtual PPCPlanning PPCPlanning { get; set; }


        public long? StoreLocationId { get; set; }
        [ForeignKey(nameof(StoreLocationId))]
        public virtual StoreLocation StoreLocation { get; set; }


        public long IssueNoteId { get; set; }
        [ForeignKey(nameof(IssueNoteId))]
        public virtual IssueNote IssueNote { get; set; }

        public long? ReprocessId { get; set; }
        [ForeignKey(nameof(ReprocessId))]
        public virtual Reprocess Reprocess{ get; set; }

        [NotMapped] // Used in issue
        public decimal AvailableLpsKgs { get; set; }
        //[DisplayName("Yarn Type")]
        //public long? YarnTypeId { get; set; }
        //[ForeignKey(nameof(YarnTypeId))]
        //public virtual YarnType YarnType{ get; set; }

        //[DisplayName("Yarn Quality")]
        //public long? YarnQualityId { get; set; }
        //[ForeignKey(nameof(YarnQualityId))]
        //public virtual YarnQuality YarnQuality { get; set; }


        //[DisplayName("Yarn Manufacturer")]
        //public long? YarnManufacturerId { get; set; }
        //[ForeignKey(nameof(YarnManufacturerId))]
        //public virtual YarnManufacturer YarnManufacturer { get; set; }
    }
}
