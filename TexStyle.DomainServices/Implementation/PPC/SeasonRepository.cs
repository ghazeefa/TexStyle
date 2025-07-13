using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
    class SeasonRepository :Repository<Season> ,ISeasonRepository
    {
        public SeasonRepository(AppDbContext db):base(db)
        { }
    }
}
