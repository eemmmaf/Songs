namespace Songs.Data
{
    using Songs.Models;
    using Microsoft.EntityFrameworkCore;
    public class SongContext : DbContext
    {
        public SongContext(DbContextOptions<SongContext> options) : base(options) { }   
        
        //Använder modellen SongModel och döper tabellen till Songs och Categories
        public DbSet<Song> Songs { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
