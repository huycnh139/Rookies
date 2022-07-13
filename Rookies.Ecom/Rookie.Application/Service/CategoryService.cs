using Microsoft.EntityFrameworkCore;
using Rookie.Application.Interface;
using Rookie.DataAccessor.Data;
using Rookie.DataAccessor.Entities;
using Rookie.Utilities.Exceptions;
using Rookie.ViewModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly EcomDbContext _ecomDbContext;
        private readonly IStorageService _storageService;
        public CategoryService(EcomDbContext ecomDbContext, IStorageService storageService)
        {
            _ecomDbContext = ecomDbContext;
            _storageService = storageService;
        }
        public async Task<int> Create(CategoryCreateRequest categoryCreateRequest)
        {
            var category = new Category()
            {
                Name = categoryCreateRequest.Name,
                Description = categoryCreateRequest.Description,
                UpdateCreate = DateTime.Now,
                DateCreate = DateTime.Now
            };
            _ecomDbContext.Categories.Add(category);
            await _ecomDbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _ecomDbContext.Categories.FindAsync(categoryId);
            if (category == null) throw new EComException($"Can not find categoryId: {categoryId}");
            _ecomDbContext.Categories.Remove(category);
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<List<CategoryDto>> GetCategoryAsync()
        {
            var query = from c in _ecomDbContext.Categories select c;
            return await query.Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DateCreate = x.DateCreate
            }).ToListAsync();
        }
        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            var category = await _ecomDbContext.Categories.FindAsync(categoryId);
            var categoryVM = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                DateCreate = category.DateCreate
                
            };
            return categoryVM;
        }

        public async Task<CategoryDto> GetCategoryByProductId(int productId)
        {
            var product = await _ecomDbContext.Products.FindAsync(productId);

            var query = from c in _ecomDbContext.Categories
                        join p in _ecomDbContext.Products on c.Id equals p.CategoryId
                        select c;
            var categoryVM = await query.Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                DateCreate = x.DateCreate
            }).FirstOrDefaultAsync();
            return categoryVM;
        }

        public async Task<int> Update(CategoryDto categoryDto)
        {
            var category = _ecomDbContext.Categories.Find(categoryDto.Id);
            if (category == null) throw new EComException($"Cannot find a category with id: {categoryDto.Id}");
            category.Name = categoryDto.Name;
            category.UpdateCreate = DateTime.Now;
            category.Description = categoryDto.Description;
            return await _ecomDbContext.SaveChangesAsync();
        }
    }
}
