using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;
using MyFirstWebApp.Business;

namespace MyFirstWebApp.Context.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext (DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<MyFirstWebApp.Business.Todo> Todo { get; set; }
        public DbSet<MyFirstWebApp.Business.Counter> Counter { get; set; }
    }
}
