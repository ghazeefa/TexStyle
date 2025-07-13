using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.Gate;
using TexStyle.Core.ReportsViewModel.CS.RateDetail;
using TexStyle.DomainServices.Interfaces.IGate;
using TexStyle.Infrastructure;


namespace TexStyle.DomainServices.Implementation.Gate
{
    class GateTrDetailRepository : Repository<GateTrDetail>, IGateTrDetailRepository
    {
        private AppDbContext _db;
        public GateTrDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        public async Task<bool> UpdateRates(List<RateDetail> rateDetail)
        {
            try
            {

                foreach(var a in rateDetail)
                {
                  await Task.FromResult(  _db.Database.ExecuteSqlCommand($"dbo.usp_GateUpdateRate {a.id},{a.rate}"));
                    // store proceure 

                }

            }
            catch(Exception ex)

            {
                throw ex;
            }

            return true;
        }
    }
}
