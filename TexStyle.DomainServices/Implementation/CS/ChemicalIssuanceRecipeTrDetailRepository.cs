using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS
{
    class ChemicalIssuanceRecipeTrDetailRepository : Repository<ChemicalIssuanceRecipeTrDetail>, IChemicalIssuanceRecipeTrDetailRepository
    {
        public ChemicalIssuanceRecipeTrDetailRepository(AppDbContext db) : base(db)
        {
        }
    }
}
