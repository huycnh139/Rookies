using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Dto
{
    public class ImageCreateRequest
    {
        public string Name { get; set; }
        public int? ProductId { get; set; }
        public DateTime? DateCreate { get; set; }
        public bool IsDefualt { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
