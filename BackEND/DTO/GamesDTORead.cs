using BackEND.Entities;

namespace BackEND.DTO
{
    public class GamesDTORead
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public int? GenreId { get; set; }
        public int? CategoryId { get; set; }


    }
}
