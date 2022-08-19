using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Interface;
using Rookie.DataAccessor.Data;
using Rookie.DataAccessor.Entities;
using Rookie.ViewModel.Dto;
using Rookie.ViewModel.Catalog.Common;
using Rookie.ViewModel.Catalog.Products;
using Rookie.Utilities.Exceptions;
using System.Net.Http.Headers;
using System.IO;

namespace Rookie.Application.Service
{
    public class ManagerProductService : IManagerProductService
    {
        private readonly EcomDbContext _ecomDbContext;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ManagerProductService(EcomDbContext ecomDbContext, IStorageService storageService)
        {
            _ecomDbContext = ecomDbContext;
            _storageService = storageService;
        }

        //Create
        public async Task<int> CreateAsync(ProductCreateRequest request)
        {
            var product = new Product()
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Cost = request.Cost,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now
            };

            if (request.ImageFile != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        DateCreate = DateTime.Now,
                        IsDefualt = true,
                        ImageSize = request.ImageFile.Length,
                        ImagePath = await SaveFileAsync(request.ImageFile),
                        Name = $"https://localhost:7055/api/Product/ImageUI/"
                    }
                 
                };
            }
            
            _ecomDbContext.Products.Add(product);
            _ecomDbContext.SaveChanges();
            return product.Id;
        }
        public async Task<int> AddNewImageAsync(IFormFile imageFile, int id)
        {
            var productImage = new ProductImage()
            {
                DateCreate = DateTime.Now,
                IsDefualt = true,
                ImageSize = imageFile.Length,
                ProductId = id,
                ImagePath = await SaveFileAsync(imageFile),
            };
            if (imageFile != null)
            {
                productImage.Name = "https://localhost:7055/api/Product/ImageUI/";
                productImage.ImagePath = await SaveFileAsync(imageFile);
                productImage.ImageSize = imageFile.Length;
            }

            _ecomDbContext.ProductImages.Add(productImage);
            await _ecomDbContext.SaveChangesAsync();
            return productImage.Id;
        }
        public async Task<int> AddImagesAsync(int productId, ImageCreateRequest request)
        {
            var product = await _ecomDbContext.Products.SingleAsync(x => x.Id == productId);
            if (product == null) throw new EComException($"Cannot find a product: {productId}");
            if (product.ProductImages != null && request.IsDefualt == true)
            {
                var images = product.ProductImages.Where(x => x.IsDefualt == true);
                foreach (var image in images)
                {
                    image.IsDefualt = false;
                }
            }
            var productImage = new ProductImage()
            {
                DateCreate = DateTime.Now,
                IsDefualt = request.IsDefualt,
                ImageSize = request.ImageFile.Length,
                ProductId = productId
            };
            if (request.ImageFile != null)
            {
                productImage.Name = $"https://localhost:7055/api/Product/ImageUI/";
                productImage.ImagePath = await SaveFileAsync(request.ImageFile);
                productImage.ImageSize = request.ImageFile.Length;
            }

            _ecomDbContext.ProductImages.Add(productImage);
            await _ecomDbContext.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task AddViewCountAsync(int productId)
        {
            var product = await _ecomDbContext.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _ecomDbContext.SaveChangesAsync();
        }

        //Delete
        public async Task<int> DeleteAsync(int productId)
        {
            var product = await _ecomDbContext.Products.FindAsync(productId);
            if (product == null) throw new EComException($"Cannot find a product: {productId}");
            var images = _ecomDbContext.ProductImages.Where(x => x.ProductId == productId);
            foreach (var image in images)
            {
                if (image == null) throw new EComException("There is no image");
                _ecomDbContext.ProductImages.Remove(image);
            }
            _ecomDbContext.Products.Remove(product);
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteImageAsync(int imageId)
        {
            var image = await _ecomDbContext.ProductImages.FindAsync(imageId);
            if (image == null) throw new EComException($"Cannot find a imageid: {imageId}");
            _ecomDbContext.ProductImages.Remove(image);
            return await _ecomDbContext.SaveChangesAsync();
        }

        //Get

        public async Task<List<ProductImageDto>> GetListImageAsync(int productId)
        {
            return await _ecomDbContext.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageDto()
                {
                    Name = i.Name,
                    DateCreate = i.DateCreate,
                    ImageSize = i.ImageSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefualt = i.IsDefualt,
                    ProductId = i.ProductId,
                }).ToListAsync();
        }

        public async Task<List<GetProductByCategoryId>> GetProductByCategoryIdsAsync(int categoryId)
        {
            var Category = await _ecomDbContext.Categories.FindAsync(categoryId);
            if (Category == null) throw new EComException($"Cannot find a category: {categoryId}");
            var query = from p in _ecomDbContext.Products
                        join c in _ecomDbContext.Categories on p.CategoryId equals c.Id
                        where p.CategoryId == categoryId
                        select p
                                 ;
            var products = await query.Select(x => new GetProductByCategoryId()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price
            }).ToListAsync();
            return products;

        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _ecomDbContext.Products.FindAsync(productId);

            //var categories = await (from c in _ecomDbContext.Categories
            //                        join pic in _ecomDbContext.ProductInCategories on c.Id equals pic.CategoryId
            //                        where pic.ProductId == productId
            //                        select c.Name).ToListAsync();

            // var image = await _ecomDbContext.ProductImages.Where(x => x.ProductId == productId && x.IsDefualt == true).FirstOrDefaultAsync();

            var productViewModel = new ProductDto()
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Description = product != null ? product.Description : null,
                Name = product != null ? product.Name : null,
                Cost = product.Cost,
                Price = product.Price,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                DateCreate = DateTime.Now
                //ImageFile = image != null ? image.ImgagePath : "no-image.jpg"
            };

            return productViewModel;
        }

        public async Task<List<ProductImageDto>> GetAllProductImageAsync(int productId)
        {
            return await _ecomDbContext.ProductImages
                .Where(x => x.ProductId == productId)
                .Select(i => new ProductImageDto()
                {
                    Id = i.Id,
                    IsDefualt = i.IsDefualt,
                    ImagePath = i.ImagePath,
                    ImageSize = i.ImageSize,
                    Name = i.Name,
                    ProductId = i.ProductId
                }).ToListAsync();
        }

        public Task<ProductDto> GetProductByNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImageDto> GetProductImageByNameAsync(string productImageName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductImageDto>> GetImageAsync()
        {
            var query = from i in _ecomDbContext.ProductImages select i;
            return await query.Select(i => new ProductImageDto()
            {
                Name = i.Name,
                DateCreate = i.DateCreate,
                ImageSize = i.ImageSize,
                Id = i.Id,
                ImagePath = i.ImagePath,
                IsDefualt = i.IsDefualt,
                ProductId = i.ProductId,
            }).ToListAsync();
        }

        //Update 
        //Update 
        public async Task<int> UpdateAsync(ProductUpdateRequest request)
        {
            var product = _ecomDbContext.Products.Find(request.id);
            if (product == null) throw new EComException($"Cannot find a product with id: {request.id}");
            product.Name = request.Name;
            product.DateUpdate = DateTime.Now;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Cost = request.Cost;
            product.Stock = request.Stock;
            product.CategoryId = request.CategoryId;
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateImageAsync(int imageId, ImageUpdateRequest request)
        {
            var image = await _ecomDbContext.ProductImages.FindAsync(imageId);
            if (image == null) throw new EComException($"Cannot find a product with id: {request.Id}");
            if (request.ImageFile != null)
            {
                image.Name = $"https://localhost:7055/api/Product/ImageUI/";
                image.ImagePath = await SaveFileAsync(request.ImageFile);
                image.ImageSize = request.ImageFile.Length;
            }
            _ecomDbContext.ProductImages.Update(image);
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePriceAsync(int productId, decimal newPrice)
        {
            var product = _ecomDbContext.Products.Find(productId);
            if (product == null) throw new EComException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _ecomDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStockAsync(int productId, int addQuantity)
        {
            var product = _ecomDbContext.Products.Find(productId);
            if (product == null) throw new EComException($"Cannot find a product with id: {productId}");
            product.Stock = addQuantity;
            return await _ecomDbContext.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return '/' + USER_CONTENT_FOLDER_NAME + '/' + fileName;
        }
        //private async Task<IFormFile> OpenFile(string imagePath)
        //{
        //    IFormFile a = new;
        //    return a;
        //}
        public async Task<ProductImageDto> GetImageByIdAsync(int imageId)
        {
            var image = await _ecomDbContext.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new EComException($"Cannot find an image with id {imageId}");

            var viewModel = new ProductImageDto()
            {
                Name = image.Name,
                DateCreate = image.DateCreate,
                ImageSize = image.ImageSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefualt = image.IsDefualt,
                ProductId = image.ProductId,
            };
            return viewModel;
        }

        //Get
        public async Task<PageResult<ProductDto>> GetAllPagingAsync(GetManagerProductPagingRequest request)
        {
            var query = from p in _ecomDbContext.Products
                        join c in _ecomDbContext.Categories on p.CategoryId equals c.Id
                        where p.Name.Contains(request.keyWord)
                        select p;

            if (!string.IsNullOrEmpty(request.keyWord))
                query = query.Where(x => x.Name.Contains(request.keyWord));
            if (request.CategoryIds.Count() > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.CategoryId));
            }
            int totalRaw = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x => new ProductDto()
                            {
                                Id = x.Id,
                                CategoryId = x.CategoryId,
                                Name = x.Name,
                                Description = x.Description,
                                Price = x.Price,
                                Cost = x.Cost,
                                Stock = x.Stock
                            }).ToListAsync();
            var pagedResult = new PageResult<ProductDto>()
            {
                TotalRecord = totalRaw,
                Items = data
            };
            return pagedResult;
        }
    }
}
