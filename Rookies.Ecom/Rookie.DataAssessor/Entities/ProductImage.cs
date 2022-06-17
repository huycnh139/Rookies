using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }

        [Required]
        [StringLength(maximumLength:50)]
        public string Name { set; get; }

        public string Url { set; get; }

        public virtual Product Product { set; get; }

    }
}
