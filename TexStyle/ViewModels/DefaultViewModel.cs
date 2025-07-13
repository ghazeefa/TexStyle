using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Reports
{
    
    public class DefaultViewModel
    {
        public string CompanyName { get; set; }= "Company Name";
        public string CompanyBranch { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

    }
}
