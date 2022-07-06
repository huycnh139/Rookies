using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Dto
{
    public class ImageUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool IsDefualt { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
