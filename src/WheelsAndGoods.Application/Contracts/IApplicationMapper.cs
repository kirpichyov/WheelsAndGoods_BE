namespace WheelsAndGoods.Application.Contracts;

public interface IApplicationMapper
{
	public IReadOnlyCollection<TDestination>? MapCollection<TSource, TDestination>(
		IEnumerable<TSource>? sources,
		Func<TSource, TDestination> rule);

	public IReadOnlyCollection<TDestination> MapCollectionOrEmpty<TSource, TDestination>(
		IEnumerable<TSource>? sources,
		Func<TSource, TDestination> rule);

	public IEnumerable<TDestination>? MapEnumerable<TSource, TDestination>(
		IEnumerable<TSource>? sources,
		Func<TSource, TDestination> rule);
}
