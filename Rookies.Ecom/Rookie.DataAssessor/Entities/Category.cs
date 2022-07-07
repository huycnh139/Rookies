using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class Category
    {
        [Key]
        public int Id { set; get; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { set; get; }

        [StringLength(maximumLength: 200)]
        public string? Description { set; get; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
