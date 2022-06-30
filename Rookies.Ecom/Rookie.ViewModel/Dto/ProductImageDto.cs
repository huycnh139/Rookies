using Microsoft.AspNetCore.Http;

namespace Rookie.ViewModel.Dto
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProductId { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDefualt { get; set; }
        public IFormFile ImageFile { get; set; }
        public long? ImageSize { get; set; }
        public string ImgagePath { get; set; }
    }
}
