using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Rookie.Ecom.Customer.Api;
using Rookie.ViewModel.Dto;

namespace Rookie.Ecom.Customer.Pages
{
    public class CategoryModel : PageModel
    {
        public ApiRq _client = new ApiRq();
        public List<ProductDto> _product = new List<ProductDto>();
        public List<ProductImageDto> _productImage = new List<ProductImageDto>();
        public List<RatingDto> _ratingDtos = new List<RatingDto>();
        public CategoryDto _categoryDto = new CategoryDto();
        public decimal _start = 0;
        public async Task<IActionResult> OnGetAsync(int Id)
        {
            HttpClient client = _client.ApiRequest();
            var response = await client.GetAsync($"Product/category/{Id}");
            var product = response.Content.ReadAsStringAsync().Result;
            _product = JsonConvert.DeserializeObject<List<ProductDto>>(product);

            var responseImage = await client.GetAsync($"Product/image");
            var productImage = responseImage.Content.ReadAsStringAsync().Result;
            _productImage = JsonConvert.DeserializeObject<List<ProductImageDto>>(productImage);
            return Page();

        }
    }
}
