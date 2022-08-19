using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rookie.DataAccessor.Data;
using Rookie.DataAccessor.Entities;
using Rookie.ViewModel.System.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rookie.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly EcomDbContext _ecomDbContext;
        public UserService(EcomDbContext ecomDbContext,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _ecomDbContext = ecomDbContext;

        }

        public async Task<string> AuthencateAsync(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null) return null;
            var result = await _signInManager.PasswordSignInAsync(user, request.PassWord, request.Rememberme, true);
            if (!result.Succeeded)
            {
                return null ;
            }

            var roles = _userManager.GetRolesAsync(user) ;
            var claims = new[]
            {
                 new Claim(ClaimTypes.Email,user.Email),
                 new Claim(ClaimTypes.GivenName,user.FirstName),
                 new Claim(ClaimTypes.Role, string.Join(";", roles)),
                 new Claim(ClaimTypes.Name,user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Token:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        public async Task<List<UserResponseDto>> GetAllUserAsync()
        {
            var query = from u in _ecomDbContext.Users
                        select u;
            var users = await query.Select(x => new UserResponseDto()
            {
                Dob = x.Dob,
                UserName = x.UserName,
                FirstName = x.FirstName,
                Email = x.Email
            }).ToListAsync();
            return users;
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                Dob = request.DoB,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.PassWord);
            if (result.Succeeded)
            {
                return true;  
            }
            return false;
        }

        //DOB, Gender, Joined date, Type
        public async Task<bool> UpdateAsync(UserUpdateRequest request, Guid userId)
        {

            throw new NotImplementedException();
        }

    }
}

