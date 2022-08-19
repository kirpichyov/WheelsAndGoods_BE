using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.Contracts;

public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
{
    Task<RefreshToken?> GetById(Guid id, bool useTracking);
}
