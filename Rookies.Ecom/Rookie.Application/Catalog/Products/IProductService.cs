using Rookie.ViewModel.Catalog.Dto;
using Rookie.ViewModel.Catalog.Products;

namespace Rookie.Application.Catalog.Products
{
    public interface IProductService
    {
        public Task<PageResult<ProductViewModel>> GetAllByCategoryId(PublicGetProductPagingRequest request);
     } 
}
