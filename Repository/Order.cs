using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }
        public ShopUser Customer { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
    }
}
