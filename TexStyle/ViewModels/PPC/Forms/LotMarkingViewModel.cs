using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Forms
{
    public class LotMarkingViewModel
    {
        public long Id { get; set; }
        public decimal RollNo { get; set; }
        public decimal NoOfRolls { get; set; }
        public long Kgs { get; set; }
        public DateTime IssuanceDate { get; set; }

        public Boolean IsIssued { get; set; }
        [DisplayName("LPS No")]
        public long? PPCPlanningId { get; set; }
     



    }
}
