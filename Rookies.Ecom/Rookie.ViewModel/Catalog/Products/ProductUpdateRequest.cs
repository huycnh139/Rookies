using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Catalog.Products
{
    public class ProductUpdateRequest
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Description { set; get; }
        public IFormFile ThumbnailImage { set; get; }
    }
}
