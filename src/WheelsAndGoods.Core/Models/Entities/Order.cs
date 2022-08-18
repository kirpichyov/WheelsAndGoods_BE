using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelsAndGoods.Core.Models.Entities
{
    public class Order : EntityBase<Guid>
    {
        public Order()
            :base(Guid.NewGuid())
        {
        }

        public string Title { get; set; }
        public string Cargo { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DeliveryDeadlineAtUtc { get; set; }
        public decimal Price { get; set; }
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }

    }
}
