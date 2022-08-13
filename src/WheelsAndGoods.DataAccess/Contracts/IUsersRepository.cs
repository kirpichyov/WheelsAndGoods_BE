using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.Contracts;

public interface IUsersRepository
{
	Task<User?> GetById(Guid userId, bool useTracking);
	void Add(User user);
}
