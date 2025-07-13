using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TexStyle.Identity.Extensions.DTO;

namespace TexStyle.Core.PPC
{
    public class Dia : DefaultEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
