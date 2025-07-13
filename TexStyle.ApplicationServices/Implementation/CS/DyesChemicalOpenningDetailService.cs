using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.Core.CS;
using TexStyle.DomainServices.Interfaces.ICS;

namespace TexStyle.ApplicationServices.Implementation.CS {
    class DyesChemicalOpenningDetailService : IDyesChemicalOpenningDetailService {
        private IDyesChemicalOpenningDetailRepository _repo;
        public DyesChemicalOpenningDetailService(IDyesChemicalOpenningDetailRepository repo) {
            _repo = repo;
        }

        public DyesChemicalOpenningDetail Create(DyesChemicalOpenningDetail o) {
            try {
                _repo.Add(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public DyesChemicalOpenningDetail Delete(DyesChemicalOpenningDetail o) {
            try {
                o.IsDeleted = true;
                _repo.Update(o);
                return o;
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public List<DyesChemicalOpenningDetail> GetAll() {
            try {
                return _repo.GetList(x => x.IsDeleted == false, x => x.Dye, x => x.Chemical).ToList();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        //public List<DyesChemicalOpenningDetail>  GetAllXrefById(z.Id)
        //{
        //    try
        //    {
        //        return _repo.GetList(x => x.IsDeleted == false, x => x.Dye, x => x.Chemical).ToList();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        public List<DyesChemicalOpenningDetail> GetBetweenDateRange(DateTime start, DateTime end) {
            try {
                return _repo.GetList(x => x.IsDeleted == false && x.CreatedOn.Value.Date >= start.Date && x.CreatedOn.Value.Date <= end.Date).ToList();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public DyesChemicalOpenningDetail GetById(long id) {
            try {
                return _repo.GetSingle(x => x.Id == id && x.IsDeleted == false);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        public DyesChemicalOpenningDetail Update(DyesChemicalOpenningDetail o) {
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
