using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.Gate {
    public class GateActivityType : DefaultEntity {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsLoanINActivity { get; set; }
        public bool IsPurchaseActivity { get; set; }
        public bool IsLoanOutActivity { get; set; }
    }
}
