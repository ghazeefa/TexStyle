using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;


namespace TexStyle.DomainServices.Interfaces.ICS
{
    public interface IDyeChemicalTrDetailRepository : IRepository<DyeChemicalTrDetail>
    {
      Task<long> DyeChemicalUpdateDr(long headerid, decimal? fairprice, long? igprefno, string qtycr, int? trtype, long? invoiceno, long? dtreno, DateTime? invoicedate);
      Task<DyeChemicalTrDetail> GetSingleById(long id);
    }
}
