using Microsoft.EntityFrameworkCore;
using Rookie.DataAccessor.Data;
using Rookie.ViewModel.Catalog.Dto;
using Rookie.ViewModel.Catalog.Products;

namespace Rookie.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly EcomDbContext _ecomDbContext;
        public ProductService(EcomDbContext ecomDbContext)
        {
            _ecomDbContext = ecomDbContext;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(PublicGetProductPagingRequest request)
        {
            var query = from p in _ecomDbContext.Products
                        join pic in _ecomDbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _ecomDbContext.Categories on pic.CategoryId equals c.Id
                        select new { p, pic };
            if (request.CategoryId.HasValue && request.CategoryId.Value>0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }
            int totalRaw = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x => new ProductViewModel()
                            {
                                Id = x.p.Id,
                                Name = x.p.Name,
                                Description = x.p.Description,
                                Price = x.p.Price,
                                Cost = x.p.Cost,
                                Stock = x.p.Stock

                            }).ToListAsync();
            var pagedResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRaw,
                Items = data
            };
            return pagedResult;
        }
    }
}
