namespace WheelsAndGoods.DataAccess.Contracts;

public interface IUnitOfWork
{
	Task CommitTransactionAsync(Action action);
	Task CommitTransactionAsync(Func<Task> action);
}
