using Rookie.ViewModel.Catalog.Common;
using Rookie.ViewModel.Catalog.Products;
using Rookie.ViewModel.Dto;

namespace Rookie.Application.Catalog.Products
{
    public interface IProductService
    {
        public Task<PageResult<ProductDto>> GetAllByCategoryId(PublicGetProductPagingRequest request);
     } 
}
