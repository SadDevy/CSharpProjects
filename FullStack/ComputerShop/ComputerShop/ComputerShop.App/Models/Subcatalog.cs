using System;
using System.Collections.Generic;

#nullable disable

namespace ComputerShop.App.Models
{
    public partial class Subcatalog
    {
        public Subcatalog()
        {
            Goods = new HashSet<Good>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogId { get; set; }

        public virtual Catalog Catalog { get; set; }
        public virtual ICollection<Good> Goods { get; set; }
    }
}
