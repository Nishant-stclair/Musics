using System.ComponentModel.DataAnnotations;

namespace MvcSongs.Models
{
    public class Songs
    {
        public int Id { get; set; }
        public string? SingerName { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }

        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public string? Production { get; set; }
        public bool IsHidden { get; set; }
    }
}