using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS {
    internal class LocalPurchaseInTrDetailRepository : Repository<LocalPurchaseInTrDetail>, ILocalPurchaseInTrDetailRepository {
        public LocalPurchaseInTrDetailRepository(AppDbContext db) : base(db) {
        }
    }
}
