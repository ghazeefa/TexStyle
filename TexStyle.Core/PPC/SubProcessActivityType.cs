using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TexStyle.Core.PPC {
    public class SubActivityType: DefaultEntity {
        public long Id { get; set; }
        public string Name { get; set; }

        public long? ActivityTypeId { get; set; }
        [ForeignKey(nameof(ActivityTypeId))]
        public virtual ActivityType ActivityType { get; set; }
    }
}
