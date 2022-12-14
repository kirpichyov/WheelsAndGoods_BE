namespace WheelsAndGoods.Application.Models.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Cargo { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DeliveryDeadlineAtUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public decimal Price { get; set; }
        public Customer Customer { get; set; }
    }
}
