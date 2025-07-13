using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
    public class EcruYarnStockRepository_P7ViewModel
    {
        public DateTime IgpDate { get; set; }
        public string Party { get; set; }
        public long IGPNo { get; set; }
        public decimal Bags { get; set; }
        public string YarnManufacturer { get; set; }
        public string Yarntype { get; set; }
        public string YarnQuality { get; set; }
        public decimal TearWeightInKg { get; set; }
        public decimal InStockKgs { get; set; }
        public decimal DyedKgs { get; set; }
        public decimal IssuedKgs { get; set; }
        public decimal NetKgs { get; set; }
        public decimal ReturnedKgs { get; set; }
        public string Buyer { get; set; }
        public string BuyerPO { get; set; }
        public long? GateReffId { get; set; }
        public string FabricType { get; set; }
        public string FabricQuality { get; set; }
        public long? GSM { get; set; }
        public long? Dia { get; set; }
        public decimal? NoOfRolls { get; set; }
        public bool IsYarn { get; set; }
        public string Knitter { get; set; }
        public decimal FinishingKgs { get; set; }
    }
}
