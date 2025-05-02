using System.Text.Json.Serialization;

namespace BackEND.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [JsonIgnore]
        public List<Game> Games { get; set; } = new List<Game>();

    }
}
