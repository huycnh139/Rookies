using Rookie.Application.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Catalog.Products.Dtos
{
    public class ProductViewModel : PagingRequestBase
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public decimal Cost { set; get; }
        public int Stock { set; get; }
        public DateTime DateCreate { get; internal set; }
    }
}
