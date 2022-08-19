using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Rookie.DataAccessor.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime Dob { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<AppRole> AppRole { get; set; }
    }
}
