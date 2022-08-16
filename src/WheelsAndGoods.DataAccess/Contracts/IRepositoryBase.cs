namespace WheelsAndGoods.DataAccess.Contracts;

public interface IRepositoryBase<T> 
	where T : class
{
	void Add(T entity);
	void AddRange(IEnumerable<T> entities);
	void Remove(T entity);
	void RemoveRange(IEnumerable<T> entities);
	void Update(T entity);
	void UpdateRange(IEnumerable<T> entities);
}
