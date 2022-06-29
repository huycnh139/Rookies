using Microsoft.AspNetCore.Http;
using Rookie.ViewModel.Catalog.Common;
using Rookie.ViewModel.Catalog.Products;
using Rookie.ViewModel.Dto;

namespace Rookie.Application.Catalog.Products
{
    public interface IManagerProductService
    {
        Task<ProductDto> GetProductByIdAsync(int productId);

        Task<ProductDto> GetProductByNameAsync(string productName);
        
        Task<ProductImageDto> GetProductImageDtoByIdAsync(int productImageId);
        
        Task<ProductImageDto> GetProductImageByNameAsync(string productName);

        Task<int> Create(ProductDto request);

        Task<int> Update(ProductDto request);

        Task<int> Delete(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId,int addeQuantity);

        Task AddViewCount(int productId);

        Task<int> AddImages(int productId, ProductImageDto request);

        Task<int> DeleteImage(int imageId);

        Task<int> UpdateImage(ProductImageDto request);

        Task<List<ProductImageDto>> GetListImage(int productId);

        Task<PageResult<ProductDto>> GetAllPaging(GetManagerProductPagingRequest request);

    }
}
