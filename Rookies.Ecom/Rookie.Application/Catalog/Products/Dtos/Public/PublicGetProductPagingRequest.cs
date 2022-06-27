using Rookie.Application.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Catalog.Products.Dtos.Public
{
    public class PublicGetProductPagingRequest :PagingRequestBase
    {
        public int? CategoryId { get; set; }
    }
}
