using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;

namespace TexStyle.ApplicationServices.Interfaces.ICS
{
   public interface IDyeingEnergyConsumptionService : IDefaultService<DyeingEnergyConsumption>
    {
        Task<DyeingEnergyConsumption> GetEnergyConsumptionBetweenDates(DateTime dateFrom, DateTime dateTo, bool IsYarn);
    }
}
