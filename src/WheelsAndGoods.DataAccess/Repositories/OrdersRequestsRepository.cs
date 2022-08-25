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
    }
}
