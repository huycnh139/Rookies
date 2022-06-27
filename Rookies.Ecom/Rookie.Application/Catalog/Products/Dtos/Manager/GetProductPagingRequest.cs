using Rookie.Application.Catalog.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Application.Catalog.Products.Dtos.Manager
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string keyWord { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
 