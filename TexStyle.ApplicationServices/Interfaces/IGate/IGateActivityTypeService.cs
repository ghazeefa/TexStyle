using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.Gate;

namespace TexStyle.ApplicationServices.Interfaces.IGate {
    public interface IGateActivityTypeService : IDefaultService<GateActivityType> {
        Task<GateActivityType> GetByName(string name);
        Task<List<GateActivityType>> GetAllByStatus(bool ispurchaseactivity, bool isloaninactivity, bool isloanoutactivity);
    }
}
