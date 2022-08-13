using Microsoft.Extensions.Configuration;

namespace WheelsAndGoods.Core;

public static class ServiceCollectionExtensions
{
	public static TOptions BindFromAppSettings<TOptions>(
		this IConfiguration configuration,
		string? sectionName = null)
		where TOptions : new()
	{
		var instance = new TOptions();

		sectionName ??= typeof(TOptions).Name;
		configuration.Bind(sectionName, instance);

		return instance;
	}
}
