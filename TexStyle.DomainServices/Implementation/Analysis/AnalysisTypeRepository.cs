using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.Analysis;
using TexStyle.DomainServices.Interfaces.IAnalysis;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.Analysis
{
    internal class AnalysisTypeRepository:Repository<AnalysisType>,IAnalysisTypeRepository
    {
        public AnalysisTypeRepository(AppDbContext db) : base(db)
        {
        }
    }
}

