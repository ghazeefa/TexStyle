using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS
{
    class LoanPartyGivenOutTrService : ILoanPartyGivenOutTrService
    {
        private ILoanPartyGivenOutTrRepository _repo;
        public LoanPartyGivenOutTrService(ILoanPartyGivenOutTrRepository repo)
        {
            _repo = repo;
        }
        public LoanPartyGivenOutTr Create(LoanPartyGivenOutTr o)
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

        public LoanPartyGivenOutTr Delete(LoanPartyGivenOutTr o)
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

        public List<LoanPartyGivenOutTr> GetAll()
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

        public List<LoanPartyGivenOutTr> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
   

        public LoanPartyGivenOutTr GetById(long id)
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

        public LoanPartyGivenOutTr Update(LoanPartyGivenOutTr o)
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
