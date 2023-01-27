using System.ComponentModel.DataAnnotations;

namespace CarsCatalogAPI.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "LicensePlate is required")]
        public string LicensePlate { get; set; }
        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }
        public string Color { get; set; }
        public DateTime ReleaseYear { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
