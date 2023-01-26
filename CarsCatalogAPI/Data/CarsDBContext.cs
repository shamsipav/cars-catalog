using CarsCatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsCatalogAPI.Data
{
    public class CarsDBContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<RequestInfo> RequestInfos { get; set; }

        public CarsDBContext(DbContextOptions<CarsDBContext> options) : base(options)
        {

        }
    }
}
