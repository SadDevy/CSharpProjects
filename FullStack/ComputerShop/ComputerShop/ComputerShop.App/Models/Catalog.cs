using System;
using System.Collections.Generic;

#nullable disable

namespace ComputerShop.App.Models
{
    public partial class Catalog
    {
        public Catalog()
        {
            Subcatalogs = new HashSet<Subcatalog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Subcatalog> Subcatalogs { get; set; }
    }
}
