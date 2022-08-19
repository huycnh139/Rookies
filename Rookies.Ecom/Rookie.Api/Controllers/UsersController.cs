using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.System.Users;
using Rookie.DataAccessor.Data;
using Rookie.ViewModel.System.Users;

namespace Rookie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly EcomDbContext _context;

        public UsersController(EcomDbContext context,IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _context = context;
        }

        //[HttpPost("authenticate")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Authenticate([FromBody]LoginRequest request)
        //{
        //    if(!ModelState.IsValid) 
        //        return BadRequest(ModelState);

        //    var resultToken =await _userService.AuthencateAsync(request);
        //    if (string.IsNullOrEmpty(resultToken))
        //    {
        //        return BadRequest("Usernmae or password is incorrect.");
        //    }
        //    return Ok(resultToken);
        //}

        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RegisterAsync(request);
            if (!result)
            {
                return BadRequest("Register is unsuccessful.");
            }
            return Ok();
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUser()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.GetAllUserAsync();

            if (result == null)
            {
                return BadRequest("No user");
            }
            return Ok(result);

        }
        //[HttpPut("update/{id}")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await _userService.UpdateAsync(request, id);

        //    if (result == false)
        //    {
        //        return BadRequest(result);
        //    }
        //    return Ok(result);
        //}

        //private ClaimsPrincipal ValidateToken(string jwtToken)
        //{
        //    IdentityModelEventSource.ShowPII = true;

        //    SecurityToken validatedToken;
        //    TokenValidationParameters validationParameters = new TokenValidationParameters();

        //    validationParameters.ValidateLifetime = true;

        //    validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
        //    validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
        //    validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

        //    ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

        //    return principal;
        //}
        //[HttpPost("register")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public async Task<ActionResult<User>> Register(RegisterRequest request)
        //{
        //    CreatePasswordHash(request.PassWord, out byte[] passwordHash, out byte[] passwordSalt);
        //    var user = new User()
        //    {
        //        Dob = request.DoB,
        //        Email = request.Email,
        //        FirstName = request.FirstName,
        //        LastName = request.LastName,
        //        UserName = request.UserName,
        //        PhoneNumber = request.PhoneNumber,
        //        PasswordHash = passwordHash,
        //        PasswordSalt = passwordSalt
        //};
        //    await _context.Users.AddAsync(user);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetByUsername), new { username = user.UserName }, user);
        //}

        //[HttpGet("{username}")]
        //[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        ////[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetByUsername(string username)
        //{
        //    var user = await _context.Users
        //        .Where(x => x.UserName == username)
        //        .FirstOrDefaultAsync();
        //    var response = new AppUser
        //    {
        //        UserName = user.UserName,
        //        FirstName = user.FirstName,
        //        Dob = user.Dob,
        //        Email = user.Email,
        //        PhoneNumber = user.PhoneNumber
        //    };
        //    return user == null ? NotFound() : Ok(user);
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult<string>> login(LoginRequest request)
        //{
        //    var user = await _context.Users
        //        .Where(x => x.UserName == request.UserName)
        //        .FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        return BadRequest("user not found.");
        //    }

        //    if (!VerifyPasswordHash(request.PassWord, user.PasswordHash, user.PasswordSalt))
        //    {
        //        return BadRequest("wrong password");
        //    }

        //    string token = CreateToken(user);
        //    return Ok(token);
        //}

        //private string CreateToken(User user)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.FirstName)
        //    };

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        //        _configuration.GetSection("AppSettings:Token").Value));

        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration.GetSection("AppSettings:Token").Value,
        //        audience: _configuration.GetSection("AppSettings:Token").Value,
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: creds);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return jwt;
        //}

        //private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computeHash.SequenceEqual(passwordHash);
        //    }
        //}
    }
}
