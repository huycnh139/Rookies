using Microsoft.EntityFrameworkCore;
using Rookie.DataAccessor.Data;
using Microsoft.OpenApi.Models;
using Rookie.Application.Interface;
using Rookie.Application.Service;

var builder = WebApplication.CreateBuilder(args);

//Add connectionString
var connectionString = builder.Configuration.GetConnectionString("EComDataBase");
builder.Services.AddDbContext<EcomDbContext>(x => x.UseSqlServer(connectionString));

//Declare DI
builder.Services.AddTransient<IStorageService, FileStorageService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IManagerProductService, ManagerProductService>();
builder.Services.AddTransient<IRatingService, RatingService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
