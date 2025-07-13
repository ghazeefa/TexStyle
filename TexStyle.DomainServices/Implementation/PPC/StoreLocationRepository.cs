using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.DomainServices.Implementation.PPC
{
    class StoreLocationRepository : Repository<StoreLocation>, IStoreLocationRepository
    {
        public StoreLocationRepository(Infrastructure.AppDbContext db) : base(db)
        {
        }
    
    }
}
