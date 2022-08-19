﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Dto
{
    public class ProductCreateRequest
    {
        public int CategoryId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public decimal Cost { set; get; }
        public int Stock { set; get; }
        public int? ViewCount { set; get; }
        public DateTime? DateCreate { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
