using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Rookie.Ecom.Customer.Api;
using Rookie.Ecom.Customer.Models;

namespace Rookie.Ecom.Customer.Pages.ProductPage
{
    public class ProductModel : PageModel
    {
        public ApiRq _client = new ApiRq();
        public List<ProductVM> _product = new List<ProductVM>();
        
        public async Task<IActionResult> OnGetAsync()
        {
 //           var productId = 2;
            HttpClient client = _client.ApiRequest();
            var response = await client.GetAsync("Product");
            var product = response.Content.ReadAsStringAsync().Result;
            _product = JsonConvert.DeserializeObject<List<ProductVM>>(product);
            return Page();
        }
    }
}
