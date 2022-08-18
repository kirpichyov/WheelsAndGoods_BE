﻿using Microsoft.Extensions.Configuration;
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
        
		services.AddScoped<IApplicationMapper, ApplicationMapper>();
		services.AddScoped<IHashingProvider, HashingProvider>();
		services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUsersService, UsersService>();
		services.AddScoped<IProfileService, ProfileService>();

        services.AddSingleton<IBlobService, BlobService>();
        services.AddSingleton<IEmailSenderService, EmailSenderService>();

		return services;
	}
}
