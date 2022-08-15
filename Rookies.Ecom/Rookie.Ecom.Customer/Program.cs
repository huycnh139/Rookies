using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Rookie.DataAccessor.Data;
using Rookie.Ecom.Customer.Api;
using Rookie.Ecom.Customer.Service;
using Rookie.ViewModel.Dto;
using Rookie.ViewModel.System.Validator;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
//Add connectionString
var connectionString = builder.Configuration.GetConnectionString("EComDataBase");
builder.Services.AddDbContext<EcomDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddTransient<IUserApiClient, UserApiClient>();
builder.Services.AddTransient<ProductDto>();
builder.Services.AddTransient<ApiRq>();
//builder.Services.AddControllersWithViews()
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Account/Forbidden/";
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoint =>
{
    endpoint.MapRazorPages();
});
app.MapRazorPages();
app.Run();
