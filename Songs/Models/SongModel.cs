using System.ComponentModel.DataAnnotations;

namespace Songs.Models
{
    public class SongModel
    {
        public int Id { get; set; } //PK

        [Required]
        public string? Artist { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public string Category { get; set; }

    }
}
