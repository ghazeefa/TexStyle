using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD {
    internal class ChemicalRepository : Repository<Chemical>, IChemicalRepository {
        public ChemicalRepository(AppDbContext db) : base(db) {
        }
    }
}
