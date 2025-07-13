using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.PPC.Forms
{
    public class RollMarkingViewModel
    {
        public long Id { get; set; } // lps id
        [DisplayName("LPSNO")]
        public long PPCPlanningId { get; set; } // lps id
     
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        public decimal NoOfRolls { get; set; }

        public List<RollMarkingDetail> RollMarkingDetails { get; set; }




    }
}
