using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelsAndGoods.Application.Models.Orders
{
    public class UpdateOrderRequest
    {
        public string Title { get; set; }
        public string Cargo { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DeliveryDeadlineAtUtc { get; set; }
        public decimal Price { get; set; }
    }
}
