using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rookie.Application.Interface;
using Rookie.ViewModel.Dto;

namespace Rookie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpGet("{productId}/product-rating")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var ratings = await _ratingService.GetAllByProductId(productId);
            if (ratings == null) return BadRequest($"Can not find product id = {productId}");
            return Ok(ratings);
        }
        [HttpGet("{ratingId}")]
        public async Task<IActionResult> GetById(int ratingId)
        {
            var ratings = await _ratingService.Get(ratingId);
            if (ratings == null) return BadRequest($"Can not find rating id = {ratingId}");
            return Ok(ratings);
        }
        [HttpGet("/star/{productId}")]
        public async Task<IActionResult> GetStarByProductId(int productId)
        {
            var ratings = await _ratingService.GetStar(productId);
            if (ratings == 0) return BadRequest();
            return Ok(ratings);
        }
        [HttpPost("{productId}")]
        public async Task<IActionResult> CreateRating([FromBody]int productId, CreateRatingDto ratingDto)
        {
            var ratingId = await _ratingService.CreateAsync(productId,ratingDto);
            if (ratingId == 0) return BadRequest();
            var rating = await GetById(ratingId);
            return CreatedAtAction(nameof(GetById), new { id = ratingId }, ratingId);
        }
    }
}
