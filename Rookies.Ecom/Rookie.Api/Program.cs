using Microsoft.EntityFrameworkCore;
using Rookie.DataAccessor.Data;
using Microsoft.OpenApi.Models;
using Rookie.Application.Interface;
using Rookie.Application.Service;
using Rookie.DataAccessor.Entities;
using Rookie.Application.System.Users;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//Add connectionString
var connectionString = builder.Configuration.GetConnectionString("EComDataBase");
builder.Services.AddDbContext<EcomDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, AppRole>() 
    .AddEntityFrameworkStores<EcomDbContext>();
//Declare DI
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IManagerProductService, ManagerProductService>();
builder.Services.AddTransient<IRatingService, RatingService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
builder.Services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
builder.Services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
 
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Rookie API",
        Description = "Rookie Web API",
        //TermsOfService = new Uri("https://example.com/terms"),
        //Contact = new OpenApiContact
        //{
        //    Name = "Example Contact",
        //    Url = new Uri("https://example.com/contact")
        //},
        //License = new OpenApiLicense
        //{
        //    Name = "Example License",
        //    Url = new Uri("https://example.com/license")
        //}
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
