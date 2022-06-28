using Rookie.ViewModel.Catalog.Dto;

namespace Rookie.ViewModel.Catalog.Products
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
