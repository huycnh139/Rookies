﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ProductDto>> GetAll()
        {
            var query = from p in _ecomDbContext.Products
                        select p;
            var data = await query.Select(x => new ProductDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Cost = x.Cost,
                Stock = x.Stock
            }).ToListAsync();
            return data;
        }

        public async Task<PageResult<ProductDto>> GetAllByCategoryId(PublicGetProductPagingRequest request)
        {
            var query = from p in _ecomDbContext.Products
                        join pic in _ecomDbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _ecomDbContext.Categories on pic.CategoryId equals c.Id
                        select new { p, pic };
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }
            int totalRaw = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x => new ProductDto()
                            {
                                Id = x.p.Id,
                                Name = x.p.Name,
                                Description = x.p.Description,
                                Price = x.p.Price,
                                Cost = x.p.Cost,
                                Stock = x.p.Stock

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
