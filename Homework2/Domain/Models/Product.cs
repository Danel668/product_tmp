using Domain.Enums;

namespace Domain.Models
{
    public class Product
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; set; }
        public double Weight { get; init; }
        public ProductType Type { get; init; }
        public DateTime CreationDate { get; init; }
        public int WarehouseId { get; init; }
    }
}
