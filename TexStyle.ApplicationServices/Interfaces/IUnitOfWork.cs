using System;
using System.Collections.Generic;
using System.Text;
using TexStyle.ApplicationServices.Interfaces.Accounts;
using TexStyle.ApplicationServices.Interfaces.IAnalysis;
//using TexStyle.ApplicationServices.Interfaces.IAnalysis;
using TexStyle.ApplicationServices.Interfaces.ICS;
using TexStyle.ApplicationServices.Interfaces.IGate;
using TexStyle.ApplicationServices.Interfaces.IPPC;
using TexStyle.ApplicationServices.Interfaces.IYD;

using TexStyle.Core.CS;

namespace TexStyle.ApplicationServices.Interfaces {
    public interface IUnitOfWork {

        #region Gate
        //IGateIgpService GateIgpService { get; }
        IGateActivityTypeService GateActivityTypeService { get; }
        IGatePassTypeService GatePassTypeService { get; }
        //IGateIgpDetailService GateIgpDetailService { get; }


        //lIGateOgpService GateOgpService { get; }
        //IGateOgpDetailService GateOgpDetailService { get; }

        IGateTrService GateTrService { get; }
        IGateTrDetailService GateTrDetailService { get; }

        #endregion

        #region PPC Planing
        IknittingPartyService knittingPartyService { get; }
        IRollMarkingService RollMarkingService { get; }
        IFabricQualityService FabricQualityService { get; }
        IFabricTypesService FabricTypesService { get; }
        IBagMarkingService BagMarkingService { get; }
        IBuyerColorService BuyerColorService { get; }
        IBuyerService BuyerService { get; }
        IConeMarkingService ConeMarkingService { get; }
        IIGPDetailService IGPDetailService { get; }
        IIGPService IGPService { get; }
        IIssueNoteDetailService IssueNoteDetailService { get; }
        IIssueNoteService IssueNoteService { get; }
        IOGPService OGPService { get; }
        IOGPDetailService OGPDetailService { get; }
        IActivityTypeService OrderActivityTypeService { get; }
        IPartyService PartyService { get; }
        IPPCPlanningService PPCPlanningService { get; }
        IPurchaseOrderService PurchaseOrderService { get; }
        IReportFilterService ReportFilterService { get; }
        IReprocessService ReprocessService { get; }
        IReturnNoteDetailService ReturnNoteDetailService { get; }
        IReturnNoteService ReturnNoteService { get; }
        ISeasonService SeasonService { get; }
        IShadeService ShadeService { get; }
        IStoreLocationService StoreLocationService { get; }
        IYarnManufacturerService YarnManufacturerService { get; }
        IYarnQualityService YarnQualityService { get; }
        IYarnTypeService YarnTypeService { get; }
        IProductionTypeService ProductionTypeService { get; }
        IFactoryPoService FactoryPoService { get; }   
        IFactoryPoDetailService FactoryPoDetailService { get; }   
        ILotMarkingService LotMarkingService { get; }

        #endregion

      



        #region Yarn Dyeing

        IMachineTypeService MachineTypeService { get; }
        IMachineService MachineService { get; }
        IProcessTypeService ProcessTypeService { get; }
        IDyeService DyeService { get; }
        ICostingService CostingService { get; }
        IChemicalService ChemicalService { get; }


        IStickerService StickerService { get; }
        IRecipeStepService RecipeStepService { get; }
        IRecipeService RecipeService { get; }
        IRecipeDetailService RecipeDetailService { get; }
        IRecipeLPSService RecipeLPSService { get; }
        IRecipeFormatHeaderService RecipeFormatHeaderService { get; }
        IRecipeFormatDetailService RecipeFormatDetailService { get; }

        #endregion

        #region Chemical Store
        //ISupplierService SupplierService { get; }

        //ILCImportInTrService LCImportInTrService { get; }
        //ILCImportInTrDetailService LCImportInTrDetailService { get; }

        //ILoanPartyReturnInTrService LoanPartyReturnInTrService { get; }
        //ILoanPartyReturnInTrDetailService LoanPartyReturnInTrDetailService { get;  }

        //ILoanTakenInTrService LoanTakenInTrService { get; }
        //ILoanTakenInTrDetailService LoanTakenInTrDetailService { get; }

        //ILocalPurchaseInTrService LocalPurchaseInTrService { get; }
        //ILocalPurchaseInTrDetailService LocalPurchaseInTrDetailService { get; }

        //IInterUnitOutTrDetailService InterUnitOutTrDetailService { get; }
        //IInterUnitOutTrService InterUnitOutTrService { get; }

        //ILoanPartyGivenOutTrDetailService LoanPartyGivenOutTrDetailService { get; }
        //ILoanPartyGivenOutTrService LoanPartyGivenOutTrService { get; }

        //ILoanTakenReturnOutTrDetailService LoanTakenReturnOutTrDetailService { get; }
        //ILoanTakenReturnOutTrService LoanTakenReturnOutTrService { get; }

        //IChemicalDilutionTrService ChemicalDilutionTrService { get; }
        //IChemicalDilutionTrDetailService ChemicalDilutionTrDetailService { get; }

        //IChemicalIssuanceRecipeTrService ChemicalIssuanceRecipeTrService { get; }
        //IChemicalIssuanceRecipeTrDetailService ChemicalIssuanceRecipeTrDetailService { get; }

        //IDyesChemicalOpenningService DyesChemicalOpenningService { get; }
        //IDyesChemicalOpenningDetailService DyesChemicalOpenningDetailService { get; }

        //IStoreReturnNoteService StoreReturnNoteService { get; }
        //IStoreReturnNoteDetailService StoreReturnNoteDetailService { get; }

        //ITrLinkerMasterService TrLinkerMasterService { get; }

        IDyeChemicalTrService DyeChemicalTrService { get; }
        IDyeChemicalTrDetailService DyeChemicalTrDetailService { get; }

        IDyeingEnergyConsumptionService DyeingEnergyConsumptionService { get; }
        #endregion

        #region User Management
        IAccountService AccountService { get; }
        IAccountRoleService AccountRoleService { get; }

        #endregion

        #region Analysis
        IAnalysisTypeService AnalysisTypeService { get; }
        IDefectService DefectService { get; }
        IDefectAnalysisService DefectAnalysisService { get; }
        #endregion
    }
}
