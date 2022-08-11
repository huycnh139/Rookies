using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.ViewModel.System.Users;

namespace Rookie.Ecom.Customer.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
        []
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Page();
        }
    }
}
