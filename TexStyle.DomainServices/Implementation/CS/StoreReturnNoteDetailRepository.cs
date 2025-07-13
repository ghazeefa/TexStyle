using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.CS
{
    class StoreReturnNoteDetailRepository : Repository<StoreReturnNoteDetail>, IStoreReturnNoteDetailRepository
    {
        public StoreReturnNoteDetailRepository(AppDbContext db) : base(db)
        {
        }
    }
}
