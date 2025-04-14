namespace API_1.Models
{
    public class Catigory
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
