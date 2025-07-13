using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD
{
    public class RecipeFormatHeaderViewModel
    {
        [DisplayName("Format No")]
        public long Id { get; set; }
        [DisplayName("Format Name")]
        public string Name { get; set; }
        [DisplayName("Liquor Ratio")]
        public decimal LiquorRate { get; set; }
        [DisplayName("Process Type")]
        public long? ProcessTypeId { get; set; }
        [DisplayName("Yarn")]
        public bool IsYarn { get; set; }
        [DisplayName("Fabric?")]
        public bool IsFabric { get; set; }

    }
}
