namespace WheelsAndGoods.Core.Models.Filter;

public class FilterOrderModel
{
    public string Title { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal? Price { get; set; }
    public string CustomerFullName { get; set; }
}
