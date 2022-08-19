using Microsoft.AspNetCore.Http;
using Rookie.ViewModel.Catalog.Common;

namespace Rookie.ViewModel.Dto
{
    public class ProductUpdateRequest
    {
        public int id { set; get; }
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public decimal Cost { set; get; }
        public int Stock { set; get; }
        public DateTime? DateUpdate { get; set; }

    }
}
