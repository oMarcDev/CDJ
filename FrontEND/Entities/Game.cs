namespace FrontEND.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public Genre? Genre { get; set; } 
        public Category? Category { get; set; }

    }
}
