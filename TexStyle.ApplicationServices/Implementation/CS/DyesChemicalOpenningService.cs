using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    class DyesChemicalOpenningService : IDyesChemicalOpenningService {
        private IDyesChemicalOpenningRepository _repo;
        public DyesChemicalOpenningService(IDyesChemicalOpenningRepository repo) {
            _repo = repo;
        }

        public DyesChemicalOpenning Create(DyesChemicalOpenning o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }
        public DateTime GetLastOpeningDate()
        {
            try
            {
                var list = _repo.GetList(x => x.IsDeleted == false).ToList().LastOrDefault().TransactionDate;

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DyesChemicalOpenning Delete(DyesChemicalOpenning o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<DyesChemicalOpenning> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false, x => x.DyesChemicalOpenningDetails).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<DyesChemicalOpenning> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && (x.TransactionDate.Date >= start.Date && x.TransactionDate.Date <= end.Date)).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public DyesChemicalOpenning GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public DyesChemicalOpenning Update(DyesChemicalOpenning o) {
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
