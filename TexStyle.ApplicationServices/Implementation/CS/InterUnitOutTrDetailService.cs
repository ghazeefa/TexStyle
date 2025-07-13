using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    class InterUnitOutTrDetailService : IInterUnitOutTrDetailService {
        private IInterUnitOutTrDetailRepository _repo;
        public InterUnitOutTrDetailService(IInterUnitOutTrDetailRepository repo) {
            _repo = repo;
        }

        public InterUnitOutTrDetail Create(InterUnitOutTrDetail o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public InterUnitOutTrDetail Delete(InterUnitOutTrDetail o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<InterUnitOutTrDetail> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false, x => x.Dye, x => x.Chemical).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<InterUnitOutTrDetail> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.InterUnitOutTr.TransactionDate.Date >= start.Date && x.InterUnitOutTr.TransactionDate.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public InterUnitOutTrDetail GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public InterUnitOutTrDetail Update(InterUnitOutTrDetail o) {
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
