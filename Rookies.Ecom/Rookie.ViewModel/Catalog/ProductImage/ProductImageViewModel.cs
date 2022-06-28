using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Catalog.ProductImage
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }

        public long ImageSize { get; set; }
    }
}
