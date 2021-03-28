using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlipShop_OnlineShopping.Models.ViewModel.RoleViewModels
{
    public class RoleListViewModel
    {
        [Required]
        public string RoleName { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public int NumberOfUsers { get; set; }
    }
}
