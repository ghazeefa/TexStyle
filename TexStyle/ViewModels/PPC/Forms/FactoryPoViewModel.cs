using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Forms
{
    public class FactoryPoViewModel
    {
        public string Description { get; set; }
        [DisplayName("Sno")]
        public long? Id { get; set; }
        [DisplayName("Date")]
        public DateTime Date { get; set; }
    
        [DisplayName("Confirm?")]
        public Boolean IsCancel { get; set; }
        [DisplayName("Confirm?")]
        public Boolean IsChecked { get; set; }
        [DisplayName("Buyer")]
        public long? BuyerId { get; set; }
 
        [DisplayName("GSM")]
        public long? GSM { get; set; }
        [DisplayName("Po")]
        public long? Po { get; set; }

        [DisplayName("BuyerColor")]
        public long? BuyerColorId { get; set; }

    }
}
