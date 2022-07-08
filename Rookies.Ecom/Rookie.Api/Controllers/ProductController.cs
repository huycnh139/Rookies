using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Interface;
using Rookie.ViewModel.Catalog.Products;
using Rookie.ViewModel.Dto;

namespace Rookie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IManagerProductService _managerProductService;
        private readonly IStorageService _storageService;

        public ProductController(IProductService productService, IManagerProductService managerProductService,IStorageService storageService)
        {
            _storageService = storageService;
            _productService = productService;
            _managerProductService = managerProductService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductByCategoryId(int categoryId)
        {
            var products = await _managerProductService.GetProductByCategoryIds(categoryId);
            return Ok(products);
        }
        [HttpGet("{productId}/Image")]
        public async Task<IActionResult> GetImage(int productId)
        {
            var images = await _managerProductService.GetAllProductImageAsync(productId);
            if (images == null) return BadRequest($"Cannot find image");
            return Ok(images);
         }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var products = await _managerProductService.GetProductByIdAsync(productId);
            if (products == null) return BadRequest($"Can not find product id = {productId}");
            return Ok(products);
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] PublicGetProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategoryId(request);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            var productId = await _managerProductService.Create(request);
            if (productId == 0) return BadRequest();
            var product = await GetById(productId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _managerProductService.Update(productDto);
            if (affectedResult == 0) return BadRequest($"Can not update product");
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _managerProductService.Delete(productId);
            if (affectedResult == 0) return BadRequest($"Can not delete product id = {productId}");
            return Ok();
        }

        [HttpPut("Price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery] int productId, decimal newPrice)
        {
            var isSucces = await _managerProductService.UpdatePrice(productId, newPrice);
            if (!isSucces) return BadRequest("Can not add new price");
            return Ok();
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _managerProductService.AddImages(productId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _managerProductService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetById), new { id = imageId }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm]ImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _managerProductService.UpdateImage(imageId,request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _managerProductService.DeleteImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }
    }
}
