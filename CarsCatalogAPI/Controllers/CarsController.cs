using CarsCatalogAPI.Classes;
using CarsCatalogAPI.Data;
using CarsCatalogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarsCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly CarsDBContext _carsDbContext;
        public CarsController(CarsDBContext carsDbContext, IMemoryCache cashe)
        {
            _carsDbContext = carsDbContext;
            _cache = cashe;
        }

        [HttpGet]
        public List<Car> Get(string? brand)
        {
            _cache.TryGetValue("carsList", out List<Car>? carsList);

            if (carsList == null)
            {
                carsList = _carsDbContext.Cars.ToList();
                if (carsList != null)
                {
                    _cache.Set("carsList", carsList, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return carsList;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _cache.TryGetValue(id, out Car? car);

            if (car == null)
            {
                car = _carsDbContext.Cars.FirstOrDefault(x => x.Id == id);
                if (car != null)
                {
                    _cache.Set(car.Id, car, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                } else
                {
                    return NotFound("Objest was not found");
                }
            }

            return Ok(car);
        }

        [HttpPost]
        public IActionResult Post(Car car)
        {
            var existCar = _carsDbContext.Cars.FirstOrDefault(x => x.LicensePlate == car.LicensePlate);
            if (existCar != null) return BadRequest("Object already exists");

            car.CreateTime = DateTime.Now;
            car.UpdateTime = DateTime.Now;

            try 
            {
                _carsDbContext.Cars.Add(car);
                _carsDbContext.SaveChanges();
            } 
            catch 
            {
                return Problem("Error");
            }

            _cache.Remove("carsList");
            _cache.Remove("stats");

            return Ok("Success");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Car car)
        {
            var existCar = _carsDbContext.Cars.FirstOrDefault(x => x.LicensePlate == car.LicensePlate);

            if (existCar == null) return NotFound("Objest was not found");

            existCar.LicensePlate = car.LicensePlate;
            existCar.Brand = car.Brand;
            existCar.Color = car.Color;
            existCar.ReleaseYear = car.ReleaseYear;
            existCar.UpdateTime = existCar.UpdateTime;

            try
            {
                _carsDbContext.Cars.Update(existCar);
                _carsDbContext.SaveChanges();
            }
            catch
            {
                return Problem("Error");
            }

            _cache.Remove("stats");

            return Ok("Success");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existCar = _carsDbContext.Cars.FirstOrDefault(x => x.Id == id);

            if (existCar == null) return NotFound("Objest was not found");

            try
            {
                _carsDbContext.Cars.Remove(existCar);
                _carsDbContext.SaveChanges();
            }
            catch
            {
                return Problem("Error");
            }

            _cache.Remove("stats");

            return Ok("Success");
        }
    }
}
