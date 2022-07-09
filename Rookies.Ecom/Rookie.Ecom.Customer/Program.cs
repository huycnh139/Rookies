using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Rookie.DataAccessor.Data;
using Rookie.Ecom.Customer.Api;
using Rookie.ViewModel.Dto;

var builder = WebApplication.CreateBuilder(args);

//Add connectionString
var connectionString = builder.Configuration.GetConnectionString("EComDataBase");
builder.Services.AddDbContext<EcomDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddTransient<ProductDto>();
builder.Services.AddTransient<ApiRq>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddHttpClient();

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

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoint =>
{
    endpoint.MapRazorPages();
});
app.MapRazorPages();
app.Run();
