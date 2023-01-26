namespace CarsCatalogAPI.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public DateTime ReleaseYear { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
