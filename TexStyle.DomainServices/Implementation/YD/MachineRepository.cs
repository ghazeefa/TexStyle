using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD
{
    internal class MachineRepository : Repository<Machine>, IMachineRepository
    {
        public MachineRepository(AppDbContext db) : base(db)
        {
        }
    }
}
