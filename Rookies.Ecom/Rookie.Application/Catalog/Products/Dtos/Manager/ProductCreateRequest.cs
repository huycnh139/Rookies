using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Catalog.Products.Dtos.Manager
{
    public class ProductCreateRequest
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public decimal Cost { set; get; }
        public int Stock { set; get; }
    }
}
