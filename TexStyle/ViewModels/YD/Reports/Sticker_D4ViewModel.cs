using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD.Reports
{
    public class Sticker_D4ViewModel
    {
        public DateTime Date { get; set; }

        public long LPSId { get; set; }
        public int LotNo { get; set; }
        public Decimal No { get; set; }    
       public Decimal Cons { get; set; }
        public string Yarntype { get; set; }
        public string YarnQuality { get; set; }
        public string Party { get; set; }
        public string Color { get; set; }
    }
}
