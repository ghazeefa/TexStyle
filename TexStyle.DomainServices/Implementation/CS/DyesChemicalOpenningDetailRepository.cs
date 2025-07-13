using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS
{
    class DyesChemicalOpenningDetailRepository : Repository<DyesChemicalOpenningDetail>, IDyesChemicalOpenningDetailRepository
    {
       // private AppDbContext _db;
        public DyesChemicalOpenningDetailRepository(AppDbContext db) : base(db)
        {
            
        }


    }

}
