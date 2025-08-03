using Microsoft.EntityFrameworkCore;

namespace TodoAPI.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        protected TodoDbContext()
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
