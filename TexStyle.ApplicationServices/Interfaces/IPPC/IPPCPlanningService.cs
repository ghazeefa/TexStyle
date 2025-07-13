using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexStyle.Core.PPC;
using TexStyle.Core.ReportsViewModel.PPC;

namespace TexStyle.ApplicationServices.Interfaces.IPPC {
    public interface IPPCPlanningService : IDefaultService<PPCPlanning> {
        Task<IQueryable<PPCPlanning>> GetReprocessById(long? id);
        Task<PPCPlanning> CreateFabric(PPCPlanning o);
        Task<PPCPlanning> UpdateFabric(PPCPlanning o);
        Task<List<PPCPlanning>> GetAllFabric();
        Task<List<PPCPlanning>> GetBetweenDateRangeFabric(DateTime start, DateTime end);
        Task<List<PPCPlanning>> GetListbyIgpDetailId(long? id);
        Task<List<EcruYarnReceiveRepository_P1ViewModel>> EcruYarnReceiveService_P1(long userid);
        Task<List<EcruYarnConsumptionRepository_P2ViewModel>> EcruYarnConsumptionService_P2(long userid);
        Task<List<DailyProductionRepository_P3ViewModel>> DailyProductionService_P3(long userid);
        Task<List<DyedStockRepository_P4ViewModel>> DyedStockService_P4(long userid);
        Task<List<DyedStockRepository1_P4ViewModel>> DyedStockService1_P4(long userid);
        Task<List<DyedYarnDespatchedRepository_P5ViewModel>> DyedYarnDespatchedService_P5(long userid);
        Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository_P7(long userid);    
        Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository1_P7(long user);
        Task<List<EcruYarnStockRepository_P7ViewModel>> EcruYarnStockRepository11_P7(long user);
        Task<List<IssuanceRecordRepository_P9ViewModel>> IssuanceRecordRepository_P9ViewModel(long user);
        Task<List<IssuanceRecordRepository_P9ViewModel>> IssuanceRecord1Repository_P9ViewModel(int LotNo);
        Task<List<IGPRecordFabric_P8ViewModel>> IGPRecordFabric_P8ViewModel(long BuyerId, long FabricTypeId, long FabricQualityId);
        Task<long> AddRecord(long headerid, long detailid, long po, long ProductionTypeId, DateTime? issueddate, bool IsConfirmed, decimal Qty, int lotno, long BuyerColorId, string FactoryPo, int dyeingWOID, int dyeingWODetailID);
        Task<List<LPSIssuanceRepository_P8ViewModel>> LPSIssuanceRepository_P8ViewModel(long userid);
        Task<List<EcruStockSummary_P10RepositoryViewModal>> EcruStockSummary_P10RepositoryViewModal(long userid);
        Task<List<DyedStockSummary_P11RepositoryViewModal>> DyedStockSummary_P11RepositoryViewModal(long userid);
        Task<List<FactoryPoKgRepository_P12ViewModel>> FactoryPoKgRepository_P12ViewModel();
        Task<List<CKL1POViewModelRepository>> CKL1POViewModelRepository(long po);
        Task<List<PPCPlanning>> GetListByPoCode(string id);
        Task<List<DyeingLossLotWiseViewModel>> DyeingLossLotWiseViewModel(long userid);
        Task<List<DyeingLossPOWiseColorWiseViewModel>> DyeingLossPOWiseColorWiseViewModel(long userid);
        Task<List<EcruFabricPOWiseKnitterWiseViewModel>> EcruFabricPOWiseKnitterWiseViewModel(long userid);
        Task<List<EcruFabricIssuenceViewModel>> EcruFabricIssuenceViewModel(long userid);
        Task<List<GetFabricDetailByLot>> GetFabricDetailByLot(long lotNo);
        Task<List<EcruStockSummary_YarnCountGsm_RepositoryViewModal>> EcruStockSummary_YarnCountGsm(long userid);
    }
}
