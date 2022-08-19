using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.ViewModel.Dto
{
    public class CreateRatingDto
    {
        public int ProductId { get; set; }

        public DateTime? DateCreate { set; get; }

        public DateTime? DateUpdate { set; get; }

        public DataAccessor.Enums.Start Star { get; set; }

        public string Comment { get; set; }
    }
}
