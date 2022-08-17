using WheelsAndGoods.Core.Models.Entities;

namespace WheelsAndGoods.DataAccess.Contracts;

public interface IUsersRepository
{
	Task<User?> GetById(Guid userId, bool useTracking);
	Task<bool> IsEmailExists(string email);
	Task<bool> IsPhoneExists(string phone);
	void Add(User user);
	Task<User?> GetByEmail(string email);
}
