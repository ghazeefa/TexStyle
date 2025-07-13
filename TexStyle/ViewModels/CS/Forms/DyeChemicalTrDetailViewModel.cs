using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS.Forms
{
    public class DyeChemicalTrDetailViewModel
    {
        public long? Id { get; set; }
        [DisplayName("Qty Dr")]
        public decimal? QtyDr { get; set; }
        [DisplayName("Qty Cr")]
        public decimal? QtyCr { get; set; }
        public decimal Rate { get; set; }
        public decimal? AvailableKgs { get; set; }
        public decimal? Packet { get; set; }
        public decimal Amount { get; set; }
        [DisplayName("Chemical")]
        public long? ChemicalId { get; set; }
        [DisplayName("Dye")]
        public long? DyeId { get; set; }
        public long? DyeChemicalTrId { get; set; }
        public long? DetailId { get; set; }
        public string Remarks { get; set; }
        public long? DrId { get; set; }
        public decimal? DrBalance { get; set; }
    }
}
