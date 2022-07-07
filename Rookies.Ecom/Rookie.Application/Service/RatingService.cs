using Microsoft.EntityFrameworkCore;
using Rookie.Application.Interface;
using Rookie.DataAccessor.Data;
using Rookie.DataAccessor.Entities;
using Rookie.Utilities.Exceptions;
using Rookie.ViewModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Service
{
    public class RatingService : IRatingService
    {
        private readonly EcomDbContext _ecomDbContext;
        private readonly IStorageService _storageService;

        public RatingService(EcomDbContext ecomDbContext, IStorageService storageService)
        {
            _ecomDbContext = ecomDbContext;
            _storageService = storageService;
        }

        public async Task<int> CreateAsync(int productId, RatingDto ratingDto)
        {
            if (productId == null) throw new EComException($"Can not find ProductID");
            var rating = new Rating()
            {
                ProductId = productId,
                DateCreate = DateTime.Now,
                Star = ratingDto.Star,
                Comment = ratingDto.Comment
            };
            _ecomDbContext.Ratings.Add(rating);
            await _ecomDbContext.SaveChangesAsync();
            return rating.Id;
        }

        public Task<bool> Delete(int ratingId)
        {
            throw new NotImplementedException();
        }

        public async Task<RatingDto> Get(int ratingId)
        {
            var rating = await _ecomDbContext.Ratings.FindAsync(ratingId);
            if (rating == null) throw new EComException($"Cannot find an rating with id: {ratingId}");
            var viewRating = new RatingDto()
            {
                Id = ratingId,
                ProductId = rating.ProductId,
                Star = rating.Star,
                Comment = rating.Comment
            };
            return viewRating;
        }

        public Task<List<RatingDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RatingDto>> GetAllByProductId(int productId)
        {
             return await _ecomDbContext.Ratings.Where(x => x.ProductId == productId)
                .Select(i => new RatingDto()
                {
                    Id = i.Id, 
                    Star = i.Star,
                    ProductId = productId,
                    DateCreate= i.DateCreate,
                    Comment = i.Comment
                }).ToListAsync();
             
        }

        public async Task<decimal> GetStar(int productId)
        {
            var ratings = await _ecomDbContext.Ratings
                .Where(x => x.ProductId == productId)
                .ToListAsync();
            var count = ratings.Average(i => (decimal)i.Star);
            return count;
        }

        public Task<bool> Update(int ratingId)
        {
            throw new NotImplementedException();
        }
    }
}
