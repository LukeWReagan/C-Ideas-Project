using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace bank.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> user { get; set; }
        public DbSet<Function> function { get; set; }
         public DbSet<Key> key { get; set; }
    }

}