using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.Contracts
{
    public interface IOrdersRequestsRepository : IRepositoryBase<OrderRequest>
    {
        Task<bool> IsUserAlreadyHasRequest(Guid orderId, Guid userId);
    }
}
