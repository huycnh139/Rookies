namespace Rookie.ViewModel.Dto
{
    public class RatingDto
    {
        public int? Id { get; set; }

        public int ProductId { get; set; }

        public DateTime? DateCreate { set; get; }

        public DateTime? UpdateCreate { set; get; }

        public DataAccessor.Enums.Start Star { get; set; }

        public string Comment { get; set; }
    }
}
