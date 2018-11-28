using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestForJob.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
