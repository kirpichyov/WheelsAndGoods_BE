namespace WheelsAndGoods.DataAccess.Contracts;

public interface IUnitOfWork
{
	IUsersRepository Users { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    Task CommitTransactionAsync(Action action);
	Task CommitTransactionAsync(Func<Task> action);
}
