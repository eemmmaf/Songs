using System;
using System.ComponentModel.DataAnnotations;

namespace Songs.Models
{
    public class Song
    {
        public int Id { get; set; } //PK

        [Required]
        public string? Artist { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public int Length { get; set; }



        //Foreign key
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }

    //Klass för kategorier
    public class Category
    {
        //PK
        public int CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }


        // Navigerings som refererar till Songs
        public List<Song>? Songs { get; set; }

        }
}
