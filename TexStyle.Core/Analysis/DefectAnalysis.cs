using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.Analysis
{
    public class DefectAnalysis:DefaultEntity
    {
        public long Id { get; set; }
        public string NoOfDefects { get; set; }
        public long AnalysisTypeID { get; set; }
        [ForeignKey(nameof(AnalysisTypeID))]
        public virtual AnalysisType AnalysisType { get; set; }
        public long DefectId { get; set; }
        [ForeignKey(nameof(DefectId))]
        public virtual Defect Defect { get; set; }
    }
}
