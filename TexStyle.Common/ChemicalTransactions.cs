using System;
using System.Collections.Generic;
using System.Text;
namespace TexStyle.Common
{
   public static class ChemicalTransactions
    {
        //debit
        public static long LCImport  = 3;
        public static long LoanPartyReturnIn = 4;
        public static long LoanTakenIn = 5;
        public static long LocalPurchase  = 6;
        public static long OpeningBalance = 13;
        //credit
        public static long InterUnitOut = 7;
        public static long LoanPartyGivenOut = 8;
        public static long LoanTakenReturnOut = 9;
        public static long Dilution  = 10;
        public static long ChemicalIssuanceRecipe  = 11;
        public static long StoreReturnNote  = 12;           
        public static long GeneralConsumption = 14;
        public static long CKL6RecipeIssuance = 17;
    }
}
