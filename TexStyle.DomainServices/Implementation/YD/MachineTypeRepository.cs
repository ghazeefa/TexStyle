using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD
{
    internal class MachineTypeRepository : Repository<MachineType>, IMachineTypeRepository
    {
        public MachineTypeRepository(AppDbContext db) : base(db)
        {
        }
    }
}
