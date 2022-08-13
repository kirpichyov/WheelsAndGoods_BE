using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WheelsAndGoods.Application;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		// TODO: register services and options for DI container.

		return services;
	}
}
