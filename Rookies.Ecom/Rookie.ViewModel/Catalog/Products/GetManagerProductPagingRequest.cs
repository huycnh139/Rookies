using Rookie.ViewModel.Catalog.Dto;

namespace Rookie.ViewModel.Catalog.Products
{
    public class GetManagerProductPagingRequest : PagingRequestBase
    {
        public string keyWord { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
