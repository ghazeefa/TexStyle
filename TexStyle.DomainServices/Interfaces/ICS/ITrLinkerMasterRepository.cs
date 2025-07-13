using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;

namespace TexStyle.DomainServices.Interfaces.ICS
{
    public interface ITrLinkerMasterRepository : IRepository<TrLinkerMaster>
    {
        void ExceRateStoreProcedure(DateTime date);
        //List<StockSummaryC1ViewModel> ExceC1StoreProcedure(DateTime date)

     }
}
