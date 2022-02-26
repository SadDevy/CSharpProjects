using System;
using System.Collections.Generic;

#nullable disable

namespace ComputerShop.App.Models
{
    public partial class BasketElement
    {
        public BasketElement()
        {
            Baskets = new HashSet<Basket>();
        }

        public int Id { get; set; }
        public int Count { get; set; }
        public int GoodsId { get; set; }

        public virtual Good Goods { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
    }
}
