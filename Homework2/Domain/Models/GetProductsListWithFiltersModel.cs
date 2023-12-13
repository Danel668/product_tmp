using Domain.Enums;

namespace Domain.Models
{
    public class GetProductsListWithFiltersModel
    {
        public DateTime? CreationDate { get; set; }
        public ProductType? Type { get; set; }
        public int? WarehouseId { get; set; }
        public int PageSize { get; set; }
        public int Pagination { get; set; }
    }
}
