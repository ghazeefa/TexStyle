using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    internal class LoanTakenInTrDetailService : ILoanTakenInTrDetailService {
        private ILoanTakenInTrDetailRepository _repo;
        public LoanTakenInTrDetailService(ILoanTakenInTrDetailRepository repo) {
            _repo = repo;
        }

        public LoanTakenInTrDetail Create(LoanTakenInTrDetail o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LoanTakenInTrDetail Delete(LoanTakenInTrDetail o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanTakenInTrDetail> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public decimal GetUsedKgLoanTaken(long id)
        {
            try
            {
                var list = _repo.GetList(x => x.IsDeleted == false && x.GateTrDetailId == id).ToList();

                return list.Sum(x => x.QtyCr).Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LoanTakenInTrDetail> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LoanTakenInTrDetail GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LoanTakenInTrDetail Update(LoanTakenInTrDetail o) {
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
