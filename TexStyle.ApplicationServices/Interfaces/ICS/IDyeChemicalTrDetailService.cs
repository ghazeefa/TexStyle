using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;
namespace TexStyle.ApplicationServices.Interfaces.ICS
{
   public interface IDyeChemicalTrDetailService : IDefaultService<DyeChemicalTrDetail>
    {
        Task<decimal> GetUsedKgLoanTakenofGateTr(long id);
        Task<List<DyeChemicalTrDetail>> GetAllById(long id);
        Task<List<DyeChemicalTrDetail>> GetAllXrefById(long Id);
        Task<decimal> GetUsedKgLoanTakenofDyeChemicalTr(long id);
        Task<long> DyeChemicalUpdateDr(long headerid, decimal? fairprice, long? igprefno, string qtycr, int? trtype, long? invoiceno, long? dtreno, DateTime? invoicedate);


    }
}
