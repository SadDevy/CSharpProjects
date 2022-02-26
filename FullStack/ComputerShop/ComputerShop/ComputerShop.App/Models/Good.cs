using System;
using System.Collections.Generic;

#nullable disable

namespace ComputerShop.App.Models
{
    public partial class Good
    {
        public Good()
        {
            BasketElements = new HashSet<BasketElement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public int Count { get; set; }
        public int SubcatalogId { get; set; }

        public virtual Subcatalog Subcatalog { get; set; }
        public virtual ICollection<BasketElement> BasketElements { get; set; }
    }
}
