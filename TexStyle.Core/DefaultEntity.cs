using System;
using System.Collections.Generic;
using System.Text;

namespace TexStyle.Core {
    public class DefaultEntity {

        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
