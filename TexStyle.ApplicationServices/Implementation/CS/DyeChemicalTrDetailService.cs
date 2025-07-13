using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS
{
    class DyeChemicalTrDetailService : IDyeChemicalTrDetailService
    {
        private IDyeChemicalTrDetailRepository _repo;
        public DyeChemicalTrDetailService(IDyeChemicalTrDetailRepository repo)
        {
            _repo = repo;
        }
        public async Task<DyeChemicalTrDetail> Create(DyeChemicalTrDetail o)
        {
            try
            {
                await _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public DyeChemicalTrDetail SetRate ( long? o)
        //{
        //    try
        //    {
        //        _repo.Update(o);
        //        return o;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public async Task<DyeChemicalTrDetail> Delete(DyeChemicalTrDetail o)
        {
            try
            {
                o.IsDeleted = true;
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DyeChemicalTrDetail>> GetAll()
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false, x => x.Dye, x => x.Chemical);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DyeChemicalTrDetail>> GetAllById(long id)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false &&x.Id==id,  x => x.Dye, x => x.Chemical);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<DyeChemicalTrDetail>> GetAllXrefById(long Id)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.DyeChemicalXrefDetailTrId == Id, x => x.Dye, x => x.Chemical, x => x.DyeChemicalTrs);
                return list.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<decimal> GetUsedKgLoanTakenofGateTr(long id)
        {
            try
            {
                var list = await _repo.GetList(x => x.IsDeleted == false && x.GateTrDetailId == id);

                return list.ToList().Sum(x => x.QtyCr).Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<decimal> GetUsedKgLoanTakenofDyeChemicalTr(long id)
        {
            try
            {
                var list =await _repo.GetList(x => x.IsDeleted == false && x.DyeChemicalXrefDetailTrId == id);
                return list.ToList().Sum(x => x.QtyCr).Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DyeChemicalTrDetail>> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
               var list = await _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date);
                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DyeChemicalTrDetail> GetById(long id)
        {
            try
            {
                //return await _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
                return await _repo.GetSingleById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<DyeChemicalTrDetail> Update(DyeChemicalTrDetail o)
        {
            try
            {
                await _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<long> DyeChemicalUpdateDr(long headerid, decimal? fairprice, long? igprefno, string qtycr, int? trtype, long? invoiceno, long? dtreno, DateTime? invoicedate)
        {
            try
            {
               var id = await _repo.DyeChemicalUpdateDr( headerid,  fairprice,  igprefno,  qtycr,  trtype,  invoiceno,  dtreno,  invoicedate);
               return id;
            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }
    }
}
