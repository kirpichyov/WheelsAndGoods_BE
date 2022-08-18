using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelsAndGoods.Application.Models.Orders
{
    public class CreateOrderResponce
    {
        public string Title { get; set; }
        public string Cargo { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DeliveryDeadlinAtUtc { get; set; }
        public decimal Price { get; set; }
        public Customer Customer { get; set; }
    }
}
