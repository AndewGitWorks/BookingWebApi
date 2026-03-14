namespace API.Models
{
    public class ProductSortModel
    {
        public string? Search {  get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }


    }
}
