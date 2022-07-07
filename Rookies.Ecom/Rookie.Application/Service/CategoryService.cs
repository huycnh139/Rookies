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
        public async Task<int> Create(CategoryDto categoryDto)
        {
            var category = new Category()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                DateCreate = DateTime.Now
            };
            _ecomDbContext.Categories.Add(category);
            await _ecomDbContext.SaveChangesAsync();
            return category.Id;
        }

        public async Task<List<CategoryDto>> GetCategoryAsync()
        {
            var query = from c in _ecomDbContext.Categories select c;
            return await query.Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();
        }
        public async Task<CategoryDto> GetCategoryById(int categoryId)
        {
            var category = await _ecomDbContext.Categories.FindAsync(categoryId);
            var categoryVM = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return categoryVM;
        }
    }
}
