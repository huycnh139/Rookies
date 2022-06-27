using Rookie.DataAccessor.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.DataAccessor.Entities
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }

        public Star Star { get; set; }

        public string Comment { get; set; }
    }
}
