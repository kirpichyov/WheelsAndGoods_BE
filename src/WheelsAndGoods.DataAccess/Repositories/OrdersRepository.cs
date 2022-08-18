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
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        private readonly DatabaseContext _databaseContext;

        public OrdersRepository(DatabaseContext databaseContext)
            : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
