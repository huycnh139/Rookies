using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class Product
    {
        [Key]
        public int Id { set; get; }
      
        public DateTime DateCreate { set; get; }

        public DateTime DateUpdate { set; get; }

        public int CategoryId { set; get; }

        [Required]
        [DisplayName("Product Name")]
        public string Name { set; get; }

        [Required]
        [StringLength(maximumLength:200)]
        public string Description { set; get; }

        [DefaultValue(0)]
        [Required]
        public decimal Price { set; get; }
        [DefaultValue(0)]
        [Required]               
        public decimal Cost { set; get; }

        [Required]
        [DefaultValue(0)]
        public int Stock { set; get; }

        public int ViewCount { get; set; }

        public bool isFeatured { set; get; }
        
        public virtual ICollection<ProductImage> ProductImages { set; get; }
        public virtual ICollection<Rating> Ratings { set; get; }
        public virtual ICollection<ProductDetail> ProductDetails { set; get; }
        //public virtual ICollection<OrderItem> OrderItems { set; get; }
    }
}
