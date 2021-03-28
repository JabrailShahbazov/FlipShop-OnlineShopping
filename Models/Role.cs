using Microsoft.AspNetCore.Identity;
using System;

namespace FlipShop_OnlineShopping.Models
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
    }

}