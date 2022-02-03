using agenda.Models;
using Microsoft.EntityFrameworkCore;
namespace agenda.Db
{
    public class DbConnect : DbContext
    {
        public DbConnect(DbContextOptions<DbConnect> option) : base(option)
        {
    
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
    }
}
