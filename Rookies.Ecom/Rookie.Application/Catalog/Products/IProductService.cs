using Rookie.Application.Catalog.Dto;
using Rookie.Application.Catalog.Products.Dtos;
using Rookie.Application.Catalog.Products.Dtos.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Catalog.Products
{
    public interface IProductService
    {
        public Task<PageResult<ProductViewModel>> GetAllByCategoryId(PublicGetProductPagingRequest request);
     } 
}
