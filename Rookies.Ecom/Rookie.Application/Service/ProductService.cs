using Microsoft.EntityFrameworkCore;
using Rookie.Application.Interface;
using Rookie.DataAccessor.Data;
using Rookie.ViewModel.Catalog.Common;
using Rookie.ViewModel.Catalog.Products;
using Rookie.ViewModel.Dto;

namespace Rookie.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly EcomDbContext _ecomDbContext;
        public ProductService(EcomDbContext ecomDbContext)
        {
            _ecomDbContext = ecomDbContext;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var query = from p in _ecomDbContext.Products
                        select p;
            var data = await query.Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Price = x.Price,
                Cost = x.Cost,
                Stock = x.Stock,
                DateCreate = x.DateCreate,
                DateUpdate = x.DateUpdate
            }).ToListAsync();
            return data;
        }

        public async Task<PageResult<ProductDto>> GetAllByCategoryIdAsync(PublicGetProductPagingRequest request)
        {
            var query = from p in _ecomDbContext.Products
                        join c in _ecomDbContext.Categories on p.CategoryId equals c.Id
                        select p;
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId);
            }
            int totalRaw = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x => new ProductDto()
                            {
                                Id = x.Id,
                                Name = x.Name,
                                CategoryId = x.CategoryId,
                                Description = x.Description,
                                Price = x.Price,
                                Cost = x.Cost,
                                Stock = x.Stock

                            }).ToListAsync();
            var pagedResult = new PageResult<ProductDto>()
            {
                TotalRecord = totalRaw,
                Items = data
            };
            return pagedResult;
        }
    }
}
