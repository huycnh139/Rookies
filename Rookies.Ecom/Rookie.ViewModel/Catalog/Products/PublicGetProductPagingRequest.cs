using Rookie.ViewModel.Catalog.Dto;

namespace Rookie.ViewModel.Catalog.Products
{
    public class PublicGetProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
