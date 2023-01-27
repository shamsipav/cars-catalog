using CarsCatalogAPI.Data;

namespace CarsCatalogAPI.Models
{
    public class Statistics
    {
        public long ObjectsCount { get; set; }
        public DateTime FirstObjectAddedTime { get; set; }
        public DateTime LastObjectAddedTime { get; set; }
    }
}
