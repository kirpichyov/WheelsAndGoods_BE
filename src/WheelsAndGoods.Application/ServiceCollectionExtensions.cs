using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WheelsAndGoods.Application.Contracts;
using WheelsAndGoods.Application.Contracts.Services;
using WheelsAndGoods.Application.Mapping;
using WheelsAndGoods.Application.Options;
using WheelsAndGoods.Application.Services;

namespace WheelsAndGoods.Application;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationServices(
		this IServiceCollection services,
		IConfiguration configuration)
    {
        services.Configure<BlobOptions>(configuration.GetSection(nameof(BlobOptions)));
        services.Configure<SendGridOptions>(configuration.GetSection(nameof(SendGridOptions)));
        services.Configure<ResetPasswordCodeOptions>(configuration.GetSection(nameof(ResetPasswordCodeOptions)));
        
		services.AddScoped<IApplicationMapper, ApplicationMapper>();
		services.AddScoped<IHashingProvider, HashingProvider>();
		services.AddScoped<IAuthService, AuthService>();
		services.AddScoped<IResetPasswordCodesService, ResetPasswordCodesService>();
		services.AddScoped<IKeysGeneratorService, KeysGeneratorService>();
        services.AddScoped<IUsersService, UsersService>();
		services.AddScoped<IProfileService, ProfileService>();
		services.AddScoped<IOrdersService, OrdersService>();

		services.AddSingleton<IBlobService, BlobService>();
        services.AddSingleton<IEmailSenderService, EmailSenderService>();

		return services;
	}
}
