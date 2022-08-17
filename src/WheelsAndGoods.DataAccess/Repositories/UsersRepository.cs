using Microsoft.EntityFrameworkCore;
using WheelsAndGoods.Core.Models.Entities;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;
using WheelsAndGoods.DataAccess.Extensions;

namespace WheelsAndGoods.DataAccess.Repositories;

public class UsersRepository : RepositoryBase<User>, IUsersRepository
{
	private readonly DatabaseContext _databaseContext;

	public UsersRepository(DatabaseContext databaseContext) 
		: base(databaseContext)
	{
		_databaseContext = databaseContext;
	}
	
	public async Task<User?> GetById(Guid userId, bool useTracking)
	{
		return await _databaseContext.Users
			.WithTracking(useTracking)
			.FirstOrDefaultAsync(user => user.Id == userId);
	}

	public async Task<bool> IsEmailExists(string email)
	{
		return await _databaseContext.Users.AnyAsync(user => user.Email == email);
	}

	public async Task<bool> IsPhoneExists(string phone)
	{
		return await _databaseContext.Users.AnyAsync(user => user.Phone == phone);
	}

	public async Task<User?> GetByEmail(string email)
	{
		return await _databaseContext.Users.FirstOrDefaultAsync(user => user.Email == email);
	}
}
