using Rookie.Application.Catalog.Dto;
using Rookie.Application.Catalog.Products.Dtos;
using Rookie.Application.Catalog.Products.Dtos.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Catalog.Products
{
    public interface IManagerProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId,int addeQuantity);

        Task AddViewCount(int productId); 

        Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);

    }
}
