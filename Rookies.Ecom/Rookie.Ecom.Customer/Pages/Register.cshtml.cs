using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.Ecom.Customer.Api;
using Rookie.ViewModel.System.Users;

namespace Rookie.Ecom.Customer.Pages
{
    public class RegisterModel : PageModel
    {
        private ApiRq _api = new ApiRq();

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string lastname { get; set; }

        [BindProperty]
        public string firstname { get; set; }

        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public DateTime dob { get; set; }
        [BindProperty]
        public string phonenumber { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var UserDto = new RegisterRequest()
            {
                UserName = username,
                PassWord = password,
                LastName = lastname,
                FirstName = firstname,
                DoB = dob,
                Email = email,
                PhoneNumber = phonenumber
            };
            HttpClient client = _api.ApiRequest();
            var response = await client.PostAsJsonAsync("Users/register", UserDto);
            if ((int)response.StatusCode == 201)
            {
                return new RedirectToPageResult("Index");
            }
            return Page();
        }

    }
}
