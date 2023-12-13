using DataLayer.Enums;

namespace DataLayer.Models
{
    public class FilteredProductListRequest
    {
        public DateTime? CreationDate { get; set; }
        public ProductType? Type { get; set; }
        public int? WarehouseId { get; set; }
        public int PageSize { get; set; }
        public int Pagination { get; set; }
    }
}
