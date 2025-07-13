using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS
{
    public class StoreReturnNoteService : IStoreReturnNoteService
    {
        private IStoreReturnNoteRepository _repo;
        public StoreReturnNoteService(IStoreReturnNoteRepository repo)
        {
            _repo = repo;
        }
        public StoreReturnNote Create(StoreReturnNote o)
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

        public StoreReturnNote Delete(StoreReturnNote o)
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

        public List<StoreReturnNote> GetAll()
        {
            try
            {
                return _repo.GetList(x => x.IsDeleted == false, x=>x.StoreReturnNoteDetails).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StoreReturnNote> GetBetweenDateRange(DateTime start, DateTime end)
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

        public StoreReturnNote GetById(long id)
        {
            try
            {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, x => x.StoreReturnNoteDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StoreReturnNote Update(StoreReturnNote o)
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
