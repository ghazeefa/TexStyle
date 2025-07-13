using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.Gate;
using TexStyle.DomainServices.Interfaces.IGate;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.Gate {
    internal class GateTrRepository : Repository<GateTr>, IGateTrRepository {
        private AppDbContext _db;
        public GateTrRepository(AppDbContext db) : base(db) {
            _db = db;
        }

        public virtual async Task AddByNewSno(GateTr o) {
            using (var commit = _db.Database.BeginTransaction()) {
                try {

                    _db.Entry(o).State = EntityState.Added;
                    o.Sno = (_db.GateTrs.Where(x => x.GateActivityTypeId == o.GateActivityTypeId).Select(x => (long?)x.Sno).Max() ?? 0) + 1;
                    await _db.SaveChangesAsync();
                    commit.Commit();
                }
                catch (Exception ex) {
                    commit.Rollback();
                    throw ex;
                }
            }
        }
        public virtual async Task AddByNewActivityType(GateTr o, bool ispurchase, bool isloanin, bool isloanout) {
            using (var commit = _db.Database.BeginTransaction()) {
                try {

                    _db.Entry(o).State = EntityState.Added;
                    o.Sno = (_db.GateTrs.Where(x => x.GateActivityType.IsPurchaseActivity == ispurchase && x.GateActivityType.IsLoanINActivity == isloanin && x.GateActivityType.IsLoanOutActivity == isloanout).Select(x => (long?)x.Sno).Max() ?? 0) + 1;
                    await _db.SaveChangesAsync();
                    commit.Commit();
                }
                catch (Exception ex) {
                    commit.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<GateTr> GetSingle()
        {
            return await Task.FromResult( _db.GateTrs

                    .Include(x => x.Party)
                    .Include(x => x.Buyer)
                    .Include(x => x.GateActivityType)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.YarnType)

                 .Include(x => x.GateTrDetails).ThenInclude(y => y.FabricTypes)

                .Include(x => x.GateTrDetails).ThenInclude(y => y.GateTr)
                 .Include(x => x.GetDyeChemicalTr)
                    .Include(x => x.GetDyeChemicalTr).ThenInclude(y => y.DyeChemicalTrDetails)
                .SingleOrDefault());
        }

        public override async Task<GateTr> GetSingle(Func<GateTr, bool> where, params Expression<Func<GateTr, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.GateTrs

                    .Include(x => x.Party)
                    .Include(x => x.Buyer)
                    .Include(x => x.GateActivityType)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.YarnType)

                 .Include(x => x.GateTrDetails).ThenInclude(y => y.FabricTypes)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.GateTr)
                 .Include(x => x.GetDyeChemicalTr)
                    .Include(x => x.GetDyeChemicalTr).ThenInclude(y => y.DyeChemicalTrDetails)
                .SingleOrDefault(where));
        }

        public async Task<IList<GateTr>> GetList()
        {
            return await Task.FromResult( _db.GateTrs
                    //.Include(x => x.YarnType)
                    .Include(x => x.Party)
                     .Include(x => x.Buyer)
                  
                    .Include(x => x.GateActivityType)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.YarnType)
                 .Include(x => x.GateTrDetails).ThenInclude(y => y.FabricTypes)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.GateTr)
                 .Include(x => x.GetDyeChemicalTr)
                    .Include(x => x.GetDyeChemicalTr).ThenInclude(y => y.DyeChemicalTrDetails)
                .ToList());
        }

        public override async Task<IList<GateTr>> GetList(Func<GateTr, bool> where, params Expression<Func<GateTr, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.GateTrs
                    //.Include(x => x.YarnType)
                    .Include(x => x.Party)
                          .Include(x => x.Buyer)
                    .Include(x => x.GateActivityType)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.YarnType)
                 .Include(x => x.GateTrDetails).ThenInclude(y => y.FabricTypes)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.GateTr)
                 .Include(x => x.GetDyeChemicalTr)
                    .Include(x => x.GetDyeChemicalTr).ThenInclude(y => y.DyeChemicalTrDetails)
                .Where(where).ToList());
        }
        public async Task<IList<GateTr>> GetListForActivityStatus(bool isdeleted, bool ispurchase, bool isloaninactivity, bool isloanoutactivity, params Expression<Func<GateTr, object>>[] navigationProperties) {
            return await Task.FromResult( _db.GateTrs
              //  .Include(x => x.YarnType)
                .Include(x => x.Party)
                 .Include(x => x.Buyer)
                .Include(x => x.GateActivityType)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Chemical)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.Dye)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.YarnType)
                 .Include(x => x.GateTrDetails).ThenInclude(y => y.FabricTypes)
                .Include(x => x.GateTrDetails).ThenInclude(y => y.GateTr)
                 .Include(x => x.GetDyeChemicalTr)
                    .Include(x => x.GetDyeChemicalTr).ThenInclude(y => y.DyeChemicalTrDetails)
                .Where(x => x.GateActivityType.IsPurchaseActivity == ispurchase && x.GateActivityType.IsLoanINActivity == isloaninactivity
                && x.GateActivityType.IsLoanOutActivity == isloanoutactivity && x.IsDeleted == isdeleted).ToList());
        }

    }
}
