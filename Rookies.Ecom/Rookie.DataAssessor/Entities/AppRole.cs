using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public AppUser AppUser { get; set; }     

    }
}
