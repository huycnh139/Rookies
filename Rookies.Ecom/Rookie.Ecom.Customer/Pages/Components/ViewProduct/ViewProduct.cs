using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rookie.Ecom.Customer.Api;
using Rookie.ViewModel.Dto;

namespace Rookie.Ecom.Customer.Pages.Components.ViewProduct
{
    public class ViewProduct : ViewComponent
    {
        public ApiRq _client = new ApiRq();
        public ProductDto _product;
        public ViewProduct(ProductDto product)
        {   
            _product = product;
        }
        public async Task<ProductDto> OnGetAsync(int productId)
        {
            HttpClient client = _client.ApiRequest();
            var response = await client.GetAsync($"Product/{productId}");
            var product = response.Content.ReadAsStringAsync().Result;
            return _product = JsonConvert.DeserializeObject<ProductDto>(product);
        }
        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            _product = await OnGetAsync(productId);
            return View<string>(_product.ToString());
        }
    }
}
