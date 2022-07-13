using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Interface;
using Rookie.ViewModel.Dto;

namespace Rookie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _catogryService;

        public CategoryController(ICategoryService catogryService)
        {
            _catogryService = catogryService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _catogryService.GetCategoryAsync();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetbyId(int categoryId)
        {
            var category = await _catogryService.GetCategoryById(categoryId);
            if (category == null) return BadRequest($"Can not find categoryId: {categoryId}");
            return Ok(category);
        }
        [HttpGet("category/{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var category = await _catogryService.GetCategoryByProductId(productId);
            if (category == null) return BadRequest($"Can not find categoryId: {productId}");
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CategoryCreateRequest categoryCreateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryId = await _catogryService.Create(categoryCreateRequest);
            if (categoryId == 0) return BadRequest();
            var category = await GetbyId(categoryId);
            return CreatedAtAction(nameof(GetbyId), new { id = categoryId }, categoryId);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _catogryService.Delete(categoryId);
            if (result == 0) return BadRequest();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _catogryService.Update(categoryDto);
            if (affectedResult == 0) return BadRequest($"Can not update category");
            return Ok();
        }
    }
}
