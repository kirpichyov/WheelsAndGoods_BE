using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelsAndGoods.Core.Models.Entities
{
    public class OrderRequest : EntityBase<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
