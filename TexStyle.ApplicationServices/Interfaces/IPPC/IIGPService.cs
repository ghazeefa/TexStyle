using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Common;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface IIGPService : IDefaultService<InwardGatePass>

    {
        Task<InwardGatePass> CreateFabric (InwardGatePass o);
        Task<InwardGatePass> UpdateFabric(InwardGatePass o);
        Task<List<InwardGatePassDetail>> GetIgpReport(PagingOptions options);
        Task<List<InwardGatePass>> GetIgpRepYarnRecievedReport(ReportFilter filter);
        Task<List<InwardGatePass>> GetBetweenDateRangeFabric(DateTime start, DateTime end);
        Task<InwardGatePass> GetByXreffId (long id);
    }
}
