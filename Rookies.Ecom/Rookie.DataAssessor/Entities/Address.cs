using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Ward { get; set; }

        public string Note { get; set; }    

        public virtual AppUser User { get; set; }  
        public virtual ShipDetail ShipDetail { get; set; }
    }
}
