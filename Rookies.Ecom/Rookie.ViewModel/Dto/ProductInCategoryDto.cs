using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Dto
{
    public class ProductInCategoryDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}
