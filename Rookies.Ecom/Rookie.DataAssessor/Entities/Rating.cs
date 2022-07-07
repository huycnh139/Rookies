using Rookie.DataAccessor.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rookie.DataAccessor.Entities
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public DateTime DateCreate { set; get; }

        public DateTime UpdateCreate { set; get; }

        public Enums.Start Star { get; set; }

        public string Comment { get; set; }
    }
}
