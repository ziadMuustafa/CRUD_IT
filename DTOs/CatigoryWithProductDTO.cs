using System.Text.Json.Serialization;

namespace API_1.DTOs
{
    public class CatigoryWithProductDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? CatigoryName { get; set; }

        [JsonIgnore]
        public ICollection<Product>? AllowedProducts { get; set; }
    
        public int ProductsCount { get; set; }

    }
}
