using AppNote.Models;
using Microsoft.EntityFrameworkCore;

namespace AppNote.Data
{
    public class DBContext : DbContext
    {
        // add tables
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var filename = Path.Combine(FileSystem.AppDataDirectory, "AppDBNote.db");
            optionsBuilder.UseSqlite("FileName="+filename);
        }
    }
}
