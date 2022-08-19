using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rookie.DataAccessor.Entities;
using Rookie.Ecom.Customer.Api;
using Rookie.ViewModel.System.Users;
using System.ComponentModel.DataAnnotations;
namespace Rookie.Ecom.Customer.Pages
{
    public class LoginModel : PageModel
    {
        /*
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public LoginModel(IUserApiClient userapiclient, IConfiguration configuration)
        {

            _userApiClient = userapiclient;
            _configuration = configuration;
        }
        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public string password { get; set; }

        public bool rememberme { get; set; } = false;

        public LoginRequest user = new LoginRequest();

        public async Task<IActionResult> OnPostAsync()
        {
            var login = new LoginRequest
            {
                UserName = username,
                PassWord = password,
                Rememberme = rememberme,
            };
            var token = await _userApiClient.Authenticate(login);
            var userPrincipal = this.ValidateToken(token);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };
            //HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);
            //HttpContext.Session.SetString(SystemConstants.AppSettings.Token, result.ResultObj);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        userPrincipal,
                        authProperties);
            return Page();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Page();
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
        */
        public ApiRq _api = new ApiRq();

        [BindProperty]
            public string username { get; set; }

            [BindProperty]
            public string password { get; set; }

            public bool Toast { get; set; } = false;

            public UserResponseDto user = new UserResponseDto();

            public async Task<IActionResult> OnPostAsync()
            {
                var UserDto = new LoginRequest
                {
                    UserName = username,
                    PassWord = password,
                };

                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);

                HttpClient client = _api.ApiRequest();
                var response = await client.PostAsJsonAsync("Users/login", UserDto);
                var result = response.Content.ReadAsStringAsync().Result;
                if ((int)response.StatusCode == 200)
                {
                    var responseGetUser = await client.GetAsync($"Users/{UserDto.UserName}");
                    var resultGetUser = responseGetUser.Content.ReadAsStringAsync().Result;
                    Response.Cookies.Append("access_token", result, options);
                    Response.Cookies.Append("user", resultGetUser, options);
                    return new RedirectToPageResult("Account");
                }
                return Page();
            }


        
    }
}
