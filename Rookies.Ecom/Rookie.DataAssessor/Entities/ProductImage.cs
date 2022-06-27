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
        public int Id { get; set; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }

        [Required]
        [StringLength(maximumLength:50)]
        public string Name { set; get; }
        [Required]
        public string ImgagePath { set; get; }
    }
}
