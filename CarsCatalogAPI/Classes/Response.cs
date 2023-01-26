using CarsCatalogAPI.Models;

namespace CarsCatalogAPI.Classes
{
    public class Response
    {
        public bool Ok { get; set; }
        public Car? Car { get; set; }
        public int? Id { get; set; }
        public string Message { get; set; }
    }
}
