using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ShopUser : IdentityUser
    {
        public string FullName { get; set; }
        public override string UserName { get; set; }
        public string AddressDelivery { get; set; }
    }
}
