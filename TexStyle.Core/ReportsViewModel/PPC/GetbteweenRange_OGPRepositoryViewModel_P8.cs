using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.ReportsViewModel.PPC
{
   public  class GetbteweenRange_OGPRepositoryViewModel_P8
    {
        public long Id { get; set; }
        public DateTime OgpDate { get; set; }
        public long? InvoiceNo { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public string InvoiceDescription { get; set; }
        public long? ActivityTypeId { get; set; }
        public long? YarnTypeId { get; set; }

        public string ReceivingPerson { get; set; }
        public string VehicleNo { get; set; }
        public string IDCard { get; set; }

        public bool IsReCheck { get; set; }

        public long? PartyId { get; set; }


        public long? FabricTypeId { get; set; }

        public int? BranchId { get; set; }


        public bool? IsYarn { get; set; }


        public long? OGPReffNO { get; set; }
        public string BilityNo { get; set; }




    }
}
