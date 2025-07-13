using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC {
    public class Reprocess : DefaultEntity {
        public long Id { get; set; }
     //   public int Count { get; set; } // how many times this item has been Reprocessed
        public decimal Kgs { get; set; }
        public int Cones { get; set; }
        public int? NoOfRolls { get; set; }
        public DateTime Date { get; set; }
        
        public long? PPCPlanningId { get; set; }
        [ForeignKey(nameof(PPCPlanningId))]
        public virtual PPCPlanning PPCPlanning { get; set; }


        public long? InwardGatePassDetailId { get; set; }
        [ForeignKey(nameof(InwardGatePassDetailId))]
        public virtual InwardGatePassDetail InwardGatePassDetail { get; set; }

        [NotMapped] // Used in Issue/Return note add for reprocess case
        public decimal AvailableLpsKgs { get; set; }
        public bool? IsYarn { get; set; }

    }

}
