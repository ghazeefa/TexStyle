using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS
{
    class DyesChemicalOpenningRepository : Repository<DyesChemicalOpenning>, IDyesChemicalOpenningRepository
    {
        private AppDbContext _db;
        public DyesChemicalOpenningRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public override DyesChemicalOpenning GetSingle(Func<DyesChemicalOpenning, bool> where, params Expression<Func<DyesChemicalOpenning, object>>[] navigationProperties)
        {
            return _db.DyesChemicalOpennings
               // .Include(x => x.Party)
                .Include(x => x.DyesChemicalOpenningDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.DyesChemicalOpenningDetails).ThenInclude(y => y.Dye)
                .Include(x => x.DyesChemicalOpenningDetails).ThenInclude(y => y.DyesChemicalOpenning)
                .SingleOrDefault(where);
        }

        public override IList<DyesChemicalOpenning> GetList(Func<DyesChemicalOpenning, bool> where, params Expression<Func<DyesChemicalOpenning, object>>[] navigationProperties)
        {
            return _db.DyesChemicalOpennings
              //  .Include(x => x.Party)
                .Include(x => x.DyesChemicalOpenningDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.DyesChemicalOpenningDetails).ThenInclude(y => y.Dye)
                .Include(x => x.DyesChemicalOpenningDetails).ThenInclude(y => y.DyesChemicalOpenning)
                .Where(where).ToList();
        }

    }
}
