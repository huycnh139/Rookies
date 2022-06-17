using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAssessor.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { set; get; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }

        [Required]
        [DisplayName("Product Name")]
        public string Name { set; get; }

        public string Description { set; get; }

        [Required]
        public decimal Price { set; get; }
        
        [Required]               
        public decimal Cost { set; get; }
        
        public bool isFeatured { set; get; }

        public virtual ICollection<ProductImage> ProductImages { set; get; }
        public virtual ICollection<Rating> Ratings { set; get; }
        public virtual ICollection<OrderItem> OrderItems { set; get; }
        public virtual Category Category { set; get; }
    }
}
