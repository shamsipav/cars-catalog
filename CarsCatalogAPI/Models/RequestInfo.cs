namespace CarsCatalogAPI.Models
{
    public class RequestInfo
    {
        public int Id { get; set; }

        public Request Request { get; set; }
        public Response? Response { get; set; }
    }
}
