using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC {
    internal class BuyerColorRepository : Repository<BuyerColor>, IBuyerColorRepository {
        public BuyerColorRepository(AppDbContext db) : base(db) {
        }
    }
}
