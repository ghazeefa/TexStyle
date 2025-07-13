using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.Gate
{
    public class GateIGPDetailViewModel
    {
        public long Id { get; set; }

        public decimal? QtyDr { get; set; }
        public decimal? QtyCr { get; set; }
        public string Description { get; set; }
        public long? DyeId { get; set; }
        public long? ChemicalId { get; set; }
        public long? YarnTypeId { get; set; }
        public long? GateIgpId { get; set; }

    }
}
