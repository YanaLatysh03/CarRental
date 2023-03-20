using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Rent.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<CarEntity> Cars { get; set; }

        public DbSet<AccessTokenEntity> AccessTokens { get; set; }

        public DbSet<RentEntity> Rents { get; set; }
    }
}
