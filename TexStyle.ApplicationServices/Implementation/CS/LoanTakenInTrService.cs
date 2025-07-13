using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.DomainServices.Interfaces.IGate;

namespace TexStyle.ApplicationServices.Implementation.CS {
    internal class LoanTakenInTrService : ILoanTakenInTrService {
        private ILoanTakenInTrRepository _repo;
      //  private IGateIgpRepository _gateRepo;
        public LoanTakenInTrService(ILoanTakenInTrRepository repo)//, IGateIgpRepository gateRepo) {
        { _repo = repo;
        //    _gateRepo = gateRepo;
        }
        public LoanTakenInTr Create(LoanTakenInTr o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LoanTakenInTr Delete(LoanTakenInTr o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<LoanTakenInTr> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<LoanTakenInTr> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date, x=>x.GateTr).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LoanTakenInTr GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LoanTakenInTr Update(LoanTakenInTr o) {
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
