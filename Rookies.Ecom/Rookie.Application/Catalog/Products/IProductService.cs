using Rookie.ViewModel.Catalog.Common;
using Rookie.ViewModel.Catalog.Products;
using Rookie.ViewModel.Dto;

namespace Rookie.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<PageResult<ProductDto>> GetAllByCategoryId(PublicGetProductPagingRequest request);

        Task<List<ProductDto>> GetAll();
     } 
}
