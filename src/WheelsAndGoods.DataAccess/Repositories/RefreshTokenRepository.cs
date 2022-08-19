using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.DataAccess.Repositories;

public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(DatabaseContext context) 
        : base(context)
    {
    }
}
