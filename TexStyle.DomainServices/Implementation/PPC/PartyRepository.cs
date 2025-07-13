using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.DomainServices.Implementation.PPC
{
    class PartyRepository : Repository<Party>, IPartyRepository
    {
        public PartyRepository(Infrastructure.AppDbContext db) : base(db)
        {

        }
    }
}
