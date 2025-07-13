using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    public class P6YarnDespatchedDetailViewModel
    {
        public long No { get; set; }
        public string Party { get; set; }
        public long LpsNo { get; set; }
        public long LotNo { get; set; }
        public string Color { get; set; }
        public string YarnType { get; set; }
        public string YarnQuality { get; set; }
        public decimal NetKgs { get; set; }
        public string Description { get; set; }

    }
}
