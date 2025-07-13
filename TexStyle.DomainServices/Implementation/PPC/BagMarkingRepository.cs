using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.DomainServices.Implementation.PPC
{
   internal class BagMarkingRepository : Repository<BagMarking>, IBagMarkingRepository {
        public BagMarkingRepository(Infrastructure.AppDbContext db) : base(db)
    {
    }
    
    }
}
