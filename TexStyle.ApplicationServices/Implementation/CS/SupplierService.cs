using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    internal class SupplierService : ISupplierService {
        private ISupplierRepository _repo;
        public SupplierService(ISupplierRepository supplierRepository) {
            _repo = supplierRepository;
        }

        public Supplier Create(Supplier o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public Supplier Delete(Supplier o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<Supplier> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public List<Supplier> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public Supplier GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public Supplier Update(Supplier o) {
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
