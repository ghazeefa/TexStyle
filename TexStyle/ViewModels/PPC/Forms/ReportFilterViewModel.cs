using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC {
    public class ReportFilterViewModel {
        public long? Id { get; set; }

        [DataType(DataType.Date)]
        public Nullable<DateTime> DateFrom { get; set; }
        [DataType(DataType.Date)]
        public Nullable<DateTime> DateTo { get; set; }

        public long? YarnTypeId { get; set; }
        public long? YarnQualityId { get; set; }
        public long? YarnPartyId { get; set; }        
        
        public long? FabricTypeId { get; set; }
        public long? FabricQualityId { get; set; }
        public long? BuyerId { get; set; }

        public long? UserId { get; set; }
        public long? AnalysisTypeId { get; set; }
        public bool IsYarn { get; set; }
        public int? BranchId { get; set; }
        public long? BuyerColorId { get; set; }
        public long? FactoryPO { get; set; }
        public string BranchName { get; set; }
        public long? LotNo { get; set; }

    }
}
