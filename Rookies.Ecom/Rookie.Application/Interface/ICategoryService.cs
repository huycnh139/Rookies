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

        Task<int> Create(CategoryCreateRequest categoryCreateRequest);

        Task<CategoryDto> GetCategoryById(int categoryId);
        Task<CategoryDto> GetCategoryByProductId(int productId);
    }
}
