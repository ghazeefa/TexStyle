using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Common {
    public class PagingOptions {

        public int? Offset { get; set; }
        public int? Limit { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string GroupBy { get; set; }
    }
}
