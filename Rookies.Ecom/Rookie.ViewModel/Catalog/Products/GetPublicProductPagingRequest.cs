using Rookie.ViewModel.Catalog.Dto;

namespace Rookie.ViewModel.Catalog.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public string keyWord { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
