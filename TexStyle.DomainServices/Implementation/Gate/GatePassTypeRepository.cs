using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.Gate;
using TexStyle.DomainServices.Interfaces.IGate;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.Gate
{
    class GatePassTypeRepository: Repository<GatePassType>, IGatePassTypeRepository
    {
        public GatePassTypeRepository(AppDbContext db) : base(db)
        {
        }
    }
}




