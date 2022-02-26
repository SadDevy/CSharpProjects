using System;
using System.Collections.Generic;

#nullable disable

namespace AutoService.Models
{
    public partial class User
    {
        public string Lgn { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
