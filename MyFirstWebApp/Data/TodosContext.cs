using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;

namespace Todos.Data
{
    public class TodosContext : DbContext
    {
        public TodosContext (DbContextOptions<TodosContext> options)
            : base(options)
        {
        }

        public DbSet<CounterModel> Movie { get; set; }
    }
}