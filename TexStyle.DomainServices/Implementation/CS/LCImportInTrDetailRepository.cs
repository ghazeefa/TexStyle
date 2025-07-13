using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS {
    internal class LCImportInTrDetailRepository : Repository<LCImportInTrDetail>, ILCImportInTrDetailRepository {
        public LCImportInTrDetailRepository(AppDbContext db) : base(db) {
        }
    }
}
