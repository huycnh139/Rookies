using Microsoft.AspNetCore.Http;

namespace Rookie.ViewModel.Dto
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateUpdate { get; set; }
        public DateTime? DateCreate { get; set; }
        public bool IsDefualt { get; set; }
        public string? ImgagePath { get; set; }
        public IFormFile ImageFile { get; set; }
        public int ProductId { get; set; }
        public long? ImageSize { get; set; }
    }
}
