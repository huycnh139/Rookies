using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Dto
{
    public class GetProductByCategoryId
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public decimal Price { set; get; } 
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }
    }
}
