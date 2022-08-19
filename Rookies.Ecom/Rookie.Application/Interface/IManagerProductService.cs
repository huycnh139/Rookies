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

        Task<ProductImageDto> GetProductImageByNameAsync(string productName);

        Task<int> CreateAsync(ProductCreateRequest request);

        Task<int> UpdateAsync(ProductUpdateRequest request);

        Task<int> DeleteAsync(int productId);

        Task<bool> UpdatePriceAsync(int productId, decimal newPrice);

        Task<bool> UpdateStockAsync(int productId, int addQuantity);

        Task AddViewCountAsync(int productId);

        Task<int> AddImagesAsync(int productId, ImageCreateRequest request);

        Task<int> DeleteImageAsync(int imageId);

        Task<int> UpdateImageAsync(int imageId, ImageUpdateRequest request);

        Task<List<ProductImageDto>> GetListImageAsync(int productId);

        Task<PageResult<ProductDto>> GetAllPagingAsync(GetManagerProductPagingRequest request);

        Task<List<ProductImageDto>> GetAllProductImageAsync(int productId);

        Task<ProductImageDto> GetImageByIdAsync(int imageId);

        Task<List<GetProductByCategoryId>> GetProductByCategoryIdsAsync(int CategoryId);

        Task<List<ProductImageDto>> GetImageAsync();
    }
}
