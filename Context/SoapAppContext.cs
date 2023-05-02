using Microsoft.EntityFrameworkCore;
using SoapConsumer.Models;

namespace SoapConsumer.Context
{
    public class SoapAppContext : DbContext
    {
        public SoapAppContext(DbContextOptions<SoapAppContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
