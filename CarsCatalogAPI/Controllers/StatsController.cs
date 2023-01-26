using CarsCatalogAPI.Classes;
using CarsCatalogAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarsCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly CarsDBContext _carsDbContext;
        public StatsController(CarsDBContext carsDbContext, IMemoryCache cashe)
        {
            _carsDbContext = carsDbContext;
            _cache = cashe;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _cache.TryGetValue("stats", out Statistics? stats);

            if (stats == null)
            {
                int objectCount = _carsDbContext.Cars.Count();
                DateTime firstObjectAddedTime = _carsDbContext.Cars.OrderBy(e => e.CreateTime).FirstOrDefault().CreateTime;
                DateTime lastObjectAddedTime = _carsDbContext.Cars.OrderBy(e => e.CreateTime).LastOrDefault().CreateTime;
                stats = new Statistics { ObjectsCount = objectCount, FirstObjectAddedTime = firstObjectAddedTime, LastObjectAddedTime = lastObjectAddedTime };

                if (stats != null)
                {
                    _cache.Set("stats", stats, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return Ok(stats);
        }
    }
}
