using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS
{
    class InterUnitOutTrService : IInterUnitOutTrService
    {
        private IInterUnitOutTrRepository _repo;
        private IInterUnitOutTrDetailRepository _iuodetail;
        public InterUnitOutTrService(IInterUnitOutTrRepository repo, IInterUnitOutTrDetailRepository iuodetail )
        {
            _repo = repo;
        }

        public InterUnitOutTr Create(InterUnitOutTr o)
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

      
        public InterUnitOutTr Delete(InterUnitOutTr o)
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

        public List<InterUnitOutTr> GetAll()
        {
            try
            {
                return _repo.GetList(x => x.IsDeleted == false, x=>x.Party).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<InterUnitOutTr> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public InterUnitOutTr GetById(long id)
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

        public InterUnitOutTr Update(InterUnitOutTr o)
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
