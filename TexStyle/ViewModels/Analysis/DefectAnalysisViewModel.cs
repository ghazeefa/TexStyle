using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.Analysis
{
    public class DefectAnalysisViewModel
    {
        public long? Id { get; set; }
        public string NoOfDefects { get; set; }
        public long AnalysisTypeID { get; set; }
        public long DefectId { get; set; }
    }
}
