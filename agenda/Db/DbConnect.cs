using Microsoft.EntityFrameworkCore;
namespace agenda.Db
{
    public class DbConnect : DbContext
    {
        public DbConnect(DbContextOptions<DbConnect> option) : base(option)
        {

        }

        
    }
}
