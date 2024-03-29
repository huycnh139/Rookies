﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class ShipDetail
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreate { set; get; }

        public DateTime DateUpdate { set; get; }

        [Phone]
        public string Phone { set; get; }

        public virtual ICollection<Address> Addresses { set; get; }
    }
}
