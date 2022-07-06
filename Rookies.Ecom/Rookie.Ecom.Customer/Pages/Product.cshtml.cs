using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.Customer.Api;
using Rookie.Ecom.Customer.Models;
using Rookie.ViewModel.Dto;

namespace Rookie.Ecom.Customer.Pages
{
    public class ProductModel : PageModel
    {
        public ApiRq _client = new ApiRq();
        public Product _product = new Product();
        public async Task<Product> GetProductDtos()
        {
            var productId = 3;
            var reponse = await _client.ApiRequest().GetAsync($"Product/{productId}");
            reponse.EnsureSuccessStatusCode();
            _product.product = await reponse.Content.ReadAsAsync<Product>();
            return _product;
        }
        public async void OnGet()
        {
            
        }
    }
}
