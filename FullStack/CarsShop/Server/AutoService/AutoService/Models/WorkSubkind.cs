using System;
using System.Collections.Generic;

#nullable disable

namespace AutoService.Models
{
    public partial class WorkSubkind
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DescriptionTitle { get; set; }
        public string Description { get; set; }
        public int? WorkKindId { get; set; }

        public virtual WorkKind WorkKind { get; set; }
    }
}
