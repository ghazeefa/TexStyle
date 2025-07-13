using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS
{
    public class DyesChemicalOpenningDetailViewModel
    {
        public long Id { get; set; }
        public decimal? QtyDr { get; set; }
        public decimal? QtyCr { get; set; }

        public decimal Rate { get; set; }
        public long? ChemicalId { get; set; }
        public long? DyesChemicalOpenningId { get; set; }
        public long? DyeId { get; set; }

    }
}
