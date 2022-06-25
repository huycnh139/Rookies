using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class User
    {
        [Key]
        public Guid Id { set; get; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }
        
        [Required]
        [StringLength(maximumLength: 50)]
        public string UserName { set; get; }

        [Required]
        [DisplayName("Date of birth")]
        public DateTime DOB { set; get; }

        [Required]
        [EmailAddress]  
        public string UserEmail { set; get; }

        [Required]
        [Phone]
        public string UserPhone { set; get; }  

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
