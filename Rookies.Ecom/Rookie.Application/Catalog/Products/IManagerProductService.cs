using Microsoft.AspNetCore.Http;
using Rookie.ViewModel.Catalog.Dto;
using Rookie.ViewModel.Catalog.ProductImage;
using Rookie.ViewModel.Catalog.Products;

namespace Rookie.Application.Catalog.Products
{
    public interface IManagerProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId,int addeQuantity);

        Task AddViewCount(int productId);

        Task<int> AddImages(int productId, ProductImageCreateRequest request);

        Task<int> DeleteImage(int imageId);

        Task<int> UpdateImage(ProductImageUpdateRequest request);

        Task<List<ProductImageViewModel>> GetListImage(int productId);

        Task<PageResult<ProductViewModel>> GetAllPaging(GetManagerProductPagingRequest request);

    }
}
