using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public Enums.Size Size { get; set; }
        public Enums.Dough Dough { get; set; }
        public int ProductId { get; set; }
    }
}
