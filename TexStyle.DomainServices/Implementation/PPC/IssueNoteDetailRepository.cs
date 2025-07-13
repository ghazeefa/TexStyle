using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
    internal class IssueNoteDetailRepository : Repository<IssueNoteDetail>, IIssueNoteDetailRepository
    {
        private readonly AppDbContext _db;
        public IssueNoteDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
