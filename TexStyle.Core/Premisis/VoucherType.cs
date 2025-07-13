using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.Premisis
{
    public class VoucherType : DefaultEntity

    {
        public int Id { get; set; }
        public string Name { get; set; }                                    


        public int? MouldeId { get; set; }
        [ForeignKey(nameof(MouldeId))]
        public virtual Module Module { get; set; }





    }
}
