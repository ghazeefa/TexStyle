using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    class LoanPartyGivenOutTrDetailService : ILoanPartyGivenOutTrDetailService {
        private ILoanPartyGivenOutTrDetailRepository _repo;
        public LoanPartyGivenOutTrDetailService(ILoanPartyGivenOutTrDetailRepository repo) {
            _repo = repo;
        }
        public LoanPartyGivenOutTrDetail Create(LoanPartyGivenOutTrDetail o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }


        public LoanPartyGivenOutTrDetail Delete(LoanPartyGivenOutTrDetail o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanPartyGivenOutTrDetail> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanPartyGivenOutTrDetail> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LoanPartyGivenOutTrDetail GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LoanPartyGivenOutTrDetail Update(LoanPartyGivenOutTrDetail o) {
            try {
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }

        }
    }
}
