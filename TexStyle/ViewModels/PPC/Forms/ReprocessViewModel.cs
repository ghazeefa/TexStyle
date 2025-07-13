using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC {
    public class ReprocessViewModel {

        [DisplayName("IGP Xreference")]
        public long IGPXref { get; set; }
        public long? Id { get; set; }
        public int Count { get; set; } // how many times this item has been Reprocessed
        public decimal Kgs { get; set; }
        public int Cones { get; set; }
        public int NoOfRolls { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
        public long? InwardGatePassDetailId { get; set; }
        // Used in Issue/Return note add for reprocess case
        public decimal AvailableLpsKgs { get; set; }









    }
}
