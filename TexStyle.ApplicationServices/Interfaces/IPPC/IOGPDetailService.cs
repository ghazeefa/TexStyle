using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface IOGPDetailService : IDefaultService<OutwardGatePassDetail>
    {
        Task<decimal> GetByKgSumbyLPSNo(long id);
        Task<decimal> GetBybagsSumbyLPSNo(long id);
        Task<List<OutwardGatePassDetail>> GetByIGPDetailId(long id);
        Task<List<OutwardGatePassDetail>> GetIgpRepYarnRecievedReport(ReportFilter filter);
    }
}
