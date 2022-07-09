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
        Task<List<RatingDto>> GetAll();

        Task<List<RatingDto>> GetAllByProductId(int productId);

        Task<RatingDto> Get(int rtingId);

        Task<decimal> GetStar(int productId);

        Task<int> CreateAsync(int productId, CreateRatingDto ratingDto);

        Task<bool> Delete(int ratingId);

        Task<bool> Update(int ratingId);
        
    }
}
