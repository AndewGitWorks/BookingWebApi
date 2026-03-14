using Domain.Entities;

namespace API.Models
{
    public class ProductSortModel
    {
        public string? Search {  get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
        public bool IsAscending { get; set; }
        // private static void CheckChoice(bool isDescending, bool isAscending)
        //  {
        //     if (isDescending && isAscending)
        //     {
        //         throw new ArgumentException("Cannot sort by both ascending and descending order.");
        //     }
        // }
        //  public ProductSortModel()
        // {
        //     CheckChoice(this.IsDescending, this.IsAscending);
        // }
    }
    public class PagedResponse<T>
{
    public IEnumerable<T>? Items { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
}

