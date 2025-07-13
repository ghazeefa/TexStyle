using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC
{
  public  class FactoryPoDetail : DefaultEntity
    {
        public long Id { get; set; }
        public int Sno { get; set; }
        public string Description { get; set; }
        [DisplayName("Tear Weight (Kg)")]
        public decimal TearWeightInKg { get; set; }
        [DisplayName("Net Weight (Kg)")]
        public decimal NetWeightInKg { get; set; }
        [DisplayName("Gross Weight (Kg)")]
        [NotMapped]
        public decimal GrossWeightInKg
        {
            get
            {
                return NetWeightInKg + TearWeightInKg;
            }
        }
        [DisplayName("Weight")]
        public long? Weight { get; set; }
        [DisplayName("Dia")]
        public long? Dia { get; set; }
        [DisplayName("GSM")]
        public long? GSM { get; set; }
        public long? FactoryPoId { get; set; }
        [ForeignKey(nameof(FactoryPoId))]
        public virtual FactoryPo FactoryPo { get; set; }
        public long? FabricTypesId { get; set; }
        [ForeignKey(nameof(FabricTypesId))]
        public virtual FabricTypes FabricTypes { get; set; }
        public long? FabricQualityId { get; set; }
        [ForeignKey(nameof(FabricQualityId))]
        public virtual FabricQuality FabricQuality { get; set; }
        
        public long? BuyerColorId { get; set; }
        [ForeignKey(nameof(BuyerColorId))]
        public virtual BuyerColor BuyerColor { get; set; }
    }
}
