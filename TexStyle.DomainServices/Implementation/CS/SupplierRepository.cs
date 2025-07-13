using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS {
    internal class SupplierRepository : Repository<Supplier>, ISupplierRepository {
        public SupplierRepository(AppDbContext db) : base(db) {

        }
    }
}
