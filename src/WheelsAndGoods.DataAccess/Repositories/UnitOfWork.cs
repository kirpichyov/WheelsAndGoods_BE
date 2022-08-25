using Microsoft.Extensions.Logging;
using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly DatabaseContext _databaseContext;
	private readonly ILogger<UnitOfWork> _logger;

	private IUsersRepository? _usersRepository;
    private IRefreshTokenRepository? _refreshTokenRepository;
	private IOrdersRepository? _ordersRepository;
	private IResetPasswordCodesRepository? _resetPasswordCodesRepository;
	private IOrdersRequestsRepository? _ordersRequestsRepository;

	public UnitOfWork(DatabaseContext databaseContext, ILogger<UnitOfWork> logger)
	{
		_databaseContext = databaseContext;
		_logger = logger;
    }

	public IUsersRepository Users => _usersRepository ??= new UsersRepository(_databaseContext);
	public IRefreshTokenRepository RefreshTokens => _refreshTokenRepository ??= new RefreshTokenRepository(_databaseContext);
	public IOrdersRepository Orders => _ordersRepository ??= new OrdersRepository(_databaseContext);
    public IResetPasswordCodesRepository ResetPasswordCodes => _resetPasswordCodesRepository 
        ??= new ResetPasswordCodesRepository(_databaseContext);
	public IOrdersRequestsRepository OrdersRequests => _ordersRequestsRepository ??= new OrdersRequestsRepository(_databaseContext);
	
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
    
    public async Task<TResult> CommitTransactionAsync<TResult>(Func<TResult> action)
    {
        await using var transaction = await _databaseContext.Database.BeginTransactionAsync();

        try
        {
            var result = action();
            await _databaseContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception occured in transaction: {Message}.", exception.Message);
            await transaction.RollbackAsync();
            throw;
        }
    }
}
