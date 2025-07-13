using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC {
    internal class ReportFilterRepository : Repository<ReportFilter>, IReportFilterRepository {
        public ReportFilterRepository(AppDbContext db) : base(db) {
        }
    }
}
