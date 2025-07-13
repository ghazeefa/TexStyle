using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TexStyle.Core.YD;
using TexStyle.DomainServices.Interfaces.IYD;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.YD {
    internal class ProcessTypeRepository : Repository<ProcessType>, IProcessTypeRepository {
        public ProcessTypeRepository(AppDbContext db) : base(db) {
        }







    }
}
