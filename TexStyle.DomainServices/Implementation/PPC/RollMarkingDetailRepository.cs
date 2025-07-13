
using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;

namespace TexStyle.DomainServices.Implementation.PPC
{
    internal class RollMarkingDetailRepository : Repository<RollMarkingDetail>, IRollMarkingDetailRepository
    {
        public RollMarkingDetailRepository(Infrastructure.AppDbContext db) : base(db)
        {

        }

    }
}
