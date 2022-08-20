namespace WheelsAndGoods.Application.Models.Filtering;

public class FilterOrderRequest
{
    public string Title { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal? Price { get; set; }
    public string CustomerFullName { get; set; }
}
