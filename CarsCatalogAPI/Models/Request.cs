namespace CarsCatalogAPI.Models
{
    public class Request
    {
        public int Id { get; set; }

        public string Method { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Headers { get; set; }
        public string Schema { get; set; }
        public string Host { get; set; }
        public string Body { get; set; }
    }
}
