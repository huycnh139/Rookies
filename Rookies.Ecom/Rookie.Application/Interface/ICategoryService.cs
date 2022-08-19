using Rookie.ViewModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategoryAsync();

        Task<int> CreateAsync(CategoryCreateRequest categoryCreateRequest);

        Task<CategoryDto> GetCategoryByIdAsync(int categoryId);
        
        Task<CategoryDto> GetCategoryByProductIdAsync(int productId);
        
        Task<int> UpdateAsync(CategoryDto categoryDto);

        Task<int> DeleteAsync(int categoryId);
    }
}
