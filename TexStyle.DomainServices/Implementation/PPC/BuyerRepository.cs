using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC {
    internal class BuyerRepository: Repository<Buyer>, IBuyerRepository {
        public BuyerRepository(AppDbContext db) : base(db) {

        }
    }
}
