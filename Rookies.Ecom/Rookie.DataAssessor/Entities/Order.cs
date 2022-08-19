using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class Order
    {
        [Key]
        public int Id { set; get; }

        public DateTime DateCreate { set; get; }

        public DateTime DateUpdate { set; get; }
        [Required]
        public DateTime OrderDate { set; get; }

        public virtual ICollection<OrderItem> OrderItems { set; get; }
        public virtual ICollection<ShipDetail> ShipDetails { set; get; }
    }
}
