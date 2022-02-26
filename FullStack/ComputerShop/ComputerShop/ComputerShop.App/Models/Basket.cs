using System;
using System.Collections.Generic;

#nullable disable

namespace ComputerShop.App.Models
{
    public partial class Basket
    {
        public int UserId { get; set; }
        public int? BasketEmentId { get; set; }

        public virtual BasketElement BasketEment { get; set; }
        public virtual User User { get; set; }
    }
}
