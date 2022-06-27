using Microsoft.EntityFrameworkCore;
using Rookie.Application.Catalog.Dto;
using Rookie.Application.Catalog.Products.Dtos;
using Rookie.Application.Catalog.Products.Dtos.Manager;
using Rookie.DataAccessor.Data;
using Rookie.DataAccessor.Entities;
using Rookie.Ubitilit.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Catalog.Products
{
    public class ManagerProductService : IManagerProductService
    {
        private readonly EcomDbContext _ecomDbContext;
        public ManagerProductService(EcomDbContext ecomDbContext)
        {
            _ecomDbContext = ecomDbContext;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _ecomDbContext.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                Cost = request.Cost,
                Stock = request.Stock,
                ViewCount =0,
                DateCreate = DateTime.Now
            };
            _ecomDbContext.Products.Add(product);
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _ecomDbContext.Products.FindAsync(productId);
            if (product == null) throw new EComException($"Cannot find a product: {productId}");
            _ecomDbContext.Products.Remove(product);
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            var query = from p in _ecomDbContext.Products
                        join pic in _ecomDbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _ecomDbContext.Categories on pic.CategoryId equals c.Id
                        where p.Name.Contains(request.keyWord)
                        select new { p, pic };

            if (!string.IsNullOrEmpty(request.keyWord))
                query = query.Where(x => x.p.Name.Contains(request.keyWord));
            if (request.CategoryIds.Count() > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            int totalRaw = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x => new ProductViewModel()
                            {
                                Id = x.p.Id,
                                Name = x.p.Name,
                                DateCreate = x.p.DateCreate, 
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

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = _ecomDbContext.Products.Find(request.id);
            if(product == null)  throw new EComException($"Cannot find a product with id: {request.id}");
            product.Name = request.Name;
            product.UpdateCreate = DateTime.Now;
            product.Description = request.Description;
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = _ecomDbContext.Products.Find(productId);
            if (product == null) throw new EComException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _ecomDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addeQuantity)
        {
            var product = _ecomDbContext.Products.Find(productId);
            if (product == null) throw new EComException($"Cannot find a product with id: {productId}");
            product.Stock = addeQuantity;
            return await _ecomDbContext.SaveChangesAsync() > 0;
        }

    }
}
