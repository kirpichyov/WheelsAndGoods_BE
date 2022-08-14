using WheelsAndGoods.Application.Contracts;

namespace WheelsAndGoods.Application.Mapping;

public class ApplicationMapper : IApplicationMapper
{
	public IReadOnlyCollection<TDestination>? MapCollection<TSource, TDestination>(IEnumerable<TSource>? sources, Func<TSource, TDestination> rule)
	{
		return sources?.Select(rule).ToArray();
	}

	public IReadOnlyCollection<TDestination> MapCollectionOrEmpty<TSource, TDestination>(IEnumerable<TSource>? sources, Func<TSource, TDestination> rule)
	{
		return MapCollection(sources, rule) ?? Array.Empty<TDestination>();
	}
	
	public IEnumerable<TDestination>? MapEnumerable<TSource, TDestination>(IEnumerable<TSource>? sources, Func<TSource, TDestination> rule)
	{
		return sources?.Select(rule).ToArray();
	}
}
