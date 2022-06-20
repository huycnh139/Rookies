﻿using System;
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
        public Guid Id { set; get; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }
    
        public DateTime OrderDate { set; get; }

        public virtual ICollection<OrderItem> OrderItems { set; get; }
        public virtual Customer Customer { set; get; }
        public virtual ICollection<ShipDetail> ShipDetails { set; get; }
    }
}