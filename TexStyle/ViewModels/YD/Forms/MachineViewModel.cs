using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD
{
    public class MachineViewModel
    {
        [DisplayName("No")]
        public long? Id { get; set; }
        public int Chambers { get; set; }
        [DisplayName("Reel Speed")]
        public int ReelSpeed { get; set; }
        [DisplayName("Machine Type")]
        public long MachineTypeId { get; set; }
    }
} 
