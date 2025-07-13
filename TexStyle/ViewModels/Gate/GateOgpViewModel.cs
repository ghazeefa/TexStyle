using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ViewModels.GATE
{
    public class GateOgpViewModel
    {
        public long? Id { get; set; }
        public DateTime OgpDate { get; set; }
        public bool IsReprocessed { get; set; }
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public string NICNo { get; set; }
        public string EmpNo { get; set; }
        public long Xref { get; set; }

        public long? PartyId { get; set; }
        public Party Party { get; set; }


        public long? YarnTypeId { get; set; }

        public long? GateActivityTypeId { get; set; }
    }
}
