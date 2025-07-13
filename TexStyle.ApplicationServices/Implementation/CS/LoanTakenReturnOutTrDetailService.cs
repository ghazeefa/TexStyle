using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    class LoanTakenReturnOutTrDetailService : ILoanTakenReturnOutTrDetailService {
        private ILoanTakenReturnOutTrDetailRepository _repo;
        public LoanTakenReturnOutTrDetailService(ILoanTakenReturnOutTrDetailRepository repo) {
            _repo = repo;
        }
        public LoanTakenReturnOutTrDetail Create(LoanTakenReturnOutTrDetail o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public LoanTakenReturnOutTrDetail Delete(LoanTakenReturnOutTrDetail o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanTakenReturnOutTrDetail> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<LoanTakenReturnOutTrDetail> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LoanTakenReturnOutTrDetail GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public decimal GetUsedKgLoanTaken(long id)
        {
            try
            {
                var list = _repo.GetList(x => x.IsDeleted == false && x.LoanTakenInTrDetailId == id).ToList();

                return list.Sum(x => x.QtyCr).Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoanTakenReturnOutTrDetail Update(LoanTakenReturnOutTrDetail o) {
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
