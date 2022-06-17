using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { set; get; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }
        
        [Required]
        [StringLength(maximumLength: 50)]
        public string CustomerName { set; get; }

        [Required]
        [DisplayName("Date of birth")]
        public DateTime DOB { set; get; }

        [Required]
        [EmailAddress]  
        public string CustomerEmail { set; get; }

        [Required]
        [Phone]
        public string CustomerPhone { set; get; }  

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
