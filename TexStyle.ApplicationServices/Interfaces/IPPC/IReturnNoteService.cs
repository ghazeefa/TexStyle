using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC
{
    public interface IReturnNoteService : IDefaultService<ReturnNote>
    {
        Task<List<ReturnNote>> GetProductionDetailReport(ReportFilter filter);
        Task<List<ReturnNote>> GetBetweenDateRangeFabric(DateTime start, DateTime end);
        Task<ReturnNote> CreateFabric(ReturnNote o);
        Task<ReturnNote> UpdateFabric(ReturnNote o);
        Task<IList<ReturnNote>> CheckLotNo(long? buyerid, long? lotNo);
    }
}
