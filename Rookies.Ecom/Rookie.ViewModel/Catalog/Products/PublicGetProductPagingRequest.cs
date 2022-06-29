using Rookie.ViewModel.Catalog.Common;

namespace Rookie.ViewModel.Catalog.Products
{
    public class PublicGetProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
