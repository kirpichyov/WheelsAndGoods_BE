namespace WheelsAndGoods.DataAccess.Contracts;

public interface IUnitOfWork
{
	IUsersRepository Users { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IOrdersRepository Orders { get; }
	
	Task CommitTransactionAsync(Action action);
	Task CommitTransactionAsync(Func<Task> action);
    Task<TResult> CommitTransactionAsync<TResult>(Func<TResult> action);
}
