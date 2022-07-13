using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Rookie.Ecom.Customer.Api;
using Rookie.Ecom.Customer.Models;
using Rookie.ViewModel.Dto;
using System;

namespace Rookie.Ecom.Customer.Pages
{
    public class ProductModel : PageModel
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
            var response = await client.GetAsync("Product");
            var product = response.Content.ReadAsStringAsync().Result;
            _product = JsonConvert.DeserializeObject<List<ProductDto>>(product);

            var responseImage = await client.GetAsync($"Product/{Id}/Image");
            var productImage = responseImage.Content.ReadAsStringAsync().Result;
            _productImage = JsonConvert.DeserializeObject<List<ProductImageDto>>(productImage);

            var responseCategory = await client.GetAsync($"Category/category/{Id}");
            var category = responseCategory.Content.ReadAsStringAsync().Result;
            _categoryDto = JsonConvert.DeserializeObject<CategoryDto>(category);

            var responseRating = await client.GetAsync($"Rating/{Id}/product-rating");
            var ratingDtos = responseRating.Content.ReadAsStringAsync().Result;
            _ratingDtos = JsonConvert.DeserializeObject<List<RatingDto>>(ratingDtos);

            //var responseStar = await client.GetAsync($"Rating/star/{Id}");
            //var star = responseStar.Content.ReadAsStringAsync().Result;
            //_start = JsonConvert.DeserializeObject<decimal>(star);
            return Page();
        }
    }
}
