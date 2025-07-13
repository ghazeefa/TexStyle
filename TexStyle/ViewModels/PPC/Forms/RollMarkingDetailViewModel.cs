using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TexStyle.ViewModels.PPC.Forms
{
    public class RollMarkingDetailViewModel
    {


        //public long Id { get; set; }

        //[DisplayName("RollNo")]
        //public decimal RollNo { get; set; }

        //[DisplayName("EcruKgs")]
        //public long EcruKgs { get; set; }

        //[DisplayName("DyedKgs")]

        //public long DyedKgs { get; set; }

        //[DisplayName("Status")]
        //public string Status { get; set; }
        //[DisplayName("RollMarking Id")]
        //public long? RollMarkingId { get; set; }

        public long Id { get; set; }
        public decimal RollNo { get; set; }
        public long EcruKgs { get; set; }
        public long DyedKgs { get; set; }


        public string Status { get; set; }

        public long? RollMarkingId { get; set; }


    }
}
