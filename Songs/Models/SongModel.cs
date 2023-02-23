using System;
using System.ComponentModel.DataAnnotations;

namespace Songs.Models
{
    public class Song
    {
        [Required]
        public int Id { get; set; } //PK

        [Required]
        public string? Artist { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public int Length { get; set; }



        //Foreign key
        [Required]
        //Foreign key property
        public int CategoryId { get; set; }
        //Navigation property
        public Category? Category { get; set; }
    }

    //Klass för kategorier
    public class Category
    {
        //PK
        public int CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }


        /* Navigerings som refererar till Songs
         * Tagit bort navigeringen pga det skapade nån sorts cykel som loopade oändligt
        public List<Song>? Songs { get; set; } */

        }
}
