using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Rookie.DataAccessor.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
