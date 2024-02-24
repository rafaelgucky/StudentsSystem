using Microsoft.EntityFrameworkCore;
using SellersManager.Models;

namespace SellersManager.DataBase
{
    public class AplicationContext : DbContext
    {
        public AplicationContext(DbContextOptions<AplicationContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        //public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Avaliation> Avaliations { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
