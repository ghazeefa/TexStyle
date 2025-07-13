using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    class LoanTakenReturnOutTrService : ILoanTakenReturnOutTrService {
        private ILoanTakenReturnOutTrRepository _repo;
        public LoanTakenReturnOutTrService(ILoanTakenReturnOutTrRepository repo) {
            _repo = repo;
        }
        public LoanTakenReturnOutTr Create(LoanTakenReturnOutTr o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LoanTakenReturnOutTr Delete(LoanTakenReturnOutTr o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanTakenReturnOutTr> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanTakenReturnOutTr> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date ).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LoanTakenReturnOutTr GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false, x=>x.LoanTakenInTr);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LoanTakenReturnOutTr Update(LoanTakenReturnOutTr o) {
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
