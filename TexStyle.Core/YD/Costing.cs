using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core.YD {
    public class Costing: DefaultEntity {
        public long Id { get; set; }
        public double MIS { get; set; }
        public double Gas { get; set; }
        public DateTime Date { get; set; }
        public double Electricity { get; set; }
        public double SalaryAndWage { get; set; }
        public double FurnaceCharges { get; set; }
        public double ExportQuantity { get; set; }
    }
}
