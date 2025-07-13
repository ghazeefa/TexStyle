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
    class ChemicalIssuanceRecipeTrRepository : Repository<ChemicalIssuanceRecipeTr>, IChemicalIssuanceRecipeTrRepository
    {
       private AppDbContext _db;
        public ChemicalIssuanceRecipeTrRepository(AppDbContext db) : base(db)
        {
         _db = db;
        }

        public override ChemicalIssuanceRecipeTr GetSingle(Func<ChemicalIssuanceRecipeTr, bool> where, params Expression<Func<ChemicalIssuanceRecipeTr, object>>[] navigationProperties)
        {
            return _db.ChemicalIssuanceRecipeTrs
                .Include(x => x.ChemicalIssuanceRecipeTrDetail).ThenInclude(y => y.Chemical)
                .Include(x => x.ChemicalIssuanceRecipeTrDetail).ThenInclude(y => y.Dye)
                .Include(x => x.ChemicalIssuanceRecipeTrDetail).ThenInclude(y => y.ChemicalIssuanceRecipeTr)
                .SingleOrDefault(where);
        }

        public override IList<ChemicalIssuanceRecipeTr> GetList(Func<ChemicalIssuanceRecipeTr, bool> where, params Expression<Func<ChemicalIssuanceRecipeTr, object>>[] navigationProperties)
        {
            return _db.ChemicalIssuanceRecipeTrs
                .Include(x => x.ChemicalIssuanceRecipeTrDetail).ThenInclude(y => y.Chemical)
                .Include(x => x.ChemicalIssuanceRecipeTrDetail).ThenInclude(y => y.Dye)
                .Include(x => x.ChemicalIssuanceRecipeTrDetail).ThenInclude(y => y.ChemicalIssuanceRecipeTr)
                .Where(where).ToList();
        }
    }
}
