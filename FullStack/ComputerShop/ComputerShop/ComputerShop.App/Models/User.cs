using System;
using System.Collections.Generic;

#nullable disable

namespace ComputerShop.App.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Basket Basket { get; set; }
    }
}
