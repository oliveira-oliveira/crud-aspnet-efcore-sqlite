using Microsoft.EntityFrameworkCore;
using MyFirstApi.Models;
using System.Collections.Generic;

namespace MyFirstApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("DataSource=todoApp.db;Cache=Shared");
    }
}
