using Microsoft.AspNetCore.Http;
using Rookie.ViewModel.Catalog.Common;
using Rookie.ViewModel.Catalog.Products;
using Rookie.ViewModel.Dto;

namespace Rookie.Application.Interface
{
    public interface IManagerProductService
    {
        Task<ProductDto> GetProductByIdAsync(int productId);

        Task<ProductDto> GetProductByNameAsync(string productName);

        Task<ProductImageDto> GetProductImageDtoByIdAsync(int productImageId);

        Task<ProductImageDto> GetProductImageByNameAsync(string productName);

        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductDto request);

        Task<int> Delete(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addQuantity);

        Task AddViewCount(int productId);

        Task<int> AddImages(int productId, ImageCreateRequest request);

        Task<int> DeleteImage(int imageId);

        Task<int> UpdateImage(int imageId, ImageUpdateRequest request);

        Task<List<ProductImageDto>> GetListImage(int productId);

        Task<PageResult<ProductDto>> GetAllPaging(GetManagerProductPagingRequest request);

        Task<List<ProductImageDto>> GetAllProductImageAsync(int productId);

        Task<List<GetProductByCategoryId>> GetProductByCategoryIds(int CategoryId);
    }
}
