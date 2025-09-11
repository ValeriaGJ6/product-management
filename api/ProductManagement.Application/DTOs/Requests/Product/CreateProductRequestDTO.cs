namespace ProductManagement.Application.DTOs.Requests.Product
{
    public class CreateProductRequestDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}