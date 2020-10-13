using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure.Shared
{
    public class DbContextFactory : DesignTimeDbContextFactoryBase<TodoContext>
    {
        protected override TodoContext CreateNewInstance(DbContextOptions<TodoContext> options)
        {
            return new TodoContext(options);
        }
    }
}
