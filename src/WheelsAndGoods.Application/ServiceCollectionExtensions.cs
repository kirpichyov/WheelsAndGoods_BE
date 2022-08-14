using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Mapping;

namespace WheelsAndGoods.Application;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddScoped<IApplicationMapper, ApplicationMapper>();

		return services;
	}
}
