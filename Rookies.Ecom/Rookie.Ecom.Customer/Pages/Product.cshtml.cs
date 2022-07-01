using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.DataAccessor.Entities;
using Rookie.Ecom.Customer.Api;
using Rookie.ViewModel.Dto;

namespace Rookie.Ecom.Customer.Pages
{
    public class ProductModel : PageModel
    {
        public ApiRq _client = new ApiRq();

        public async Task<List<ProductDto>> GetProductDtos()
        {
            var reponse = await _client.ApiRequest().GetAsync("Product");
            reponse.EnsureSuccessStatusCode();
            var product = await reponse.Content.ReadAsAsync<List<ProductDto>>();
            return product;
        }
        public async void OnGet()
        {
            
        }
    }
}
