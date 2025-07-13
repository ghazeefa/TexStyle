using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC
{
 public   class LotMarking :DefaultEntity
    {
        public long Id { get; set; }
        public decimal RollNo { get; set; }
        public decimal NoOfRolls { get; set; }
        public long Kgs { get; set; }
        public DateTime? IssuanceDate { get; set; }
        public bool IsIssued { get; set; }
        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
        [ForeignKey(nameof(PPCPlanningId))]
        public virtual PPCPlanning PPCPlanning { get; set; }
    }
}
