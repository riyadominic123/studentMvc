using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using student.Model;

namespace student.Data
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
    }
}
