using System;
using System.Collections.Generic;

#nullable disable

namespace AutoService.Models
{
    public partial class WorkKind
    {
        public WorkKind()
        {
            WorkSubkinds = new HashSet<WorkSubkind>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DescriptionTitle { get; set; }
        public string Description { get; set; }

        public virtual ICollection<WorkSubkind> WorkSubkinds { get; set; }
    }
}
