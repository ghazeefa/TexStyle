using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TexStyle.Core.CS;
using TexStyle.Core.ReportsViewModel.CS;
using TexStyle.DomainServices.Interfaces.ICS;
using TexStyle.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;
using OfficeOpenXml.Drawing.Chart;

namespace TexStyle.DomainServices.Implementation.CS
{
    class DyeingEnergyConsumptionRepository : Repository<DyeingEnergyConsumption>, IDyeingEnergyConsumptionRepository
    {
        private AppDbContext _db;
        public DyeingEnergyConsumptionRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<DyeingEnergyConsumption> GetEnergyConsumptionBetweenDates(DateTime dateFrom, DateTime dateTo, bool IsYarn)
        {
            return await Task.FromResult(_db.DyeingEnergyConsumptions.Where(x => x.Date >= dateFrom && x.Date <= dateTo && x.IsYarn == IsYarn).FirstOrDefault());
        }

        

    }
}
