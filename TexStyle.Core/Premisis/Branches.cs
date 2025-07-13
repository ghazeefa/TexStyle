using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.Premisis {
    public class Branches : DefaultEntity {
        
        public int Id { get; set; }
        public string Name { get; set; }


    }
}
