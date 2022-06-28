using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Catalog.ProductImage
{
    public class ProductImageCreateRequest
    {
        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public IFormFile ImageFile { get; set; }

        public int ProductId { get; set; }
    }
}
