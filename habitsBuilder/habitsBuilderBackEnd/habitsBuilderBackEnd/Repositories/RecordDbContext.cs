using habitsBuilderBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace habitsBuilderBackEnd.Repositories
{
    public class RecordDbContext : DbContext
    {
        public RecordDbContext(DbContextOptions<RecordDbContext> options)
           : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Record> Records { get; set; }
    }
}
