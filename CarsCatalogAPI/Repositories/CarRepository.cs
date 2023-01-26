using CarsCatalogAPI.Data;
using CarsCatalogAPI.Interfaces;

namespace CarsCatalogAPI.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarsDBContext _carsDbContext;

        public CarRepository(CarsDBContext carsDbContext)
        {
            _carsDbContext = carsDbContext;
        }
    }
}
