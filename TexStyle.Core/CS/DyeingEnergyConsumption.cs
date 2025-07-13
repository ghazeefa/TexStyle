using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.CS
{
    public class DyeingEnergyConsumption:DefaultEntity
    {
        public long? Id { get; set; }
        public DateTime Date { get; set; }
        public decimal? ElectricityCost { get; set; }
        public decimal? GassCost { get; set; }
        public decimal? CoalCost { get; set; }
        public decimal? SalaryCost { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsYarn { get; set; }
       
    }
}
