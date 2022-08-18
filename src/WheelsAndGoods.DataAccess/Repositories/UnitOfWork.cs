using Microsoft.Extensions.Logging;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly DatabaseContext _databaseContext;
	private readonly ILogger<UnitOfWork> _logger;

	private IUsersRepository? _usersRepository;
	private IOrdersRepository? _ordersRepository;

	public UnitOfWork(DatabaseContext databaseContext, ILogger<UnitOfWork> logger)
	{
		_databaseContext = databaseContext;
		_logger = logger;
	}

	public IUsersRepository Users => _usersRepository ??= new UsersRepository(_databaseContext);
	public IOrdersRepository Orders => _ordersRepository ??= new OrdersRepository(_databaseContext);
	
	public async Task CommitTransactionAsync(Action action)
	{
		await using var transaction = await _databaseContext.Database.BeginTransactionAsync();

		try
		{
			action();
			await _databaseContext.SaveChangesAsync();
			await transaction.CommitAsync();
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "Exception occured in transaction: {Message}.", exception.Message);
			await transaction.RollbackAsync();
			throw;
		}
	}

	public async Task CommitTransactionAsync(Func<Task> action)
	{
		await using var transaction = await _databaseContext.Database.BeginTransactionAsync();

		try
		{
			await action();
			await _databaseContext.SaveChangesAsync();
			await transaction.CommitAsync();
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "Exception occured in transaction: {Message}.", exception.Message);
			await transaction.RollbackAsync();
			throw;
		}
	}
}
