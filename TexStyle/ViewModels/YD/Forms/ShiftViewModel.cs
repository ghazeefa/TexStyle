using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.Core.YD;

namespace TexStyle.ViewModels.YD
{
    public class ShiftViewModel
    {
        public int Id { get; set; }
        public string ShiftName { get; set; }

    }
}
