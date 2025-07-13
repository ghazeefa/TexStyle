using System;
using System.Collections.Generic;
using System.Text;
using static TexStyle.ApplicationServices.ApplicationServiceRegistration;
using TexStyle.ApplicationServices.Interfaces;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using TexStyle.ApplicationServices.Interfaces.IYD;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.ApplicationServices.Interfaces.IGate;
using TexStyle.ApplicationServices.Implementation.CS;
using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.ApplicationServices.Interfaces.IAnalysis;

namespace TexStyle.ApplicationServices.Implementation {
    internal class UnitOfWork : IUnitOfWork {
        #region Gate
        //public IGateIgpService GateIgpService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IGateIgpService>();
        public IGateActivityTypeService GateActivityTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IGateActivityTypeService>();
        public IGatePassTypeService  GatePassTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IGatePassTypeService>();
        //public IGateIgpDetailService GateIgpDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IGateIgpDetailService>();

        //public IGateOgpService GateOgpService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IGateOgpService>();
        //public IGateOgpDetailService GateOgpDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IGateOgpDetailService>();

        public IGateTrService GateTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IGateTrService>();
        public IGateTrDetailService GateTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IGateTrDetailService>();

        #endregion

        #region PPC Planing
        public IknittingPartyService knittingPartyService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IknittingPartyService>();
        public IRollMarkingService RollMarkingService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IRollMarkingService>();
        public IFabricQualityService FabricQualityService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IFabricQualityService>();
        public IBagMarkingService BagMarkingService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IBagMarkingService>();
        public IBuyerColorService BuyerColorService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IBuyerColorService>();
        public IBuyerService BuyerService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IBuyerService>();
        public IConeMarkingService ConeMarkingService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IConeMarkingService>();
        public IIGPDetailService IGPDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IIGPDetailService>();
        public IIGPService IGPService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IIGPService>();
        public IIssueNoteDetailService IssueNoteDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IIssueNoteDetailService>();
        public IIssueNoteService IssueNoteService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IIssueNoteService>();
        public IOGPService OGPService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IOGPService>();
        public IOGPDetailService OGPDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IOGPDetailService>();
        public IActivityTypeService OrderActivityTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IActivityTypeService>();
        public IPartyService PartyService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IPartyService>();
        public IPPCPlanningService PPCPlanningService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IPPCPlanningService>();
        public IPurchaseOrderService PurchaseOrderService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IPurchaseOrderService>();
        public IReportFilterService ReportFilterService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IReportFilterService>();
        public IReprocessService ReprocessService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IReprocessService>();
        public IReturnNoteDetailService ReturnNoteDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IReturnNoteDetailService>();
        public IReturnNoteService ReturnNoteService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IReturnNoteService>();
        public ISeasonService SeasonService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ISeasonService>();
        public IShadeService ShadeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IShadeService>();
        public IStoreLocationService StoreLocationService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IStoreLocationService>();
        public IYarnManufacturerService YarnManufacturerService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IYarnManufacturerService>();
        public IYarnQualityService YarnQualityService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IYarnQualityService>();
        public IYarnTypeService YarnTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IYarnTypeService>();
        public IProductionTypeService ProductionTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IProductionTypeService>();
        public IFabricTypesService FabricTypesService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IFabricTypesService>();
        public ILotMarkingService LotMarkingService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILotMarkingService>();
        public IFactoryPoService FactoryPoService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IFactoryPoService>();
        public IFactoryPoDetailService FactoryPoDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IFactoryPoDetailService>();



        #endregion

        //#region Fabric Planing
        ////public IFabricTypeService IFabricTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IFabricTypeService>();
        //#endregion
        #region Yarn Dying
        // TODO: Review this code
        // TODO: I read this and this is the summary
        /// <summary>
        /// i think this can work but i need to review it in detail and it might take time, 
        /// but if this is working. let it stay here
        /// so if this create scope works i we will transform this into something else like maybe binding shit or something.
        /// </summary>
        public IMachineTypeService MachineTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IMachineTypeService>();
        public IMachineService MachineService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IMachineService>();
        public IChemicalService ChemicalService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IChemicalService>();
        public IDyeService DyeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IDyeService>();
        public ICostingService CostingService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ICostingService>();
        public IProcessTypeService ProcessTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IProcessTypeService>();
        public IRecipeStepService RecipeStepService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IRecipeStepService>();
        public IRecipeService RecipeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IRecipeService>();
        public IRecipeDetailService RecipeDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IRecipeDetailService>();
        public IRecipeLPSService RecipeLPSService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IRecipeLPSService>();
        public IRecipeFormatHeaderService RecipeFormatHeaderService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IRecipeFormatHeaderService>();
        public IRecipeFormatDetailService RecipeFormatDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IRecipeFormatDetailService>(); 
        public IStickerService StickerService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IStickerService>();


        #endregion

        #region Chemical Store
        //public ISupplierService SupplierService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ISupplierService>();

        //public ILCImportInTrService LCImportInTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILCImportInTrService>();
        //public ILCImportInTrDetailService LCImportInTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILCImportInTrDetailService>();

        //public ILoanPartyReturnInTrService LoanPartyReturnInTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILoanPartyReturnInTrService>();
        //public ILoanPartyReturnInTrDetailService LoanPartyReturnInTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILoanPartyReturnInTrDetailService>();

        //public ILoanTakenInTrService LoanTakenInTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILoanTakenInTrService>();
        //public ILoanTakenInTrDetailService LoanTakenInTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILoanTakenInTrDetailService>();

        //public ILocalPurchaseInTrService LocalPurchaseInTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILocalPurchaseInTrService>();
        //public ILocalPurchaseInTrDetailService LocalPurchaseInTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILocalPurchaseInTrDetailService>();
     
        //public IInterUnitOutTrService InterUnitOutTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IInterUnitOutTrService>();
        //public IInterUnitOutTrDetailService InterUnitOutTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IInterUnitOutTrDetailService>();
        
        //public ILoanPartyGivenOutTrDetailService LoanPartyGivenOutTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILoanPartyGivenOutTrDetailService>();
        //public ILoanPartyGivenOutTrService LoanPartyGivenOutTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILoanPartyGivenOutTrService>();
        
        //public ILoanTakenReturnOutTrDetailService LoanTakenReturnOutTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILoanTakenReturnOutTrDetailService>();
        //public ILoanTakenReturnOutTrService LoanTakenReturnOutTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ILoanTakenReturnOutTrService>();

        //public IChemicalDilutionTrService ChemicalDilutionTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IChemicalDilutionTrService>();
        //public IChemicalDilutionTrDetailService ChemicalDilutionTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IChemicalDilutionTrDetailService>();

        //public IChemicalIssuanceRecipeTrService ChemicalIssuanceRecipeTrService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IChemicalIssuanceRecipeTrService>();
        //public IChemicalIssuanceRecipeTrDetailService ChemicalIssuanceRecipeTrDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IChemicalIssuanceRecipeTrDetailService>();

        //public IDyesChemicalOpenningService DyesChemicalOpenningService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IDyesChemicalOpenningService>();
        //public IDyesChemicalOpenningDetailService DyesChemicalOpenningDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IDyesChemicalOpenningDetailService>();

        //public IStoreReturnNoteService StoreReturnNoteService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IStoreReturnNoteService>();
        //public IStoreReturnNoteDetailService StoreReturnNoteDetailService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IStoreReturnNoteDetailService>();

        //public ITrLinkerMasterService TrLinkerMasterService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<ITrLinkerMasterService>();
     
        public IDyeChemicalTrService DyeChemicalTrService=> GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IDyeChemicalTrService>();
        public IDyeChemicalTrDetailService DyeChemicalTrDetailService=> GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IDyeChemicalTrDetailService>();
        public IDyeingEnergyConsumptionService DyeingEnergyConsumptionService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IDyeingEnergyConsumptionService>();


        #endregion

        #region User Management

        public IAccountService AccountService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IAccountService>();
        public IAccountRoleService AccountRoleService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IAccountRoleService>();



        #endregion

        #region Analysis
        public IAnalysisTypeService AnalysisTypeService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IAnalysisTypeService>();
        public IDefectService DefectService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IDefectService>();
        public IDefectAnalysisService DefectAnalysisService => GlobalServiceProvider.CreateScope().ServiceProvider.GetService<IDefectAnalysisService>();

        #endregion
    }
}
