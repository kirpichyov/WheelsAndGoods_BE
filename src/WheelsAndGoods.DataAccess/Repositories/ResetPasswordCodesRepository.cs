using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.DataAccess.Repositories;

public class ResetPasswordCodesRepository : RepositoryBase<ResetPasswordCode>, IResetPasswordCodesRepository
{
    public ResetPasswordCodesRepository(DatabaseContext context) 
        : base(context)
    {
    }
}
