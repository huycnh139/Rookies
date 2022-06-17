using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
