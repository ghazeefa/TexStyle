using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD {
    internal class CostingRepository : Repository<Costing>, ICostingRepository {
        public CostingRepository(AppDbContext db) : base(db) {
        }
    }
}
