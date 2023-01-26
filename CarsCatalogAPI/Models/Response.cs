namespace CarsCatalogAPI.Models
{
    public class Response
    {
        public int Id { get; set; }

        public string StatusCode { get; set; }
        public string ContentType { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
    }
}
