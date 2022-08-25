using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.DataAccess.Repositories
{
    public class OrdersRequestsRepository : RepositoryBase<OrderRequest>, IOrdersRequestsRepository
    {
        public OrdersRequestsRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {

        }

        public async Task<bool> CheckIfOrderedByUser(Guid orderId, Guid userId)
        {
            if(Context.OrdersRequests.FirstOrDefault(entity => entity.UserId == userId && entity.OrderId == orderId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
