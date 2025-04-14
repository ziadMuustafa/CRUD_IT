using System.ComponentModel.DataAnnotations.Schema;

namespace API_1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }


        [ForeignKey(nameof(Catigory))]
        public int? CatigoryId { get; set; }

        [NotMapped]
        public Catigory? Catigory { get; set; }
    }
}
