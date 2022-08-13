namespace WheelsAndGoods.DataAccess.Contracts;

public interface IUnitOfWork
{
	IUsersRepository Users { get; }
	
	Task CommitTransactionAsync(Action action);
	Task CommitTransactionAsync(Func<Task> action);
}
