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
    class StoreReturnNoteRepository : Repository<StoreReturnNote>, IStoreReturnNoteRepository
    {
        private AppDbContext _db;
        public StoreReturnNoteRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public override StoreReturnNote GetSingle(Func<StoreReturnNote, bool> where, params Expression<Func<StoreReturnNote, object>>[] navigationProperties)
        {
            return _db.StoreReturnNotes
                .Include(x => x.Party)
                .Include(x => x.LocalPurchaseInTr).ThenInclude(x=>x.GateTr)
                .Include(x => x.StoreReturnNoteDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.StoreReturnNoteDetails).ThenInclude(y => y.Dye)
                .Include(x => x.StoreReturnNoteDetails).ThenInclude(y => y.StoreReturnNote)
                .SingleOrDefault(where);
        }

        public override IList<StoreReturnNote> GetList(Func<StoreReturnNote, bool> where, params Expression<Func<StoreReturnNote, object>>[] navigationProperties)
        {
            return _db.StoreReturnNotes
                .Include(x => x.Party)
                .Include(x => x.LocalPurchaseInTr).ThenInclude(x => x.GateTr)
                .Include(x => x.StoreReturnNoteDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.StoreReturnNoteDetails).ThenInclude(y => y.Dye)
                .Include(x => x.StoreReturnNoteDetails).ThenInclude(y => y.StoreReturnNote)
                .Where(where).ToList();
        }
    }
}
