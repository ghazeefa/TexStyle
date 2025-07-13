using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.Analysis;
using TexStyle.DomainServices.Interfaces.IAnalysis;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.Analysis
{
    internal class DefectAnalysisRepository:Repository<DefectAnalysis>,IDefectAnalysisRepository
    {
        public DefectAnalysisRepository(AppDbContext db) : base(db)
        {
        }
    }
}
