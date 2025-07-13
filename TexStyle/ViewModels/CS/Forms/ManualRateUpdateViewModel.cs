using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.CS.Forms
{
    public class ManualRateUpdateViewModel
    {


        public DateTime Date { get; set; }
        public string Name { get; set; }
        public long? Sno { get; set; }
        public long? Id { get; set; }
        public decimal? QtyDr { get; set; }
        public decimal? TakenRate { get; set; }
        public long? TrType { get; set; }
    }
}
