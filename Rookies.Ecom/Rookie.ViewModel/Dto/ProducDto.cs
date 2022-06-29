﻿using Rookie.ViewModel.Catalog.Common;

namespace Rookie.ViewModel.Dto
{
    public class ProductDto : PagingRequestBase
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public decimal Cost { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreate { get; internal set; }
        public ProductImageDto ProductImageDto { set; get; }
    }
}
