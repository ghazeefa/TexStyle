using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.Core.ReportsViewModel.PPC;
using TexStyle.DomainServices.Interfaces.IPPC;
using TexStyle.Infrastructure;

namespace TexStyle.DomainServices.Implementation.PPC
{
    class PPCPlanningRepository: Repository<PPCPlanning> , IPPCPlanningRepository
    {
        private AppDbContext _db;
        public PPCPlanningRepository(AppDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<IList<PPCPlanning>> GetAll()
        {
            return await Task.FromResult( _db.PPCPlannings
                .Include(x => x.ProductionType)
                .Include(x => x.Reprocesses)
                .Include(x => x.YarnManufacturer)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)

                .Include(x => x.knittingParty)

                .Include(x => x.FabricQuality)
                .Include(x => x.FabricTypes).Include(x => x.FabricType)
                .Include(x => x.Party)
                .Include(x => x.Buyer)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.InwardGatePassDetail)
                .Include(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                .Include(x => x.PurchaseOrder)
                .Where(x => x.IsDeleted == false).AsNoTracking().ToList());
        }

        public override async Task<IList<PPCPlanning>> GetAll(params Expression<Func<PPCPlanning, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.PPCPlannings
                .Include(x => x.ProductionType)
                .Include(x => x.Reprocesses)
                .Include(x => x.YarnManufacturer)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)

                .Include(x => x.knittingParty)

                .Include(x => x.FabricQuality)
                .Include(x => x.FabricTypes).Include(x => x.FabricType)
                .Include(x => x.Party)
                .Include(x => x.Buyer)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.InwardGatePassDetail)
                .Include(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                .Include(x => x.PurchaseOrder)
                .Where(x => x.IsDeleted == false).AsNoTracking().ToList());
        }
        public async Task<PPCPlanning> GetSingle()
        {
            return await Task.FromResult( _db.PPCPlannings
                .Include(x => x.ProductionType)
                .Include(x => x.Reprocesses)
                .Include(x => x.YarnManufacturer)
                .Include(x => x.YarnQuality)

                .Include(x => x.knittingParty)

                .Include(x => x.FabricQuality)
                .Include(x => x.FabricTypes).Include(x => x.FabricType)
                .Include(x => x.Party)
                .Include(x => x.Buyer)
                .Include(x => x.YarnType)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.InwardGatePassDetail)
                .Include(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                .Include(x => x.PurchaseOrder)
                .AsNoTracking().Where(x=>x.IsDeleted==false)
                .SingleOrDefault());
        }
        public override async Task< PPCPlanning> GetSingle(Func<PPCPlanning, bool> where, params Expression<Func<PPCPlanning, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.PPCPlannings
                .Include(x => x.ProductionType)
                .Include(x => x.Reprocesses)
                .Include(x => x.YarnManufacturer)
                .Include(x => x.YarnQuality)

                .Include(x => x.knittingParty)

                .Include(x => x.FabricQuality)
                .Include(x => x.FabricTypes).Include(x => x.FabricType)
                .Include(x => x.Party)
                .Include(x => x.Buyer)
                .Include(x => x.YarnType)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.InwardGatePassDetail)
                .Include(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                .Include(x => x.PurchaseOrder)
                .AsNoTracking()
                .SingleOrDefault(where));
        }

        //public override IList<PPCPlanning> GetReprocessed(params Expression<Func<PPCPlanning, object>>[] navigationProperties)
        //{
        //    return _db.PPCPlannings
        //        //.Include(x => x.ActivityType)
        //        .Include(x => x.YarnManufacturer)
        //        .Include(x => x.YarnQuality)
        //        .Include(x => x.YarnType)
        //        .Include(x => x.PurchaseOrder)
        //        .Include(x => x.InwardGatePassDetail)
        //        .Include(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
        //        .Include(x => x.PurchaseOrder)
        //        .Include(x => x.Reprocesses)
        //        .Where(x => x.IsDeleted == false).
        //        AsNoTracking().ToList(); ;
        //}
        public async Task<IList<PPCPlanning>> GetAllFabric()
        {
            return await Task.FromResult( _db.PPCPlannings
                .Include(x => x.ProductionType)
                .Include(x => x.Reprocesses)
                .Include(x => x.YarnManufacturer)
                .Include(x => x.YarnQuality)
                .Include(x => x.YarnType)

                .Include(x => x.knittingParty)

                .Include(x => x.FabricQuality)
                .Include(x => x.FabricTypes).Include(x => x.FabricType)
                .Include(x => x.Party)
                .Include(x => x.Buyer)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.InwardGatePassDetail)
                .Include(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                .Include(x => x.PurchaseOrder)
                .Where(x => x.IsDeleted == false && x.IsYarn == false).AsNoTracking().ToList());
        }
        public async Task<IList<PPCPlanning>> GetList()
        {
            return await Task.FromResult( _db.PPCPlannings
                .Include(x => x.ProductionType)
                .Include(x => x.Reprocesses)
                .Include(x => x.YarnManufacturer)
                .Include(x => x.YarnQuality)
                .Include(x => x.FabricQuality)
                .Include(x => x.FabricTypes)
                .Include(x => x.Party)
                .Include(x => x.FabricType)

                .Include(x => x.knittingParty)

                .Include(x => x.Buyer)
                .Include(x => x.YarnType)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.InwardGatePassDetail)
                .Include(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                .Include(x => x.PurchaseOrder)
                .ToList());
        }


        public override async Task<IList<PPCPlanning>> GetList(Func<PPCPlanning, bool> where, params Expression<Func<PPCPlanning, object>>[] navigationProperties)
        {
            return await Task.FromResult( _db.PPCPlannings
                .Include(x => x.ProductionType)
                .Include(x => x.Reprocesses)
                .Include(x => x.YarnManufacturer)
                .Include(x => x.YarnQuality)
                .Include(x => x.FabricQuality)
                .Include(x => x.FabricTypes)
                .Include(x => x.Party)      
                .Include(x => x.FabricType)

                .Include(x => x.knittingParty)

                .Include(x => x.Buyer)
                .Include(x => x.YarnType)
                .Include(x => x.PurchaseOrder)
                .Include(x => x.InwardGatePassDetail)
                .Include(x => x.BuyerColor).ThenInclude(x => x.Buyer).ThenInclude(x => x.Party)
                .Include(x => x.PurchaseOrder)
                 .Where(where).ToList());
        }

        public async Task<List<EcruYarnReceiveRepository_P1ViewModel>> EcruYarnReceiveRepository_P1(long userid)
        {
            return await Task.FromResult(_db.EcruYarnReceiveRepository_P1ViewModels.FromSql($"dbo.usp_EcruYarnReceive_P1  @userid = {userid}, @yarntypeid = {0}, @yarnqualityid = {0}, @yarnpartyid = {0}").ToList());
        }
        public async Task<List<EcruYarnConsumptionRepository_P2ViewModel>> EcruYarnConsumptionRepository_P2(long userid)
        {
            return await Task.FromResult(_db.EcruYarnConsumptionRepository_P2ViewModels.FromSql($"dbo.usp_EcruYarnConsuption_P2 { userid}").ToList());
        }

        public async Task<List<DailyProductionRepository_P3ViewModel>> DailyProductionRepository_P3(long userid)
        {
         
            return await Task.FromResult( _db.DailyProductionRepository_P3ViewModels.FromSql($"dbo.usp_DailyProduction_P3 @userid = {userid}, @yarntypeid = {0}, @yarnqualityid = {0}, @yarnpartyid = {0}").ToList());
        }  
        public async Task<List<DyedStockRepository_P4ViewModel>> DyedStockRepository_P4(long userid)
        {
            return await Task.FromResult( _db.DyedStockRepository_P4ViewModels.FromSql($"dbo.usp_DyedStock_P4 { userid}").ToList());
        }       
        public async Task<List<DyedStockRepository1_P4ViewModel>> DyedStockRepository1_P4(long userid)
        {
            return await Task.FromResult( _db.DyedStockRepository1_P4ViewModels.FromSql($"dbo.usp_DyedStock_P41 { userid}").ToList());
        } 
        
        public async Task<List<LPSIssuanceRepository_P8ViewModel>> LPSIssuanceRepository_P8ViewModel(long userid)
        {
            return await Task.FromResult( _db.LPSIssuanceRepository_P8ViewModel.FromSql($"usp_LPSIssuanceStatus_P8 { userid}").ToList());
        } 
        public async Task< List<DyedYarnDespatchedRepository_P5ViewModel>> DyedYarnDespatchedRepository_P5(long userid)
        {
            return await Task.FromResult( _db.DyedYarnDespatchedRepository_P5ViewModels.FromSql($"dbo.usp_DyedYarnDespatched_P5  @userid = {userid}, @yarntypeid = {0}, @yarnqualityid = {0}").ToList());
        }
        public async Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository_P7(long userid)
        {
            return await Task.FromResult( _db.EcruYarnStockRepository_P7ViewModels.FromSql($"dbo.usp_EcruYarnStockReport_P7 { userid}").ToList());
        } 
        public async Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository1_P7(long userid)
        {
            return await Task.FromResult( _db.EcruYarnStockRepository1_P7ViewModels.FromSql($"usp_EcruYarnStockReport1_P7 { userid}").ToList());
        }  
        public async Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository11_P7(long userid)
        {
            return await Task.FromResult( _db.EcruYarnStockRepository11_P7ViewModels.FromSql($"usp_EcruYarnStockReport1_P71 @userid = {userid}, @fabrictypeid = {0}, @fabricqualityid = {0}, @buyerid = {0}").ToList());

        } 
        public async Task<List<IssuanceRecordRepository_P9ViewModel>> IssuanceRecordRepository_P9ViewModel(long userid)
        {
            return await Task.FromResult( _db.IssuanceRecordRepository_P9ViewModel.FromSql($"usp_IssuanceRecord_P9 { userid}").ToList());
        } 
        public async Task<List<EcruStockSummary_P10RepositoryViewModal>> EcruStockSummary_P10RepositoryViewModal(long userid)
        {
            return await Task.FromResult( _db.EcruStockSummary_P10RepositoryViewModal.FromSql($"usp_EcruStockSummary_10 { userid}").ToList());
        }
        public async Task<List<DyedStockSummary_P11RepositoryViewModal>> DyedStockSummary_P11RepositoryViewModal(long userid)
        {
            return await Task.FromResult( _db.DyedStockSummary_P11RepositoryViewModal.FromSql($"usp_DyedStockSummary_P11 { userid}").ToList());
        }
         
        public async Task<List<FactoryPoKgRepository_P12ViewModel>> FactoryPoKgRepository_P12ViewModel()
        {
            return await Task.FromResult( _db.FactoryPoKgRepository_P12ViewModel.FromSql($"usp_FactoryPoKgReport_P12").ToList());
        }
  
        public async Task<List<IssuanceRecordRepository_P9ViewModel>> IssuanceRecord1Repository_P9ViewModel(int LotNo)
        {
            return await Task.FromResult( _db.IssuanceRecord1Repository_P9ViewModel.FromSql($"usp_IssuanceRecord1_P9 { LotNo}").ToList());
        }
           public async Task<List<IGPRecordFabric_P8ViewModel>> IGPRecordFabric_P8ViewModel(long BuyerId, long FabricTypeId, long FabricQualityId)
        {
            return await Task.FromResult( _db.IGPRecordFabric_P8ViewModel.FromSql($"usp_IGPRecordFabric_P8 { BuyerId}, {FabricTypeId}, {FabricQualityId}").ToList());
        }

        public async Task<long> AddRecord(long headerid, long detailid, long po, long ProductionTypeId, DateTime? issueddate, bool IsConfirmed, decimal Qty, int lotno, long BuyerColorId, string FactoryPo, int dyeingWOID, int dyeingWODetailID)
        {
            try
            {
                //store procedure
                var id = _db.PPCPlaningAddData.FromSql("usp_PPCPlaning_AddTrData @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9",
                                        headerid, detailid, po, ProductionTypeId, Qty, lotno, BuyerColorId, FactoryPo, dyeingWOID, dyeingWODetailID)
                          .ToList();
                          return await Task.FromResult(1);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<List<CKL1POViewModelRepository>> CKL1POViewModelRepository(long po)
        {
            return await Task.FromResult( _db.CKL1POViewModelRepository.FromSql($"usp_GetPODataFromOtherServer {po}").ToList());
        }

        public async Task<List<DyeingLossLotWiseViewModel>> DyeingLossLotWiseViewModel(long userid)
        {
            return await Task.FromResult( _db.DyeingLossLotWiseViewModel.FromSql($"usp_DyeingLossLotWise { userid}").ToList());
        }
        public async Task<List<DyeingLossPOWiseColorWiseViewModel>> DyeingLossPOWiseColorWiseViewModel(long userid)
        {
            return await Task.FromResult( _db.DyeingLossPOWiseColorWiseViewModel.FromSql($"usp_DyeingLossPOWiseColorWise { userid}").ToList());
        }

        public async Task<List<EcruFabricPOWiseKnitterWiseViewModel>> EcruFabricPOWiseKnitterWiseViewModel(long userid)
        {
            return await Task.FromResult(_db.EcruFabricPOWiseKnitterWiseViewModel.FromSql($"usp_EcruFabricPOWiseKnitterWise { userid}").ToList());
        }
        public async Task<List<EcruFabricIssuenceViewModel>> EcruFabricIssuenceViewModel(long userid)
        {
            return await Task.FromResult(_db.EcruFabricIssuenceViewModel.FromSql($"usp_EcruFabricIssuence { userid}").ToList());
        }

        public async Task<List<GetFabricDetailByLot>> GetFabricDetailByLot(long lotNo)
        {
            return await Task.FromResult(_db.GetFabricDetailByLot.FromSql($"usp_GetFabricDetailByLot { lotNo }").ToList());
        }

        public async Task<List<EcruStockSummary_YarnCountGsm_RepositoryViewModal>> EcruStockSummary_YarnCountGsm_RepositoryViewModal(long userid)
        {
            return await Task.FromResult(_db.EcruStockSummary_YarnCountGsm_RepositoryViewModal.FromSql($"usp_EcruStockSummary_YarnCountGsm { userid }").ToList());
        }
    }
}
