using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.DomainServices.Implementation.PPC
{
    internal class FabricQualityRepository : Repository<FabricQuality>, IFabricQualityRepository
    {
        public FabricQualityRepository(Infrastructure.AppDbContext db) : base(db)
        { }



    }
}
