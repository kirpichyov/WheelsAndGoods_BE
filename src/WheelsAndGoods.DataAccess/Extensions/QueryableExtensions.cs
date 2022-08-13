namespace WheelsAndGoods.DataAccess.Extensions;

using Microsoft.EntityFrameworkCore;

public static class QueryableExtensions
{
	public static IQueryable<T> WithTracking<T>(this IQueryable<T> queryable, bool useTracking)
		where T : class
	{
		return useTracking ? queryable.AsTracking() : queryable.AsNoTracking();
	}
}
