using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Catalog.Products.Dtos.Manager
{
    public class ProductUpdateRequest
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Description { set; get; }

    }
}
