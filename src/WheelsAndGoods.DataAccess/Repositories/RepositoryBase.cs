using WheelsAndGoods.DataAccess.Connection;
using WheelsAndGoods.DataAccess.Contracts;

namespace WheelsAndGoods.DataAccess.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> 
	where T : class
{
	protected readonly DatabaseContext Context;

    protected RepositoryBase(DatabaseContext context)
	{
		Context = context;
	}

	public void Add(T entity)
	{
		Context.Set<T>().Add(entity);
	}

	public void AddRange(IEnumerable<T> entities)
	{
		Context.Set<T>().AddRange(entities);
	}

	public void Remove(T entity)
	{
		Context.Set<T>().Remove(entity);
	}

	public void RemoveRange(IEnumerable<T> entities)
	{
		Context.Set<T>().RemoveRange(entities);
	}

	public void Update(T entity)
	{
		Context.Set<T>().Update(entity);
	}

	public void UpdateRange(IEnumerable<T> entities)
	{
		Context.Set<T>().UpdateRange(entities);
	}
}
