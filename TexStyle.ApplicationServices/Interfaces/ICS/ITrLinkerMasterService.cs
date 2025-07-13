using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.Core.CS;

namespace TexStyle.ApplicationServices.Interfaces.ICS
{
   public interface ITrLinkerMasterService : IDefaultService<TrLinkerMaster>
    {
        void UpdateRate(DateTime Date);
        List<TrLinkerMaster> GetbetweenDates(DateTime DateFrom, DateTime DateTo);
        //List<TrLinkerMaster> GetQtybyItem(long? dyeId, long? chemicalId);
        //   TrLinkerMaster UpdateTrMaster(long? dyeId, long? chemicalId, DateTime? Date, long headerid, long? detailid, long transid);
        //TrLinkerMaster UpdateTrMaster(DateTime date);
        //TrLinkerMaster DeletebyTRType(long headerid, long detailid, long transid);
        // void AddCreditEntry();
    }
}
