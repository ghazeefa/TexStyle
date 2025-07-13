using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
    internal class IGPRepository : Repository<InwardGatePass>, IIGPRepository
    {
        public IGPRepository(AppDbContext db) : base(db)
        {
        }
    }
}
