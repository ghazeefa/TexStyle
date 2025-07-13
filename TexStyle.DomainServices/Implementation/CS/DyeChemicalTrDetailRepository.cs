using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.Core.ReportsViewModel.CS.RateDetail;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;


namespace TexStyle.DomainServices.Implementation.CS
{
    class DyeChemicalTrDetailRepository : Repository<DyeChemicalTrDetail>, IDyeChemicalTrDetailRepository
    {
     
        private AppDbContext _db;
        public DyeChemicalTrDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public override async Task<DyeChemicalTrDetail> GetSingle(Func<DyeChemicalTrDetail, bool> where, params Expression<Func<DyeChemicalTrDetail, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.DyeChemicalTrDetails
    .Include(x => x.DyeChemicalTrs).ThenInclude(x => x.Party)
    .Include(x => x.DyeChemicalTrs).ThenInclude(x => x.GateTr)
    .Include(x => x.DyeChemicalXrefDetailTr).ThenInclude(x => x.DyeChemicalTrs).ThenInclude(x=>x.GateTr)
      .Include(y => y.Chemical)
      .Include(y => y.Dye)
     .SingleOrDefault(where));
        }

        public override async Task<IList<DyeChemicalTrDetail>> GetList(Func<DyeChemicalTrDetail, bool> where, params Expression<Func<DyeChemicalTrDetail, object>>[] navigationProperties)
        {
            
            return await Task.FromResult( _db.DyeChemicalTrDetails
              .Include(x => x.DyeChemicalTrs).ThenInclude(x => x.Party)
              .Include(x => x.DyeChemicalTrs).ThenInclude(x => x.GateTr)
                .Include(y => y.Chemical)
                .Include(y => y.Dye)
                .Where(where).ToList());
        }

        public async Task<long> DyeChemicalUpdateDr(long headerid, decimal? fairprice, long? igprefno, string qtycr, int? trtype, long? invoiceno, long? dtreno, DateTime? invoicedate)
        {
            try
            {

                //store procedure
                   var id =_db.ReturnedIdViewModels.FromSql($"usp_DyeChemical_AddTrData  {headerid} ,{ fairprice} , {igprefno}, { qtycr}, { trtype}, { invoiceno}, { dtreno}, { invoicedate}").ToList();
           

                return await Task.FromResult(id.FirstOrDefault().headerId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
               
        }

        public async Task<DyeChemicalTrDetail> GetSingleById(long id)
        {
            return await Task.FromResult(_db.DyeChemicalTrDetails
            .Include(x => x.DyeChemicalTrs).ThenInclude(x => x.Party)
            .Include(x => x.DyeChemicalTrs).ThenInclude(x => x.GateTr)
            .Include(x => x.DyeChemicalXrefDetailTr).ThenInclude(x => x.DyeChemicalTrs).ThenInclude(x => x.GateTr)
            .Include(y => y.Chemical)
            .Include(y => y.Dye)
            .Where(y => y.Id == id && !y.IsDeleted).FirstOrDefault());
             
        }
    }
}
