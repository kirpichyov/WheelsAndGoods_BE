namespace WheelsAndGoods.Core.Models.Paginations.Responses;

public class PaginationOrderResponse<TModel>
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public IReadOnlyCollection<TModel> Data { get; set; }
}
