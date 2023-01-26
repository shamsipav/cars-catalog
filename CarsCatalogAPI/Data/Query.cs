using CarsCatalogAPI.Models;

namespace CarsCatalogAPI.Data
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Car> GetCars([Service] CarsDBContext context) => context.Cars;
    }
}
