using Rookie.ViewModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Interface
{
    public interface IRatingService
    {
        Task<List<RatingDto>> GetAllAsync();

        Task<List<RatingDto>> GetAllByProductIdAsync(int productId);

        Task<RatingDto> GetAsync(int rtingId);

        Task<decimal> GetStarAsync(int productId);

        Task<int> CreateAsync(int productId, CreateRatingDto ratingDto);

        Task<bool> DeleteAsync(int ratingId);

        Task<bool> UpdateAsync(int ratingId);
        
    }
}
