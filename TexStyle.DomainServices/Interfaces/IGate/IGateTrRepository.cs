using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.Gate;

namespace TexStyle.DomainServices.Interfaces.IGate
{
   public interface IGateTrRepository : IRepository<GateTr>
    {
        Task AddByNewSno(GateTr o);
        Task AddByNewActivityType(GateTr o, bool ispurchase, bool isloanin, bool isloanout);
        Task<IList<GateTr>> GetListForActivityStatus(bool isdeleted, bool ispurchase, bool isloaninactivity, bool isloanoutactivity, params Expression<Func<GateTr, object>>[] navigationProperties);
    }
}
