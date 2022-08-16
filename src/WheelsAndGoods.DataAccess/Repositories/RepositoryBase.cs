using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.DataAccess.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> 
	where T : class
{
	private readonly DatabaseContext _context;

	public RepositoryBase(DatabaseContext context)
	{
		_context = context;
	}

	public void Add(T entity)
	{
		_context.Set<T>().Add(entity);
	}

	public void AddRange(IEnumerable<T> entities)
	{
		_context.Set<T>().AddRange(entities);
	}

	public void Remove(T entity)
	{
		_context.Set<T>().Remove(entity);
	}

	public void RemoveRange(IEnumerable<T> entities)
	{
		_context.Set<T>().RemoveRange(entities);
	}

	public void Update(T entity)
	{
		_context.Set<T>().Update(entity);
	}

	public void UpdateRange(IEnumerable<T> entities)
	{
		_context.Set<T>().UpdateRange(entities);
	}
}
