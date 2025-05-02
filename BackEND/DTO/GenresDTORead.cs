namespace BackEND.DTO
{
    public class GenresDTORead
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<GamesDTORead> Games { get; set; } = new List<GamesDTORead>();

    }
}
