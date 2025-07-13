using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Forms
{
    public class FactoryPoDetailViewModel
    {
        public long? Id { get; set; }
        public int Sno { get; set; }
        public string Description { get; set; }
        [DisplayName("Gross Weight (Kg)")]
        public decimal TearWeightInKg { get; set; }
        [DisplayName("Net Weight (Kg)")]
        public decimal NetWeightInKg { get; set; }     
        [DisplayName("Weight")]
        public long? Weight { get; set; }
        [DisplayName("Dia")]
        public long? Dia { get; set; }
        public long? FactoryPoId { get; set; }

        public long? FabricTypesId { get; set; }
      
        public long? FabricQualityId { get; set; }

        [DisplayName("GSM")]
        public long? GSM { get; set; }
  
        [DisplayName("BuyerColor")]
        public long? BuyerColorId { get; set; }


    }
}
