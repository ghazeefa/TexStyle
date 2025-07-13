using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.Analysis;
using TexStyle.DomainServices.Interfaces.IAnalysis;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.Analysis
{
   internal class DefectRepository:Repository<Defect>,IDefectRepository
    {
        public DefectRepository(AppDbContext db) : base(db)
        {
        }
    }
}

