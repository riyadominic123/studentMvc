using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using student.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace student.Data
{
    public class AppDbContext : IdentityDbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<UserClass> UserClasses { get; set; }
    }

}
