using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelsAndGoods.Application.Models.Orders
{
    public class TakeOrderResponse
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreaterAtUtc { get; set; }
        public OrderRequestAuthor Author { get; set; }
    }
}
