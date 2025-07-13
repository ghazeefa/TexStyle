using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.Gate
{
    public class GateIGPViewModel
    {
        public long Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string DriverName { get; set; }
        public string VehicleNo { get; set; }
        public string NICNo { get; set; }
        public string EmpNo { get; set; }
        [Required]
        public long PartyId { get; set; }
        [Required]
        public long GateActivityTypeId { get; set; }

    }
}
