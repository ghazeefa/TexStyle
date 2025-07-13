using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    internal class LocalPurchaseInTrService : ILocalPurchaseInTrService {
        private ILocalPurchaseInTrRepository _repo;
        public LocalPurchaseInTrService(ILocalPurchaseInTrRepository repo) {
            _repo = repo;
        }

        public LocalPurchaseInTr Create(LocalPurchaseInTr o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LocalPurchaseInTr Delete(LocalPurchaseInTr o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<LocalPurchaseInTr> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false, x=>x.GateTr).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<LocalPurchaseInTr> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.TrDate.Date >= start.Date && x.TrDate.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LocalPurchaseInTr GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public LocalPurchaseInTr Update(LocalPurchaseInTr o) {
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
