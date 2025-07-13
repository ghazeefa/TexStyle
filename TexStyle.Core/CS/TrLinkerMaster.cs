using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Core.YD;

namespace TexStyle.Core.CS
{
    public class TrLinkerMaster:DefaultEntity
    {
        public long Id { get; set; }
        public bool TrStatus { get; set; } 
        public DateTime Date { get; set; }
        public decimal Qty { get; set; } = 0;
        public decimal Amount { get; set; } = 0;

        public decimal Rate
        {
            get;set;
        }
        public decimal BalQty { get; set; } = 0;
        public decimal BalAmount { get; set; } = 0;

        public long? ChemicalId { get; set; }
        public long? DyeId { get; set; }

        public long? DyesChemicalOpenningId { get; set; }
        public long? DyesChemicalOpenningDetailId { get; set; }

        public long? LCImportInTrId { get; set; }
        public long? LCImportInTrDetailId { get; set; }

        public long? LocalPurchaseInTrId { get; set; }
        public long? LocalPurchaseInTrDetailId { get; set; }

        public long? LoanTakenInTrId { get; set; }
        public long? LoanTakenInTrDetailId { get; set; }

        public long? LoanPartyReturnInTrId { get; set; }
        public long? LoanPartyReturnInTrDetailId { get; set; }

        public long? ChemicalDilutionTrId { get; set; }
        public long? ChemicalDilutionTrDetailId { get; set; }

        public long? InterUnitOutTrId { get; set; }
        public long? InterUnitOutTrDetailId { get; set; }

        public long? LoanTakenReturnOutTrId { get; set; }
        public long? LoanTakenReturnOutTrDetailId { get; set; }

        public long? LoanPartyGivenOutTrId { get; set; }
        public long? LoanPartyGivenOutTrDetailId { get; set; }

        public long? ChemicalIssuanceRecipeTrId { get; set; }
        public long? ChemicalIssuanceRecipeTrDetailId { get; set; }

        public long? StoreReturnNoteId { get; set; }
        public long? StoreReturnNoteDetailId { get; set; }

        [ForeignKey(nameof(ChemicalId))]
        public virtual Chemical Chemical { get; set; }
        [ForeignKey(nameof(DyeId))]
        public virtual Dye Dye { get; set; }

        [ForeignKey(nameof(DyesChemicalOpenningId))]
        public virtual DyesChemicalOpenning DyesChemicalOpenning { get; set; }
        [ForeignKey(nameof(DyesChemicalOpenningDetailId))]
        public virtual DyesChemicalOpenningDetail DyesChemicalOpenningDetail { get; set; }

        [ForeignKey(nameof(LCImportInTrId))]
        public virtual LCImportInTr LCImportInTr { get; set; }
        [ForeignKey(nameof(LCImportInTrDetailId))]
        public virtual LCImportInTrDetail LCImportInTrDetail { get; set; }

        [ForeignKey(nameof(LocalPurchaseInTrId))]
        public virtual LocalPurchaseInTr LocalPurchaseInTr { get; set; }
        [ForeignKey(nameof(LocalPurchaseInTrDetailId))]
        public virtual LocalPurchaseInTrDetail LocalPurchaseInTrDetail { get; set; }

        [ForeignKey(nameof(LoanTakenInTrId))]
        public virtual LoanTakenInTr LoanTakenInTr { get; set; }
        [ForeignKey(nameof(LoanTakenInTrDetailId))]
        public virtual LoanTakenInTrDetail LoanTakenInTrDetail { get; set; }

        [ForeignKey(nameof(LoanPartyReturnInTrId))]
        public virtual LoanPartyReturnInTr LoanPartyReturnInTr { get; set; }
        [ForeignKey(nameof(LoanPartyReturnInTrDetailId))]
        public virtual LoanPartyReturnInTrDetail LoanPartyReturnInTrDetail { get; set; }

        [ForeignKey(nameof(ChemicalDilutionTrId))]
        public virtual ChemicalDilutionTr ChemicalDilutionTr { get; set; }
        [ForeignKey(nameof(ChemicalDilutionTrDetailId))]
        public virtual ChemicalDilutionTrDetail ChemicalDilutionTrDetail { get; set; }

        [ForeignKey(nameof(InterUnitOutTrId))]
        public virtual InterUnitOutTr InterUnitOutTr { get; set; }
        [ForeignKey(nameof(InterUnitOutTrDetailId))]
        public virtual InterUnitOutTrDetail InterUnitOutTrDetail { get; set; }

        [ForeignKey(nameof(LoanTakenReturnOutTrId))]
        public virtual LoanTakenReturnOutTr LoanTakenReturnOutTr { get; set; }
        [ForeignKey(nameof(LoanTakenReturnOutTrDetailId))]
        public virtual LoanTakenReturnOutTrDetail LoanTakenReturnOutTrDetail { get; set; }

        [ForeignKey(nameof(LoanPartyGivenOutTrId))]
        public virtual LoanPartyGivenOutTr LoanPartyGivenOutTr { get; set; }
        [ForeignKey(nameof(LoanPartyGivenOutTrDetailId))]
        public virtual LoanPartyGivenOutTrDetail LoanPartyGivenOutTrDetail { get; set; }

        [ForeignKey(nameof(ChemicalIssuanceRecipeTrId))]
        public virtual ChemicalIssuanceRecipeTr ChemicalIssuanceRecipeTr { get; set; }
        [ForeignKey(nameof(ChemicalIssuanceRecipeTrDetailId))]
        public virtual ChemicalIssuanceRecipeTrDetail ChemicalIssuanceRecipeTrDetail { get; set; }

        [ForeignKey(nameof(StoreReturnNoteId))]
        public virtual StoreReturnNote StoreReturnNote { get; set; }
        [ForeignKey(nameof(StoreReturnNoteDetailId))]
        public virtual StoreReturnNoteDetail StoreReturnNoteDetail { get; set; }

    }
}
