using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    internal class LoanPartyReturnInTrDetailService : ILoanPartyReturnInTrDetailService {
        private ILoanPartyReturnInTrDetailRepository _repo;
        public LoanPartyReturnInTrDetailService(ILoanPartyReturnInTrDetailRepository repo) {
            _repo = repo;
        }
        public LoanPartyReturnInTrDetail Create(LoanPartyReturnInTrDetail o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LoanPartyReturnInTrDetail Delete(LoanPartyReturnInTrDetail o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanPartyReturnInTrDetail> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanPartyReturnInTrDetail> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LoanPartyReturnInTrDetail GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LoanPartyReturnInTrDetail Update(LoanPartyReturnInTrDetail o) {
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
