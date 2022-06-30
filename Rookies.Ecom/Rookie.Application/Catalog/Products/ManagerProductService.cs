using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rookie.Application.Catalog.Common;
using Rookie.DataAccessor.Data;
using Rookie.DataAccessor.Entities;
using Rookie.ViewModel.Dto;
using Rookie.ViewModel.Catalog.Common;
using Rookie.ViewModel.Catalog.Products;
using Rookie.Utilities.Exceptions;
using System.Net.Http.Headers;

namespace Rookie.Application.Catalog.Products
{
    public class ManagerProductService : IManagerProductService
    {
        private readonly EcomDbContext _ecomDbContext;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ManagerProductService(EcomDbContext ecomDbContext)
        {
            _ecomDbContext = ecomDbContext;
        }

        public async Task<int> AddImages(int productId,ImageCreateRequest request)
        {
            var product = await _ecomDbContext.Products.SingleAsync(x => x.Id == productId);
            if (product == null) throw new EComException($"Cannot find a product: {productId}");
            if(product.ProductImages!= null && request.IsDefualt == true) 
            {
                var images = product.ProductImages.Where(x => x.IsDefualt == true);
                foreach(var image in images)
                {
                    image.IsDefualt = false;
                }
            }
            var productImage = new ProductImage()
            {
                Name = request.Name,
                DateCreate = DateTime.Now,
                IsDefualt = request.IsDefualt,
                ImageSize = request.ImageFile.Length,
                ProductId = productId
            };
            if (request.ImageFile != null)
            {
                productImage.ImgagePath = await this.SaveFile(request.ImageFile);
                productImage.ImageSize = request.ImageFile.Length;
            }
            _ecomDbContext.ProductImages.Add(productImage);
            await _ecomDbContext.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _ecomDbContext.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _ecomDbContext.SaveChangesAsync();
        }
        
        //Create
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Cost = request.Cost,
                Stock = request.Stock,
                ViewCount =0,
                DateCreate = DateTime.Now
            };

            if (request.ImageFile != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Name = "New Image",
                        DateCreate = DateTime.Now,
                        IsDefualt = true,
                        ImageSize = request.ImageFile.Length,
                        ImgagePath = await this.SaveFile(request.ImageFile)
                    }
                };
             }
            _ecomDbContext.Products.Add(product);
            await _ecomDbContext.SaveChangesAsync();
            return product.Id;
        }

        //Delete
        public async Task<int> Delete(int productId)
        {
            var product = await _ecomDbContext.Products.FindAsync(productId);
            if (product == null) throw new EComException($"Cannot find a product: {productId}");
            var images = _ecomDbContext.ProductImages.Where(x => x.ProductId == productId); 
            foreach(var image in images)
            {
                if (image == null) throw new EComException("There is no image");
                _ecomDbContext.ProductImages.Remove(image);
            }
            _ecomDbContext.Products.Remove(product);
            return await _ecomDbContext.SaveChangesAsync();
        }

        public  async Task<int> DeleteImage(int imageId)
        {
            var image = await _ecomDbContext.ProductImages.FindAsync(imageId);
            if (image == null) throw new EComException($"Cannot find a imageid: {imageId}");
            _ecomDbContext.ProductImages.Remove(image);
            return await _ecomDbContext.SaveChangesAsync();
        }

        //Get All Produce 
        public async Task<PageResult<ProductDto>> GetAllPaging(GetManagerProductPagingRequest request)
        {
            var query = from p in _ecomDbContext.Products
                        join pic in _ecomDbContext.ProductInCategories on p.Id equals pic.ProductId
                        join c in _ecomDbContext.Categories on pic.CategoryId equals c.Id
                        where p.Name.Contains(request.keyWord)
                        select new { p, pic };

            if (!string.IsNullOrEmpty(request.keyWord))
                query = query.Where(x => x.p.Name.Contains(request.keyWord));
            if (request.CategoryIds.Count() > 0)
            {
                query = query.Where(p => request.CategoryIds.Contains(p.pic.CategoryId));
            }
            int totalRaw = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x => new ProductDto()
                            {
                                Id = x.p.Id,
                                Name = x.p.Name,
                                Description = x.p.Description,
                                Price = x.p.Price,
                                Cost = x.p.Cost,
                                Stock = x.p.Stock

                            }).ToListAsync();
            var pagedResult = new PageResult<ProductDto>()
            {
                TotalRecord = totalRaw,
                Items = data
            };
            return pagedResult;
        }

        public async Task<List<ProductImageDto>> GetListImage(int productId)
        {
            return await _ecomDbContext.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageDto()
                {
                    Name = i.Name,
                    DateCreate = i.DateCreate,
                    ImageSize = i.ImageSize,
                    Id = i.Id,
                    ImgagePath = i.ImgagePath,
                    IsDefualt = i.IsDefualt,
                    ProductId = i.ProductId,
                }).ToListAsync();
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

        public Task<ProductDto> GetProductByNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImageDto> GetProductImageByNameAsync(string productName)
        {
            throw new NotImplementedException();
        }

        public Task<ProductImageDto> GetProductImageDtoByIdAsync(int productImageId)
        {
            throw new NotImplementedException();
        }

        //Update 
        public async Task<int> Update(ProductDto request)
        {
            var product = _ecomDbContext.Products.Find(request.Id);
            if(product == null)  throw new EComException($"Cannot find a product with id: {request.Id}");
            product.Name = request.Name;
            product.UpdateCreate = DateTime.Now;
            product.Description = request.Description;

            if (request.ProductImageDto != null)
            {
                var thumbnailImage = await _ecomDbContext.ProductImages.FirstOrDefaultAsync(x => x.IsDefualt == true && x.ProductId == request.Id);
                if(thumbnailImage!= null)
                {
                    thumbnailImage.ImageSize = request.ProductImageDto.ImageFile.Length;
                    thumbnailImage.ImgagePath = await this.SaveFile(request.ProductImageDto.ImageFile);
                    _ecomDbContext.ProductImages.Update(thumbnailImage);
                }
            }
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId,ImageUpdateRequest request)
        {
            var image = await _ecomDbContext.ProductImages.FindAsync(imageId);
            if (image == null) throw new EComException($"Cannot find a product with id: {request.Id}");
            if (request.ImgagePath != null)
            {
                image.ImgagePath = await this.SaveFile(request.ImageFile);
                image.ImageSize = request.ImageFile.Length;
            }
            _ecomDbContext.ProductImages.Update(image);
            return await _ecomDbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = _ecomDbContext.Products.Find(productId);
            if (product == null) throw new EComException($"Cannot find a product with id: {productId}");
            product.Price = newPrice;
            return await _ecomDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addQuantity)
        {
            var product = _ecomDbContext.Products.Find(productId);
            if (product == null) throw new EComException($"Cannot find a product with id: {productId}");
            product.Stock = addQuantity;
            return await _ecomDbContext.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}
