using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Mapping;
using WheelsAndGoods.Application.Services;

namespace WheelsAndGoods.Application;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationServices(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddScoped<IApplicationMapper, ApplicationMapper>();
		services.AddScoped<IHashingProvider, HashingProvider>();
		services.AddScoped<IAuthService, AuthService>();

		return services;
	}
}
