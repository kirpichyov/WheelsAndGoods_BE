using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;
using WheelsAndGoods.DataAccess.Extensions;

namespace WheelsAndGoods.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
	private readonly DatabaseContext _databaseContext;

	public UsersRepository(DatabaseContext databaseContext)
	{
		_databaseContext = databaseContext;
	}
	
	public async Task<User?> GetById(Guid userId, bool useTracking)
	{
		return await _databaseContext.Users
			.WithTracking(useTracking)
			.FirstOrDefaultAsync(user => user.Id == userId);
	}

	public void Add(User user)
	{
		_databaseContext.Users.Add(user);
	}
}
