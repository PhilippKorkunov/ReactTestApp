using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DataLayer
{
    public class Context : DbContext
    {
        public DbSet<Post> Post { get; set; }



        public Context(DbContextOptions<Context> options) : base(options)
        { }
    }
}
