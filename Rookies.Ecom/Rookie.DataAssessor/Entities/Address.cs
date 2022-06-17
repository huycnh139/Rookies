using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAssessor.Entities
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string Ward { get; set; }

        [Required]
        public string Note { get; set; }    
    }
}
