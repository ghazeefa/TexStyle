using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.Gate;

namespace TexStyle.ApplicationServices.Interfaces.IGate
{
    public interface IGateTrService:IDefaultService<GateTr>
    {
        Task<GateTr> CreateBySno(GateTr o);
        Task<GateTr> UpdateFabric(GateTr o);
        Task<GateTr> CreateByActivityType(GateTr o, bool ispurchase, bool isloanin, bool isloanout);
        Task<List<GateTr>> GetAllByActivityStatus(bool ispurchase, bool isloanin, bool isloanout);
        Task<List<GateTr>> GetBetweenDateRangeByActivityStatus(bool ispurchase, bool isloanin, bool isloanout, DateTime start, DateTime end);
        Task<List<GateTr>> GetBetweenDateRangeByActivityTypeId(long activityTypeId, DateTime start, DateTime end);
        Task<List<GateTr>> GetAllByActivityId(long activityid);
        Task<GateTr> GetOGPById(long id);
    }
}
