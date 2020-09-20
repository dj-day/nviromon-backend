using Microsoft.EntityFrameworkCore;
using Nviromon.Models;

namespace Nviromon.Data
{
    public class DataService : DbContext
    {
        public DataService(DbContextOptions<DataService> options) : base(options) {}
        public DbSet<Readings> Readings { get; set; }

        public DbSet<User> Users { get; set; }
    }
}