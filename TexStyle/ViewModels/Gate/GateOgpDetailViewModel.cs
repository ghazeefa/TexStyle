using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.GATE
{
    public class GateOgpDetailViewModel
    {
        public long? Id { get; set; }
        public long parentId { get; set; }
        public decimal Rate { get; set; }
        public string Description { get; set; }
    }
}
