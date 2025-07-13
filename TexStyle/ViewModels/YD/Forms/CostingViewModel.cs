using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.YD {
    public class CostingViewModel {
        [DisplayName("No")]
        public long? Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public double Electricity { get; set; }
        public double Gas { get; set; }
        [DisplayName("Salary And Wage")]
        public double SalaryAndWage { get; set; }
        [DisplayName("Furnace Charges")]
        public double FurnaceCharges { get; set; }
        public double MIS { get; set; }
        [DisplayName("Export Quantity")]
        public double ExportQuantity { get; set; }
    }
}
