using Microsoft.EntityFrameworkCore;

using TaskMangerServer.Model;

namespace TaskMangerServer.Data
{
    public class TaskManagerServerContext : DbContext
    {
        public TaskManagerServerContext(DbContextOptions<TaskManagerServerContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> TaskItem { get; set; } = default!;
    }
}
