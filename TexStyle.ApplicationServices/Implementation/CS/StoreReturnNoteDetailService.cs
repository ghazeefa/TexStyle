using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS
{
    public class StoreReturnNoteDetailService : IStoreReturnNoteDetailService
    {
        private IStoreReturnNoteDetailRepository _repo;
        public StoreReturnNoteDetailService(IStoreReturnNoteDetailRepository repo)
        {
            _repo = repo;
        }
        public StoreReturnNoteDetail Create(StoreReturnNoteDetail o)
        {
            try
            {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StoreReturnNoteDetail Delete(StoreReturnNoteDetail o)
        {
            try
            {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreReturnNoteDetail> GetAll()
        {
            try
            {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreReturnNoteDetail> GetBetweenDateRange(DateTime start, DateTime end)
        {
            try
            {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StoreReturnNoteDetail GetById(long id)
        {
            try
            {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetUsedKgLoanTaken(long id)
        {
            try
            {
                var list = _repo.GetList(x => x.IsDeleted == false && x.LocalPurchaseInTrDetailId == id).ToList();

                return list.Sum(x => x.QtyCr).Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StoreReturnNoteDetail Update(StoreReturnNoteDetail o)
        {
            try
            {
                _repo.Update(o);
                return o;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
