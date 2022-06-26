using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }
}
