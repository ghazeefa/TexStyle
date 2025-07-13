using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS {
    internal class ChemicalDilutionTrRepository : Repository<ChemicalDilutionTr>, IChemicalDilutionTrRepository
    {
        private AppDbContext _db;
        public ChemicalDilutionTrRepository(AppDbContext db) : base(db) {
            _db = db;
        }

        public override ChemicalDilutionTr GetSingle(Func<ChemicalDilutionTr, bool> where, params Expression<Func<ChemicalDilutionTr, object>>[] navigationProperties)
        {
            return _db.ChemicalDilutionTrs
                .Include(x => x.ChemicalDilutionTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.ChemicalDilutionTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.ChemicalDilutionTrDetails).ThenInclude(y => y.ChemicalDilutionTr)
                .SingleOrDefault(where);
        }

        public override IList<ChemicalDilutionTr> GetList(Func<ChemicalDilutionTr, bool> where, params Expression<Func<ChemicalDilutionTr, object>>[] navigationProperties)
        {
            return _db.ChemicalDilutionTrs
                .Include(x => x.ChemicalDilutionTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.ChemicalDilutionTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.ChemicalDilutionTrDetails).ThenInclude(y => y.ChemicalDilutionTr)
                .Where(where).ToList();
        }
    }
}
