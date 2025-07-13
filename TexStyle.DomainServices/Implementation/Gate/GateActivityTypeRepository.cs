using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.DomainServices.Interfaces.IGate;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.Gate
{
    class GateActivityTypeRepository : Repository<GateActivityType>, IGateActivityTypeRepository
    {
        public GateActivityTypeRepository(AppDbContext db) : base(db)
        {
        }
    }
}
