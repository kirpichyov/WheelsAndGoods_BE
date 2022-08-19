using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;
using WheelsAndGoods.DataAccess.Extensions;

namespace WheelsAndGoods.DataAccess.Repositories;

public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(DatabaseContext context)
        : base(context)
    {
    }

    public async Task<RefreshToken?> GetById(Guid id, bool useTracking)
    {
        return await Context.RefreshTokens
            .WithTracking(useTracking)
            .FirstOrDefaultAsync(refreshToken => refreshToken.Id == id);
    }
}
