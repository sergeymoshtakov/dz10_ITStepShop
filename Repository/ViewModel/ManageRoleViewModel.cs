using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel
{
    public class ManageRoleViewModel
    {
        public string UserId { get; set; }
        public List<RoleSelectionViewModel> Roles { get; set; }
        public List<ShopUser> Users { get; set; }
    }

    public class RoleSelectionViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
